using FluentAssertions;
using IbanNet.Registry;
using IbanNet.Registry.Patterns;
using IbanNet.Validation.Results;
using Xunit;

namespace IbanNet.Validation.Rules
{
    public class IsValidLengthRuleTests
    {
        private readonly IsValidLengthRule _sut;

        public IsValidLengthRuleTests()
        {
            _sut = new IsValidLengthRule();
        }

        [Theory]
        [InlineData(9)]
        [InlineData(11)]
        public void Given_value_of_invalid_length_when_validating_it_should_return_error(int count)
        {
            string value = new('0', count);
            var context = new ValidationRuleContext(value)
            {
                Country = new IbanCountry("XX")
                {
                    Iban = new IbanStructure(new FakePattern(new [] { new PatternToken(AsciiCategory.Digit, 10) }))
                }
            };

            // Act
            ValidationRuleResult actual = _sut.Validate(context);

            // Assert
            actual.Should().BeOfType<InvalidLengthResult>();
        }

        [Fact]
        public void Given_value_of_valid_length_when_validating_it_should_return_success()
        {
            string value = new('0', 10);
            var context = new ValidationRuleContext(value)
            {
                Country = new IbanCountry("XX")
                {
                    Iban = new IbanStructure(new FakePattern(new [] { new PatternToken(AsciiCategory.Digit, 10) }))
                }
            };

            // Act
            ValidationRuleResult actual = _sut.Validate(context);

            // Assert
            actual.Should().Be(ValidationRuleResult.Success);
        }

        [Fact]
        public void Given_country_info_is_null_when_validating_it_should_return_error()
        {
            var context = new ValidationRuleContext(string.Empty)
            {
                Country = null
            };

            // Act
            ValidationRuleResult actual = _sut.Validate(context);

            // Assert
            actual.Should().BeOfType<InvalidLengthResult>();
        }
    }
}
