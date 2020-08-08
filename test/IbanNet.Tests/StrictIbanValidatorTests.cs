﻿using FluentAssertions;
using IbanNet.Validation.Results;
using Xunit;

namespace IbanNet
{
    public class StrictIbanValidatorTests : IbanValidatorIntegrationTests
    {
        public StrictIbanValidatorTests()
            : base(new IbanValidator())
        {
        }

        [Fact]
        public void When_validating_iban_with_invalid_structure_should_not_validate()
        {
            const string ibanWithInvalidStructure = "NL91ABNA041716430A"; // Last character should be digit.

            // Act
            ValidationResult actual = Validator.Validate(ibanWithInvalidStructure);

            // Assert
            actual.Should().BeEquivalentTo(new ValidationResult
            {
                AttemptedValue = ibanWithInvalidStructure,
                Error = new InvalidStructureResult(),
                Country = Validator.SupportedCountries[ibanWithInvalidStructure.Substring(0, 2)]
            });
        }

        [Fact]
        public void When_validating_iban_that_allows_lowercase_it_should_validate()
        {
            const string ibanWithLowercase = "MT84MALT011000012345mtlcast001S";

            // Act
            ValidationResult actual = Validator.Validate(ibanWithLowercase);

            // Assert
            actual.Should().BeEquivalentTo(new ValidationResult
            {
                AttemptedValue = ibanWithLowercase.ToUpperInvariant(),
                Country = Validator.SupportedCountries[ibanWithLowercase.Substring(0, 2)]
            });
        }
    }
}
