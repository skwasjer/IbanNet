using FluentAssertions;
using IbanNet.Registry;
using IbanNet.Validation.Results;
using NUnit.Framework;

namespace IbanNet.Validation.Rules
{
	[TestFixture]
	internal class IsValidCountryCodeRuleTests
	{
		private readonly IsValidCountryCodeRule _sut;

		public IsValidCountryCodeRuleTests()
		{
			_sut = new IsValidCountryCodeRule();
		}

		[Test]
		public void Given_no_country_instance_when_validating_it_should_return_error()
		{
			ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext(string.Empty, null));

			actual.Should().BeOfType<UnknownCountryCodeResult>();
		}

		[Test]
		public void Given_valid_value_when_validating_it_should_return_success()
		{
			ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext("XX", new CountryInfo("XX")));

			actual.Should().Be(ValidationRuleResult.Success);
		}
	}
}
