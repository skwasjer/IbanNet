using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using IbanNet.Validation.Methods;

namespace IbanNet.Benchmark
{
	[SimpleJob(RuntimeMoniker.HostProcess)]
	[MarkdownExporterAttribute.GitHub]
	[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Alphabetical)]
	[MemoryDiagnoser]
	public class RunOnce
	{
		private IIbanValidator _strictValidator, _fastValidator;
		private IList<string> _testData;

		[GlobalSetup]
		public void GlobalSetup()
		{
			// IbanNet setup
			_strictValidator = new IbanValidator();
			_fastValidator = new IbanValidator(new IbanValidatorOptions { ValidationMethod = new FastValidation() });

			// IbanValidation setup
			new IbanValidation.IbanValidator();

			_testData = TestSamples.GetIbanSamples(1);
		}

		public IEnumerable<ValidatorCase> ValidateCases
		{
			get
			{
				yield return new ValidatorCase("IbanNet Strict", s => _strictValidator.Validate(s));
				yield return new ValidatorCase("IbanNet Fast", s => _fastValidator.Validate(s));
			}
		}

		[Benchmark]
		[ArgumentsSource(nameof(ValidateCases))]
		public void Validate(ValidatorCase validator)
		{
			validator.Validate(_testData[0]);
		}
	}
}
