using FluentAssertions;
using IbanNet.Validation.Results;
using NUnit.Framework;

namespace IbanNet.Validation.Rules
{
	[TestFixture]
	internal class HasCountryCodeRuleTests
	{
		private readonly HasCountryCodeRule _sut;

		public HasCountryCodeRuleTests()
		{
			_sut = new HasCountryCodeRule();
		}

		[TestCase("")]
		[TestCase("N")]
		[TestCase("@#")]
		public void Given_invalid_value_when_validating_it_should_return_error(string value)
		{
			ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext(value, null));

			actual.Should().BeOfType<IllegalCharactersResult>();
		}

		[Test]
		public void Given_valid_value_when_validating_it_should_return_success()
		{
			ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext("XX", null));

			actual.Should().Be(ValidationRuleResult.Success);
		}
	}
}
