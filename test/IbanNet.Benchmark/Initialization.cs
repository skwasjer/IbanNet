using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using IbanNet.Registry;
using IbanNet.Registry.Swift;

namespace IbanNet.Benchmark
{
    [MarkdownExporterAttribute.GitHub]
    [Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Alphabetical)]
    [MemoryDiagnoser]
    public class Initialization
    {
        [Benchmark]
#pragma warning disable CA1822 // Mark members as static
        public void Registry()
#pragma warning restore CA1822 // Mark members as static
        {
#pragma warning disable CA1806 // Do not ignore method results
            // ReSharper disable once ObjectCreationAsStatement
            new IbanRegistry
            {
                Providers = { new SwiftRegistryProvider() }
            };
#pragma warning restore CA1806 // Do not ignore method results
        }
    }
}
