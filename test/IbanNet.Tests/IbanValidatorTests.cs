using System.Collections;
using System.Linq;
using FluentAssertions;
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
			var actual = _validator.Validate(null);

			// Assert
			actual.Should().Be(IbanValidationResult.InvalidLength);
		}

		[TestCase("NL91ABNA041716430!")]
		[TestCase("#L91ABNA0417164300")]
		[TestCase("NL91ABNA^417164300")]
		public void When_validating_iban_with_illegal_characters_should_not_validate(string ibanWithIllegalChars)
		{
			// Act
			var actual = _validator.Validate(ibanWithIllegalChars);

			// Assert
			actual.Should().Be(IbanValidationResult.IllegalCharacters);
		}

		[TestCase("0091ABNA0417164300")]
		[TestCase("4591ABNA0417164300")]
		public void When_validating_iban_with_illegal_country_code_should_not_validate(string ibanWithIllegalCountryCode)
		{
			// Act
			var actual = _validator.Validate(ibanWithIllegalCountryCode);

			// Assert
			actual.Should().Be(IbanValidationResult.IllegalCharacters);
		}

		[TestCase("NL00ABNA0417164300")]
		[TestCase("NL01ABNA0417164300")]
		[TestCase("NL99ABNA0417164300")]
		public void When_validating_iban_with_invalid_checksum_should_not_validate(string ibanWithInvalidChecksum)
		{
			// Act
			var actual = _validator.Validate(ibanWithInvalidChecksum);

			// Assert
			actual.Should().Be(IbanValidationResult.IllegalCharacters);
		}


		[TestCase("NL91ABNA04171643000")]
		[TestCase("NL91ABNA041716430")]
		[TestCase("NO938601111794")]
		[TestCase("NO93860111179470")]
		public void When_validating_iban_with_incorrect_length_should_not_validate(string ibanWithIncorrectLength)
		{
			// Act
			var actual = _validator.Validate(ibanWithIncorrectLength);

			// Assert
			actual.Should().Be(IbanValidationResult.InvalidLength);
		}

		[TestCase("AA91ABNA0417164300")]
		[TestCase("ZZ93860111179470")]
		public void When_validating_iban_with_unknown_country_code_should_not_validate(string ibanWithUnknownCountryCode)
		{
			// Act
			var actual = _validator.Validate(ibanWithUnknownCountryCode);

			// Assert
			actual.Should().Be(IbanValidationResult.UnknownCountryCode);
		}

		[TestCase("NL91ABNA041716430A")]
		[TestCase("NO938601111794A")]
		public void When_validating_iban_with_invalid_structure_should_not_validate(string ibanWithInvalidStructure)
		{
			// Act
			var actual = _validator.Validate(ibanWithInvalidStructure);

			// Assert
			actual.Should().Be(IbanValidationResult.InvalidStructure);
		}

		[TestCase("NL92ABNA0417164300")]
		[TestCase("NO9486011117947")]
		public void When_validating_tampered_iban_should_not_validate(string tamperedIban)
		{
			// Act
			var actual = _validator.Validate(tamperedIban);

			// Assert
			actual.Should().Be(IbanValidationResult.InvalidCheckDigits);
		}

		private static IEnumerable GetAllValidSamples()
		{
			return new IbanDefinitions().Select(d => new TestCaseData(d.Key, d.Value.Example));
		}

		[TestCaseSource(nameof(GetAllValidSamples))]
		public void When_validating_good_iban_should_validate(string countryCode, string iban)
		{
			// Act
			var actual = _validator.Validate(iban);

			// Assert
			actual.Should().Be(IbanValidationResult.Valid);
		}
	}
}
