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
        public void Registry()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new IbanRegistry
            {
                Providers = { new SwiftRegistryProvider() }
            };
        }
    }
}
