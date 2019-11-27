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

		[TestCaseSource(typeof(IbanTestCaseData), nameof(IbanTestCaseData.GetInvalidIbanPerCountry))]
		public void Given_a_model_with_invalid_iban_when_parsing_should_give_same_result_as_validator(string countryCode, string attemptedIbanValue)
		{
			// Act
			ValidationResult validatorResult = Iban.Validator.Validate(attemptedIbanValue);
			bool tryParseResult = Iban.TryParse(attemptedIbanValue, out Iban iban, out IbanValidationResult validationResult);

			// Assert
			validatorResult.IsValid.Should().Be(tryParseResult);
			validatorResult.Result.Should().Be(validationResult);
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
