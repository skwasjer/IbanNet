using System.Linq;
using FluentAssertions;
using IbanNet.Validation.Rules;
using NUnit.Framework;

namespace IbanNet
{
	[TestFixture]
	public class NationalCheckDigitsValidationTests
	{
		private IbanValidator _validator;

		[SetUp]
		public void SetUp()
		{
			_validator = new IbanValidator(new IbanValidatorOptions
			{
				Rules =
				{
					new HasValidNationalCheckDigits()
				}
			});
		}

		[TestCase("FR1420041010050500013M02606")]
		[TestCase("FR7630006000011234567890189")]
		[TestCase("MR1300020001010000123456753")]
		[TestCase("MC5811222000010123456789030")]
		[TestCase("IT60X0542811101000000123456")]
		[TestCase("SM86U0322509800000000270100")]
		[TestCase("NO9386011117947")]
		[TestCase("BA391290079401028494")]
		public void Given_iban_with_valid_national_check_digits_when_validating_it_should_validate(string ibanWithNationalCheckDigits)
		{
			string countryCode = ibanWithNationalCheckDigits.Substring(0, 2);

			// Act
			ValidationResult result = _validator.Validate(ibanWithNationalCheckDigits);

			// Assert
			result.Should().BeEquivalentTo(new ValidationResult
			{
				Value = ibanWithNationalCheckDigits,
				Country = _validator.SupportedCountries.First(c => c.TwoLetterISORegionName == countryCode)
			});
		}

		[TestCase("FR4120041010050500013M02605")]
		[TestCase("FR0630006000011234567890188")]
		public void Given_iban_with_invalid_national_check_digits_when_validating_it_should_not_validate(string ibanWithTamperedNationalCheckDigits)
		{
			string countryCode = ibanWithTamperedNationalCheckDigits.Substring(0, 2);

			// Act
			ValidationResult result = _validator.Validate(ibanWithTamperedNationalCheckDigits);

			// Assert
			result.Should().BeEquivalentTo(new ValidationResult
			{
				Result = IbanValidationResult.Custom,
				Value = ibanWithTamperedNationalCheckDigits,
				Country = _validator.SupportedCountries.First(c => c.TwoLetterISORegionName == countryCode),
				ValidationRuleType = typeof(HasValidNationalCheckDigits),
				ErrorMessage = "Invalid national check digits."
			});
		}
	}
}
