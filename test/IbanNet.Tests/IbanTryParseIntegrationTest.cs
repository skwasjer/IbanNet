using System;
using FluentAssertions;
using NUnit.Framework;

namespace IbanNet
{
	[TestFixture]
	internal class IbanTryParseIntegrationTest
	{
		public IbanTryParseIntegrationTest()
		{
			Iban.Validator = new IbanValidator();
		}

		[TestCase("AD1200012030200359100100")] // Valid
		[TestCase("AD12000120359100100")] // Invalid
		public void Given_a_model_with_iban_when_parsing_should_give_same_result_as_validator(string attemptedIbanValue)
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
