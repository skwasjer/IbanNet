using System;
using FluentAssertions;
using Xunit;

namespace IbanNet
{
    public class IbanParserTests
    {
        private readonly IbanValidator _ibanValidator;
        private readonly IbanParser _sut;

        public IbanParserTests()
        {
            _sut = new IbanParser(_ibanValidator = new IbanValidator());
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(IbanFormatException))]
        [InlineData("AD12000120359100100", typeof(IbanFormatException))]
        [InlineData("Invalid", typeof(IbanFormatException))]
        public void Given_invalid_value_when_parsing_it_should_throw(string attemptedIbanValue, Type expectedExceptionType)
        {
            // Act
            Action act = () => _sut.Parse(attemptedIbanValue);

            // Assert
            act.Should()
                .Throw<Exception>()
                .Which.Should()
                .BeOfType(expectedExceptionType);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("AD12000120359100100")]
        [InlineData("Invalid")]
        public void Given_invalid_value_when_trying_parsing_it_should_not_throw_and_return_false(string attemptedIbanValue)
        {
            // Act
            Func<bool> act = () => _sut.TryParse(attemptedIbanValue, out _);

            // Assert
            act.Should().NotThrow().Which.Should().BeFalse();
        }

        [Theory]
        [InlineData("AD1200012030200359100100")] // Valid
        [InlineData("AD12000120359100100")] // Invalid
        public void Given_iban_when_parsing_should_give_same_result_as_validator(string attemptedIbanValue)
        {
            // Act
            ValidationResult validatorResult = _ibanValidator.Validate(attemptedIbanValue);
            bool tryParseResult = _sut.TryParse(attemptedIbanValue, out Iban iban, out ValidationResult tryParseValidationResult, out Exception _);

            // Assert
            validatorResult.IsValid.Should().Be(tryParseResult);
            validatorResult.Should().BeEquivalentTo(tryParseValidationResult);
            if (tryParseResult)
            {
                iban.Should().NotBeNull();
            }
            else
            {
                iban.Should().BeNull();
            }
        }
    }
}
