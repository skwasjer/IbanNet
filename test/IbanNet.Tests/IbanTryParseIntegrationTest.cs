using System;
using FluentAssertions;
using NUnit.Framework;

namespace IbanNet
{
	[TestFixture]
	internal class IbanParseIntegrationTest
	{
		public IbanParseIntegrationTest()
		{
			Iban.Validator = new IbanValidator();
		}

		[TestCase(null, typeof(ArgumentNullException))]
		[TestCase("", typeof(IbanFormatException))]
		[TestCase("AD12000120359100100", typeof(IbanFormatException))]
		[TestCase("Invalid", typeof(IbanFormatException))]
		public void Given_invalid_value_when_parsing_it_should_throw(string attemptedIbanValue, Type expectedExceptionType)
		{
			// Act
			Action act = () => Iban.Parse(attemptedIbanValue);

			// Assert
			act.Should().Throw<Exception>()
				.Which.Should().BeOfType(expectedExceptionType);
		}

		[TestCase(null)]
		[TestCase("")]
		[TestCase("AD12000120359100100")]
		[TestCase("Invalid")]
		public void Given_invalid_value_when_trying_parsing_it_should_throw(string attemptedIbanValue)
		{
			// Act
			Func<bool> act = () => Iban.TryParse(attemptedIbanValue, out _);

			// Assert
			act.Should().NotThrow().Which.Should().BeFalse();
		}

		[TestCase("AD1200012030200359100100")] // Valid
		[TestCase("AD12000120359100100")] // Invalid
		public void Given_iban_when_parsing_should_give_same_result_as_validator(string attemptedIbanValue)
		{
			// Act
			ValidationResult validatorResult = Iban.Validator.Validate(attemptedIbanValue);
			bool tryParseResult = Iban.TryParse(attemptedIbanValue, out Iban iban, out ValidationResult tryParseValidationResult, out Exception _);

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
