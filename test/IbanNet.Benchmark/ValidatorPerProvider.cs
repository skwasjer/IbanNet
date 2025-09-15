using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using IbanNet.Registry;
using IbanNet.Registry.Swift;
using IbanNet.Registry.Wikipedia;

namespace IbanNet.Benchmark;

[MarkdownExporterAttribute.GitHub]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Alphabetical)]
[MemoryDiagnoser]
public class ValidatorPerProvider
{
    public static IEnumerable<object> GetProviders()
    {
        yield return new Args(new SwiftRegistryProvider());
        yield return new Args(new WikipediaRegistryProvider());
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetProviders))]
    public void Validate(Args args)
    {
        _ = args.Validator.Validate("NL91ABNA0417164300");
    }

    public sealed class Args(IIbanRegistryProvider provider)
    {
        public IIbanRegistryProvider Provider { get; } = provider;
        public IbanValidator Validator { get; } = new(new IbanRegistry { Providers = { provider } });

        public override string ToString()
        {
            return Provider.GetType().Name.Replace("RegistryProvider", string.Empty);
        }
    }
}
