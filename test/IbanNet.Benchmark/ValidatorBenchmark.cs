using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
#if VALIDATOR_COMPARISONS
using SinKien.IBAN4Net;
#endif

namespace IbanNet.Benchmark
{
    [MarkdownExporterAttribute.GitHub]
    [Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Alphabetical)]
    [MemoryDiagnoser]
    public class ValidatorBenchmark
    {
        private IIbanValidator _validator;
#if VALIDATOR_COMPARISONS
        private IbanValidation.IbanValidator _nugetIbanValidator;
#endif
        private IList<string> _testData;

        [Params(10000)]
        public int Count { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            // IbanNet setup
            _validator = new IbanValidator();

#if VALIDATOR_COMPARISONS
            _nugetIbanValidator = new IbanValidation.IbanValidator();
#endif

            _testData = TestSamples.GetIbanSamples(Count);
        }

        [Benchmark(Baseline = true)]
        public void Singleton()
        {
            // ReSharper disable once ForCanBeConvertedToForeach
            for (int i = 0; i < _testData.Count; i++)
            {
                _validator.Validate(_testData[i]);
            }
        }

        /// <summary>
        /// Not recommended: to show the difference.
        /// </summary>
        [Benchmark]
        public void Transient()
        {
            // ReSharper disable once ForCanBeConvertedToForeach
            for (int i = 0; i < _testData.Count; i++)
            {
                var validator = new IbanValidator();
                validator.Validate(_testData[i]);
            }
        }

        [Benchmark]
        public void Singleton_CacheReuse()
        {
            // ReSharper disable once ForCanBeConvertedToForeach
            for (int i = 0; i < _testData.Count; i++)
            {
                // Validate same IBAN to hit structure validator cache.
                _validator.Validate(_testData[0]);
            }
        }

#if VALIDATOR_COMPARISONS
        [Benchmark]
        public void NuGet_IbanValidator()
        {
            // ReSharper disable once ForCanBeConvertedToForeach
            for (int i = 0; i < _testData.Count; i++)
            {
                _nugetIbanValidator.Validate(_testData[i]);
            }
        }

        [Benchmark]
        public void NuGet_IBAN4NET()
        {
            // ReSharper disable once ForCanBeConvertedToForeach
            for (int i = 0; i < _testData.Count; i++)
            {
                IbanUtils.IsValid(_testData[i], out IbanFormatViolation _);
            }
        }
#endif
    }
}
