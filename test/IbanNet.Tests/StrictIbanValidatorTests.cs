using FluentAssertions;
using IbanNet.Validation.Results;
using NUnit.Framework;

namespace IbanNet
{
	[TestFixture]
	internal class StrictIbanValidatorTests : IbanValidatorIntegrationTests
	{
		public StrictIbanValidatorTests()
			: base(new IbanValidator())
		{
		}

		[TestCase("NL91abna0417164300", Description = "Lowercase not allowed.")]
		[TestCase("NL91ABNA041716430A", Description = "Last character should be digit.")]
		public void When_validating_iban_with_invalid_structure_should_not_validate(string ibanWithInvalidStructure)
		{
			// Act
			ValidationResult actual = Validator.Validate(ibanWithInvalidStructure);

			// Assert
			actual.Should().BeEquivalentTo(new ValidationResult
			{
				Value = ibanWithInvalidStructure.ToUpperInvariant(),
				Result = new InvalidStructureResult(),
				Country = CountryValidationSupport.SupportedCountries[ibanWithInvalidStructure.Substring(0, 2)]
			});
		}

		[Test]
		public void When_validating_iban_that_allows_lowercase_it_should_validate()
		{
			const string ibanWithLowercase = "MT84MALT011000012345mtlcast001S";

			// Act
			ValidationResult actual = Validator.Validate(ibanWithLowercase);

			// Assert
			actual.Should().BeEquivalentTo(new ValidationResult
			{
				Value = ibanWithLowercase.ToUpperInvariant(),
				Result = ValidationRuleResult.Success,
				Country = CountryValidationSupport.SupportedCountries[ibanWithLowercase.Substring(0, 2)]
			});
		}
	}
}
