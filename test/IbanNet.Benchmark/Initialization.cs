using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using IbanNet.Registry;

namespace IbanNet.Benchmark
{
	[SimpleJob(RuntimeMoniker.HostProcess)]
	[MarkdownExporterAttribute.GitHub]
	[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Alphabetical)]
	[MemoryDiagnoser]
	public class Initialization
	{
		[Benchmark]
		public void Registry()
		{
			// ReSharper disable once ObjectCreationAsStatement
			new IbanRegistry();
		}
	}
}
