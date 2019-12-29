using FluentAssertions;
using IbanNet.Registry;
using IbanNet.Validation.Methods;
using NUnit.Framework;

namespace IbanNet
{
	[TestFixture]
	public class IbanValidatorOptionsTests
	{
		private IbanValidatorOptions _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new IbanValidatorOptions();
		}

		[TestCase]
		public void Registry_should_default_to_default_registry()
		{
			_sut.Registry.Should().BeSameAs(IbanRegistry.Default);
		}

		[TestCase]
		public void Validation_method_should_default_to_strict()
		{
			_sut.ValidationMethod.Should().BeOfType<StrictValidation>();
		}

		[TestCase]
		public void Rules_should_default_to_empty_list()
		{
			_sut.Rules.Should().BeEmpty();
		}
	}
}
