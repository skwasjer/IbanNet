using IbanNet.Validation.Results;

namespace IbanNet;

public class IbanValidatorIntegrationTests
{
    private readonly IbanValidator _sut = new();

    [Theory]
    [InlineData("nl91ABNA0417164300", 0)]  // Lower case country code.
    [InlineData("Nl91ABNA0417164300", 1)]  // Lower case country code.
    [InlineData("NL91ABNA041716430A", 18)] // Last character should be digit.
    [InlineData("NL91AB1A041716430A", 6)]
    public void When_validating_iban_with_invalid_structure_should_not_validate(string ibanWithInvalidStructure, int expectedErrorPos)
    {
        // Act
        ValidationResult actual = _sut.Validate(ibanWithInvalidStructure);

        // Assert
        actual.Should().BeEquivalentTo(new ValidationResult
        {
            AttemptedValue = ibanWithInvalidStructure,
            Error = new InvalidStructureResult(expectedErrorPos),
            Country = _sut.SupportedCountries[ibanWithInvalidStructure.Substring(0, 2)]
        });
    }

    [Fact]
    public void When_validating_iban_that_allows_lowercase_it_should_validate()
    {
        const string ibanWithLowercase = "MT84MALT011000012345mtlcast001S";

        // Act
        ValidationResult actual = _sut.Validate(ibanWithLowercase);

        // Assert
        actual.Should().BeEquivalentTo(new ValidationResult
        {
            AttemptedValue = ibanWithLowercase,
            Country = _sut.SupportedCountries[ibanWithLowercase.Substring(0, 2)]
        });
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void When_validating_null_or_empty_value_should_not_validate(string iban)
    {
        // Act
        ValidationResult actual = _sut.Validate(iban);

        // Assert
        actual.Should().BeEquivalentTo(new ValidationResult
        {
            AttemptedValue = iban,
            Error = new InvalidLengthResult()
        });
    }

    [Theory]
    [InlineData("NL91ABNA041716430!", 16)]
    [InlineData("NL91ABNA^417164300", 8)]
    public void When_validating_iban_with_illegal_characters_should_not_validate(string ibanWithIllegalChars, int position)
    {
        // Act
        ValidationResult actual = _sut.Validate(ibanWithIllegalChars);

        // Assert
        actual.Should().BeEquivalentTo(new ValidationResult
        {
            AttemptedValue = ibanWithIllegalChars,
            Error = new IllegalCharactersResult(position)
        });
    }

    [Theory]
    [InlineData("0091ABNA0417164300", 0)]
    [InlineData("A591ABNA0417164300", 1)]
    [InlineData("#L91ABNA0417164300", 0)]
    [InlineData("A#91ABNA0417164300", 1)]
    public void When_validating_iban_with_illegal_country_code_should_not_validate(string ibanWithIllegalCountryCode, int position)
    {
        // Act
        ValidationResult actual = _sut.Validate(ibanWithIllegalCountryCode);

        // Assert
        actual.Should().BeEquivalentTo(new ValidationResult
        {
            AttemptedValue = ibanWithIllegalCountryCode,
            Error = new IllegalCountryCodeCharactersResult(position)
        });
    }

    [Theory]
    [InlineData("NL00ABNA0417164300")]
    [InlineData("NL01ABNA0417164300")]
    [InlineData("NL99ABNA0417164300")]
    public void When_validating_iban_with_invalid_checksum_should_not_validate(string ibanWithInvalidChecksum)
    {
        // Act
        ValidationResult actual = _sut.Validate(ibanWithInvalidChecksum);

        // Assert
        actual.Should().BeEquivalentTo(new ValidationResult
        {
            AttemptedValue = ibanWithInvalidChecksum,
            Error = new IllegalCharactersResult(3)
        });
    }

    [Theory]
    [InlineData("NL91ABNA04171643000")]
    [InlineData("NL91ABNA041716430")]
    [InlineData("NO938601111794")]
    [InlineData("NO93860111179470")]
    public void When_validating_iban_with_incorrect_length_should_not_validate(string ibanWithIncorrectLength)
    {
        // Act
        ValidationResult actual = _sut.Validate(ibanWithIncorrectLength);

        // Assert
        actual.Should().BeEquivalentTo(new ValidationResult
        {
            AttemptedValue = ibanWithIncorrectLength,
            Error = new InvalidLengthResult(),
            Country = _sut.SupportedCountries[ibanWithIncorrectLength.Substring(0, 2)]
        });
    }

    [Theory]
    [InlineData("AA91ABNA0417164300")]
    [InlineData("ZZ93860111179470")]
    public void When_validating_iban_with_unknown_country_code_should_not_validate(string ibanWithUnknownCountryCode)
    {
        // Act
        ValidationResult actual = _sut.Validate(ibanWithUnknownCountryCode);

        // Assert
        actual.Should().BeEquivalentTo(new ValidationResult
        {
            AttemptedValue = ibanWithUnknownCountryCode,
            Error = new UnknownCountryCodeResult()
        });
    }

    [Theory]
    [InlineData("NL92ABNA0417164300")]
    [InlineData("NO9486011117947")]
    public void When_validating_tampered_iban_should_not_validate(string tamperedIban)
    {
        // Act
        ValidationResult actual = _sut.Validate(tamperedIban);

        // Assert
        actual.Should().BeEquivalentTo(new ValidationResult
        {
            AttemptedValue = tamperedIban,
            Error = new InvalidCheckDigitsResult(),
            Country = _sut.SupportedCountries[tamperedIban.Substring(0, 2)]
        });
    }

    [Theory]
    [InlineData("NL91 ABNA 0417 1643 00", 4, typeof(IllegalCharactersResult))]
    [InlineData("NL91\tABNA\t0417\t1643\t00", 4, typeof(IllegalCharactersResult))]
    [InlineData(" NL91 ABNA041 716 4300 ", 0, typeof(IllegalCountryCodeCharactersResult))]
    public void Given_that_iban_contains_whitespace_when_validating_it_should_fail(string ibanWithWhitespace, int expectedCharPos, Type expectedErrorType)
    {
        // Act
        ValidationResult actual = _sut.Validate(ibanWithWhitespace);

        // Assert
        actual.Should().BeEquivalentTo(new ValidationResult
        {
            AttemptedValue = ibanWithWhitespace,
            Error = (ErrorResult)Activator.CreateInstance(expectedErrorType, expectedCharPos)!
        });
    }

    [Theory]
    [MemberData(nameof(IbanTestCaseData.GetValidIbanPerCountry), MemberType = typeof(IbanTestCaseData))]
    public void When_validating_good_iban_should_validate(string _, string iban)
    {
        var expectedResult = new ValidationResult
        {
            AttemptedValue = iban,
            Country = _sut.SupportedCountries[iban.Substring(0, 2)]
        };

        // Act
        ValidationResult actual = _sut.Validate(iban);

        // Assert
        actual.Should().BeEquivalentTo(expectedResult);
    }
}
