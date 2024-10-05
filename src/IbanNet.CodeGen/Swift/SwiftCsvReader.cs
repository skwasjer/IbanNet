using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using IbanNet.CodeGen.Swift.Converters;

namespace IbanNet.CodeGen.Swift;

public sealed class SwiftCsvReader : CsvReader
{
    private static readonly Func<CsvConfiguration> CreateConfiguration = () => new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        Delimiter = "\t",
        TrimOptions = TrimOptions.Trim,
        WhiteSpaceChars = new[] { ' ' },
        HasHeaderRecord = true,
        AllowComments = false,
        DetectDelimiter = false,
        HeaderValidated = _ => { },
        IgnoreBlankLines = true,
        IncludePrivateMembers = false,
        UseNewObjectForNullReferenceMembers = false,
        MissingFieldFound = _ => { }
    };

    public SwiftCsvReader(TextReader reader)
        : base(
#pragma warning disable CA2000 // Dispose objects before losing scope - justification: disposed by inner parser.
            new TransposingCsvTextReader(reader, CreateConfiguration()),
#pragma warning restore CA2000 // Dispose objects before losing scope
            CreateConfiguration()
        )
    {
        TypeConverterOptionsCache typeConverterOptions = Parser.Context.TypeConverterOptionsCache;

        List<string> stringNullValues = typeConverterOptions.GetOptions<string>().NullValues;
        stringNullValues.Add("");
        stringNullValues.Add("N/A");

        Parser.Context.TypeConverterCache.AddConverter<int>(new PatchInt32Converter());
        Parser.Context.TypeConverterCache.AddConverter<Position>(new PositionConverter());
        typeConverterOptions.AddOptions<Position?>(new TypeConverterOptions { NullValues = { "", "N/A" } });
    }

    public override IEnumerable<T> GetRecords<T>()
    {
        IEnumerable<T> insertRecords = [];
        if (typeof(T) == typeof(SwiftCsvRecord))
        {
            insertRecords = typeof(SwiftCsvRecord).Assembly.GetTypes()
                .Where(t => t != typeof(SwiftCsvRecord) && typeof(SwiftCsvRecord).IsAssignableFrom(t))
                .Select(Activator.CreateInstance)
                .Cast<T>();
        }

        return base.GetRecords<T>().Concat(insertRecords);
    }
}
