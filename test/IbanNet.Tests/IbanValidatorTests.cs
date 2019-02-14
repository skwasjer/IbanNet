using System.Collections;
using System.Linq;
using FluentAssertions;
using IbanNet.Registry;
using NUnit.Framework;

namespace IbanNet
{
	[TestFixture]
	internal class IbanValidatorTests
	{
		private IbanValidator _validator;

		[SetUp]
		public void SetUp()
		{
			_validator = new IbanValidator();
		}

		[Test]
		public void When_validating_null_value_should_not_validate()
		{
			// Act
			ValidationResult actual = _validator.Validate(null);

			// Assert
			actual.Should().BeEquivalentTo(new ValidationResult
			{
				Result = IbanValidationResult.InvalidLength
			});
		}

		[TestCase("NL91ABNA041716430!")]
		[TestCase("NL91ABNA^417164300")]
		public void When_validating_iban_with_illegal_characters_should_not_validate(string ibanWithIllegalChars)
		{
			// Act
			ValidationResult actual = _validator.Validate(ibanWithIllegalChars);

			// Assert
			actual.Should().BeEquivalentTo(new ValidationResult
			{
				Value = ibanWithIllegalChars,
				Result = IbanValidationResult.IllegalCharacters,
				Country = _validator.SupportedCountries
					.Single(c => c.TwoLetterISORegionName == ibanWithIllegalChars.Substring(0, 2))
			});
		}

		[TestCase("0091ABNA0417164300")]
		[TestCase("4591ABNA0417164300")]
		[TestCase("#L91ABNA0417164300")]
		public void When_validating_iban_with_illegal_country_code_should_not_validate(string ibanWithIllegalCountryCode)
		{
			// Act
			ValidationResult actual = _validator.Validate(ibanWithIllegalCountryCode);

			// Assert
			actual.Should().BeEquivalentTo(new ValidationResult
			{
				Value = ibanWithIllegalCountryCode,
				Result = IbanValidationResult.IllegalCharacters
			});
		}

		[TestCase("NL00ABNA0417164300")]
		[TestCase("NL01ABNA0417164300")]
		[TestCase("NL99ABNA0417164300")]
		public void When_validating_iban_with_invalid_checksum_should_not_validate(string ibanWithInvalidChecksum)
		{
			// Act
			ValidationResult actual = _validator.Validate(ibanWithInvalidChecksum);

			// Assert
			actual.Should().BeEquivalentTo(new ValidationResult
			{
				Value = ibanWithInvalidChecksum,
				Result = IbanValidationResult.IllegalCharacters,
				Country = _validator.SupportedCountries
					.Single(c => c.TwoLetterISORegionName == ibanWithInvalidChecksum.Substring(0, 2))
			});
		}


		[TestCase("NL91ABNA04171643000")]
		[TestCase("NL91ABNA041716430")]
		[TestCase("NO938601111794")]
		[TestCase("NO93860111179470")]
		public void When_validating_iban_with_incorrect_length_should_not_validate(string ibanWithIncorrectLength)
		{
			// Act
			ValidationResult actual = _validator.Validate(ibanWithIncorrectLength);

			// Assert
			actual.Should().BeEquivalentTo(new ValidationResult
			{
				Value = ibanWithIncorrectLength,
				Result = IbanValidationResult.InvalidLength,
				Country = _validator.SupportedCountries
					.Single(c => c.TwoLetterISORegionName == ibanWithIncorrectLength.Substring(0, 2))
			});
		}

		[TestCase("AA91ABNA0417164300")]
		[TestCase("ZZ93860111179470")]
		public void When_validating_iban_with_unknown_country_code_should_not_validate(string ibanWithUnknownCountryCode)
		{
			// Act
			ValidationResult actual = _validator.Validate(ibanWithUnknownCountryCode);

			// Assert
			actual.Should().BeEquivalentTo(new ValidationResult
			{
				Value = ibanWithUnknownCountryCode,
				Result = IbanValidationResult.UnknownCountryCode
			});
		}

		[TestCase("NL91ABNA041716430A")]
		[TestCase("NO938601111794A")]
		public void When_validating_iban_with_invalid_structure_should_not_validate(string ibanWithInvalidStructure)
		{
			// Act
			ValidationResult actual = _validator.Validate(ibanWithInvalidStructure);

			// Assert
			actual.Should().BeEquivalentTo(new ValidationResult
			{
				Value = ibanWithInvalidStructure,
				Result = IbanValidationResult.InvalidStructure,
				Country = _validator.SupportedCountries
					.Single(c => c.TwoLetterISORegionName == ibanWithInvalidStructure.Substring(0, 2))
			});
		}

		[TestCase("NL92ABNA0417164300")]
		[TestCase("NO9486011117947")]
		public void When_validating_tampered_iban_should_not_validate(string tamperedIban)
		{
			// Act
			ValidationResult actual = _validator.Validate(tamperedIban);

			// Assert
			actual.Should().BeEquivalentTo(new ValidationResult
			{
				Value = tamperedIban,
				Result = IbanValidationResult.InvalidCheckDigits,
				Country = _validator.SupportedCountries
					.Single(c => c.TwoLetterISORegionName == tamperedIban.Substring(0, 2))
			});
		}

		private static IEnumerable GetAllValidSamples()
		{
			return new IbanRegistry()
				.Select(d => new TestCaseData(d.TwoLetterISORegionName, d.Iban.Example));
		}

		[TestCaseSource(nameof(GetAllValidSamples))]
		public void When_validating_good_iban_should_validate(string countryCode, string iban)
		{
			var expectedResult = new ValidationResult
			{
				Value = iban,
				Result = IbanValidationResult.Valid,
				Country = _validator.SupportedCountries
					.FirstOrDefault(c => c.TwoLetterISORegionName == iban.Substring(0, 2))
			};

			// Act
			ValidationResult actual = _validator.Validate(iban);

			// Assert
			actual.Should().BeEquivalentTo(expectedResult);
		}
	}
}
