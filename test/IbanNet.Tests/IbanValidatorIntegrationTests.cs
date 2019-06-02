using System;
using FluentAssertions;
using IbanNet.Extensions;
using IbanNet.Validation.Rules;
using NUnit.Framework;

namespace IbanNet
{
	internal abstract class IbanValidatorIntegrationTests
	{
		protected readonly IbanValidator Validator;
		protected readonly ICountryValidationSupport CountryValidationSupport;

		protected IbanValidatorIntegrationTests(IbanValidator validator)
		{
			Validator = validator;
			CountryValidationSupport = validator;
		}

		[Test]
		public void When_validating_null_value_should_not_validate()
		{
			// Act
			ValidationResult actual = Validator.Validate(null);

			// Assert
			actual.Should().BeEquivalentTo(new ValidationResult
			{
				Result = IbanValidationResult.InvalidLength,
				ValidationRuleType = typeof(NotNullOrEmptyRule)
			});
		}

		[TestCase("NL91ABNA041716430!")]
		[TestCase("NL91ABNA^417164300")]
		public void When_validating_iban_with_illegal_characters_should_not_validate(string ibanWithIllegalChars)
		{
			// Act
			ValidationResult actual = Validator.Validate(ibanWithIllegalChars);

			// Assert
			actual.Should().BeEquivalentTo(new ValidationResult
			{
				Value = ibanWithIllegalChars,
				Result = IbanValidationResult.IllegalCharacters,
				Country = CountryValidationSupport.SupportedCountries[ibanWithIllegalChars.Substring(0, 2)],
				ValidationRuleType = typeof(NoIllegalCharactersRule)
			});
		}

		[TestCase("0091ABNA0417164300", typeof(HasCountryCodeRule))]
		[TestCase("4591ABNA0417164300", typeof(HasCountryCodeRule))]
		[TestCase("#L91ABNA0417164300", typeof(NoIllegalCharactersRule))]
		public void When_validating_iban_with_illegal_country_code_should_not_validate(string ibanWithIllegalCountryCode, Type failingType)
		{
			// Act
			ValidationResult actual = Validator.Validate(ibanWithIllegalCountryCode);

			// Assert
			actual.Should().BeEquivalentTo(new ValidationResult
			{
				Value = ibanWithIllegalCountryCode,
				Result = IbanValidationResult.IllegalCharacters,
				ValidationRuleType = failingType
			});
		}

		[TestCase("NL00ABNA0417164300")]
		[TestCase("NL01ABNA0417164300")]
		[TestCase("NL99ABNA0417164300")]
		public void When_validating_iban_with_invalid_checksum_should_not_validate(string ibanWithInvalidChecksum)
		{
			// Act
			ValidationResult actual = Validator.Validate(ibanWithInvalidChecksum);

			// Assert
			actual.Should().BeEquivalentTo(new ValidationResult
			{
				Value = ibanWithInvalidChecksum,
				Result = IbanValidationResult.IllegalCharacters,
				Country = CountryValidationSupport.SupportedCountries[ibanWithInvalidChecksum.Substring(0, 2)],
				ValidationRuleType = typeof(HasIbanChecksumRule)
			});
		}


		[TestCase("NL91ABNA04171643000")]
		[TestCase("NL91ABNA041716430")]
		[TestCase("NO938601111794")]
		[TestCase("NO93860111179470")]
		public void When_validating_iban_with_incorrect_length_should_not_validate(string ibanWithIncorrectLength)
		{
			// Act
			ValidationResult actual = Validator.Validate(ibanWithIncorrectLength);

			// Assert
			actual.Should().BeEquivalentTo(new ValidationResult
			{
				Value = ibanWithIncorrectLength,
				Result = IbanValidationResult.InvalidLength,
				Country = CountryValidationSupport.SupportedCountries[ibanWithIncorrectLength.Substring(0, 2)],
				ValidationRuleType = typeof(IsValidLengthRule)
			});
		}

		[TestCase("AA91ABNA0417164300")]
		[TestCase("ZZ93860111179470")]
		public void When_validating_iban_with_unknown_country_code_should_not_validate(string ibanWithUnknownCountryCode)
		{
			// Act
			ValidationResult actual = Validator.Validate(ibanWithUnknownCountryCode);

			// Assert
			actual.Should().BeEquivalentTo(new ValidationResult
			{
				Value = ibanWithUnknownCountryCode,
				Result = IbanValidationResult.UnknownCountryCode,
				ValidationRuleType = typeof(IsValidCountryCodeRule)
			});
		}

		[TestCase("NL92ABNA0417164300")]
		[TestCase("NO9486011117947")]
		public void When_validating_tampered_iban_should_not_validate(string tamperedIban)
		{
			// Act
			ValidationResult actual = Validator.Validate(tamperedIban);

			// Assert
			actual.Should().BeEquivalentTo(new ValidationResult
			{
				Value = tamperedIban,
				Result = IbanValidationResult.InvalidCheckDigits,
				Country = CountryValidationSupport.SupportedCountries[tamperedIban.Substring(0, 2)],
				ValidationRuleType = typeof(Mod97Rule)
			});
		}

		[TestCase("NL91 ABNA 0417 1643 00")]
		[TestCase("NL91\tABNA\t0417\t1643\t00")]
		[TestCase(" NL91 ABNA041 716 4300 ")]
		public void When_iban_contains_whitespace_should_validate(string ibanWithWhitespace)
		{
			// Act
			ValidationResult actual = Validator.Validate(ibanWithWhitespace);

			// Assert
			actual.Should().BeEquivalentTo(new ValidationResult
			{
				Value = ibanWithWhitespace.StripWhitespaceOrNull(),
				Result = IbanValidationResult.Valid,
				Country = CountryValidationSupport.SupportedCountries["NL"]
			});
		}

		[TestCaseSource(typeof(IbanTestCaseData), nameof(IbanTestCaseData.GetValidIbanPerCountry))]
		public void When_validating_good_iban_should_validate(string countryCode, string iban)
		{
			var expectedResult = new ValidationResult
			{
				Value = iban,
				Result = IbanValidationResult.Valid,
				Country = CountryValidationSupport.SupportedCountries[iban.Substring(0, 2)]
			};

			// Act
			ValidationResult actual = Validator.Validate(iban);

			// Assert
			actual.Should().BeEquivalentTo(expectedResult);
		}
	}
}
