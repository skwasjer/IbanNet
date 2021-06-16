using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace IbanNet.Benchmark
{
    [MarkdownExporterAttribute.GitHub]
    [Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Alphabetical)]
    [MemoryDiagnoser]
    public class RunOnce
    {
        private IIbanValidator _validator;
        private IList<string> _testData;

        [GlobalSetup]
        public void GlobalSetup()
        {
            // IbanNet setup
            _validator = new IbanValidator();

            _testData = TestSamples.GetIbanSamples(1);
        }

        [Benchmark]
        public void Validate()
        {
            _validator.Validate(_testData[0]);
        }
    }
}
