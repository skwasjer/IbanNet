using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using IbanNet.Registry;
using IbanNet.Registry.Swift;
using IbanNet.Registry.Wikipedia;

namespace IbanNet.Benchmark;

[MarkdownExporterAttribute.GitHub]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Alphabetical)]
[MemoryDiagnoser]
public class RegistryLookup
{
    public static IEnumerable<object> GetProviders()
    {
        yield return new Args(new SwiftRegistryProvider(), false);
        yield return new Args(new SwiftRegistryProvider(), true);
        yield return new Args(new WikipediaRegistryProvider(), false);
        yield return new Args(new WikipediaRegistryProvider(), true);
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetProviders))]
    public void Lookup(Args args)
    {
        args.Registry.TryGetValue("NL", out IbanCountry _);
    }

    public sealed class Args
    {
        private readonly IIbanRegistryProvider _provider;
        private readonly bool _isHydrated;
        private readonly IbanRegistry _registry;

        public Args(IIbanRegistryProvider provider, bool isHydrated)
        {
            _provider = provider;
            _isHydrated = isHydrated;

            if (!_isHydrated)
            {
                return;
            }

            _registry = new IbanRegistry { Providers = { _provider } };
            _ = _registry.Count;
        }

        public IbanRegistry Registry => _isHydrated
            ? _registry
            : new IbanRegistry { Providers = { _provider } };

        public override string ToString()
        {
            return _provider.GetType().Name.Replace("RegistryProvider", string.Empty) + ", " + (_isHydrated ? "warm" : "cold");
        }
    }
}
