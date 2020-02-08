using System.Collections.Generic;
using System.Linq;
using IbanNet.Registry;

namespace IbanNet.Benchmark
{
	internal static class TestSamples
	{
		public static IList<string> GetIbanSamples(int count)
		{
			List<string> examples = IbanRegistry.Default.Select(d => d.Iban.Example).ToList();

			return Enumerable.Range(0, count)
				.Select((i, index) => examples[index % examples.Count])
				.ToList();
		}
	}
}
