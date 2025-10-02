using System.Text;
using Fluid;
using Fluid.Ast;
using Fluid.Parser;
using Fluid.Values;
using IbanNet.CodeGen.Extensions;
using IbanNet.CodeGen.Swift;
using IbanNet.CodeGen.Syntax;
using IbanNet.Registry.Patterns;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.FileProviders;

namespace IbanNet.CodeGen.Liquid;

internal sealed class FluidProviderGenerator
{
    public string Compile(SourceProductionContext ctx, RegistryProviderTarget target, IEnumerable<SwiftCsvRecord> model)
    {
        IFluidTemplate template = LoadTemplate(ctx, target, "RegistryProvider");

        var opts = new TemplateOptions
        {
            // We're not distributing the code generator, it is only used by ourselves, so it is safe to allow all properties.
            MemberAccessStrategy = new UnsafeMemberAccessStrategy { IgnoreCasing = true }, FileProvider = new EmbeddedFileProvider(GetType().Assembly, GetType().Namespace!)
        };
        opts.ValueConverters.Add(value => value is AsciiCategory v ? Enum.GetName(typeof(AsciiCategory), v) : null);
        opts.ValueConverters.Add(value => value is IbanCsvData v ? new StructureValue(v, s => new PatternWrapper(s, v.Tokenizer)) : null);
        opts.ValueConverters.Add(value => value is PatternCsvData v ? new StructureValue(v, s => new PatternWrapper(s, v.Tokenizer)) : null);
        var m = new
        {
            Generator = new { Name = nameof(RegistryProviderTransformGenerator), Version = "2.0" },
            Syntax = target,
            Datasource = target.InputSourcePath,
            Countries = model
                .Where(record => !Boycott(record.CountryCode))
                .OrderBy(record => record.CountryCode)
                .ToList()
        };

        var context = new TemplateContext(m, opts);
        try
        {
            return template.Render(context);
        }
        catch (ParseException ex)
        {
            ctx.ReportWarning("IBAN9103", "Liquid template rendering failed.", ex.Message, GetType().FullName!, target.Location);
            return string.Empty;
        }
    }

    private IFluidTemplate LoadTemplate(SourceProductionContext ctx, RegistryProviderTarget target, string name)
    {
        string resourceName = $"{name}.liquid";
        string errorCode, errorTitle, errorMsg;

        using Stream? stream = typeof(FluidProviderGenerator).Assembly.GetManifestResourceStream(typeof(FluidProviderGenerator), resourceName);
        if (stream is null)
        {
            errorCode = "IBAN9101";
            errorTitle = "Cannot load liquid template from embedded resource.";
            errorMsg = $"The liquid template in resource stream '{resourceName}' could not be loaded.";
        }
        else
        {
            using var sr = new StreamReader(stream, Encoding.UTF8);
            string source = sr.ReadToEnd();

            var opts = new FluidParserOptions { AllowFunctions = true };
            var parser = new FluidParser(opts);

            RegisterRepeatBlock(parser);

            if (parser.TryParse(source, out IFluidTemplate? template, out errorMsg))
            {
                return template;
            }

            errorCode = "IBAN9102";
            errorTitle = "Failed to parse liquid template.";
        }

        ctx.ReportWarning(errorCode, errorTitle, errorMsg, GetType().FullName!, target.Location);

        return new FluidTemplate();
    }

    private static void RegisterRepeatBlock(FluidParser parser)
    {
        parser.RegisterExpressionBlock("repeat",
            async (value, statements, writer, encoder, context) =>
            {
                FluidValue? repeatCount = await value.EvaluateAsync(context).ConfigureAwait(false);
                for (int i = 0; i < repeatCount.ToNumberValue(); i++)
                {
                    await statements.RenderStatementsAsync(writer, encoder, context).ConfigureAwait(false);
                }

                return Completion.Normal;
            });
    }

    private sealed class StructureValue
        : ObjectValueBase
    {
        private readonly Func<string, Pattern> _patternConverter;

        public StructureValue(object value, Func<string, Pattern> patternConverter)
            : base(value)
        {
            _patternConverter = patternConverter;
        }

        public override ValueTask<FluidValue> GetValueAsync(string name, TemplateContext context)
        {
            if (name != "pattern")
            {
                return base.GetValueAsync(name, context);
            }

            string? pattern = (Value as PatternCsvData)?.Pattern ?? (Value as IbanCsvData)?.Pattern;
            return pattern is null
                ? base.GetValueAsync(name, context)
                : Create(_patternConverter(pattern), context.Options);
        }
    }

    private static bool Boycott(string countryCode)
    {
        return countryCode == "RU"; // Go Ukraine!
    }
}
