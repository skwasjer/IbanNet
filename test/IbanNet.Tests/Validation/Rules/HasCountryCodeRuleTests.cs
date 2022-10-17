using IbanNet.Validation.Results;

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
        [InlineData("", -1)]
        [InlineData("N", 1)]
        [InlineData("@#", 0)]
        [InlineData("N#", 1)]
        public void Given_invalid_value_when_validating_it_should_return_error(string value, int position)
        {
            ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext(value));

            actual.Should()
                .BeOfType<IllegalCountryCodeCharactersResult>()
                .Which.Position.Should()
                .Be(position);
        }

        [Fact]
        public void Given_valid_value_when_validating_it_should_return_success()
        {
            ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext("XX"));

            actual.Should().Be(ValidationRuleResult.Success);
        }
    }
}
