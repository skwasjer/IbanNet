using FluentAssertions;
using IbanNet.Validation.Results;
using Xunit;

namespace IbanNet.Validation.Rules
{
	public class HasCountryCodeRuleTests
	{
		private readonly HasCountryCodeRule _sut;

		public HasCountryCodeRuleTests()
		{
			_sut = new HasCountryCodeRule();
		}

		[Theory]
		[InlineData("")]
		[InlineData("N")]
		[InlineData("@#")]
		public void Given_invalid_value_when_validating_it_should_return_error(string value)
		{
			ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext(value));

			actual.Should().BeOfType<IllegalCountryCodeCharactersResult>();
		}

		[Fact]
		public void Given_valid_value_when_validating_it_should_return_success()
		{
			ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext("XX"));

			actual.Should().Be(ValidationRuleResult.Success);
		}
	}
}
