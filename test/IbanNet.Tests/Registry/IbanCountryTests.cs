namespace IbanNet.Registry;

public class IbanCountryTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("N")]
    [InlineData("NLD")]
    public void When_country_code_is_of_invalid_length_should_throw(string? twoLetterISORegionName)
    {
        // Act
        Func<IbanCountry> act = () => new IbanCountry(twoLetterISORegionName!);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(twoLetterISORegionName));
    }

    [Fact]
    public void When_country_code_is_of_valid_length_should_not_throw()
    {
        // Act
        // ReSharper disable once ObjectCreationAsStatement
        Func<IbanCountry> act = () => new IbanCountry("ZA");

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void When_country_code_is_provided_in_lowercase_should_make_it_uppercase()
    {
        // Act
        var actual = new IbanCountry("nl");

        // Assert
        actual.TwoLetterISORegionName.Should().Be("NL");
    }

    [Fact]
    public void Getters_should_return_set_properties()
    {
        const string twoLetterIsoRegionName = "nl";
        var ibanPatternDescriptor = new PatternDescriptor(NullPattern.Instance);
        var bbanPatternDescriptor = new PatternDescriptor(NullPattern.Instance);
        var bankPatternDescriptor = new PatternDescriptor(NullPattern.Instance);
        var branchPatternDescriptor = new PatternDescriptor(NullPattern.Instance);
        const string displayName = "name";
        const string domesticAccountNumberExample = "example";
        const string englishName = "english name";
        string[] includedCountries = ["AB"];
        DateTimeOffset lastUpdatedDate = DateTimeOffset.Now;
        var sepaInfo = new SepaInfo();

        // Act
        var ibanCountry = new IbanCountry(twoLetterIsoRegionName)
        {
            Iban = ibanPatternDescriptor,
            Bban = bbanPatternDescriptor,
            Bank = bankPatternDescriptor,
            Branch = branchPatternDescriptor,
            DisplayName = displayName,
            DomesticAccountNumberExample = domesticAccountNumberExample,
            EnglishName = englishName,
            IncludedCountries = includedCountries,
            LastUpdatedDate = lastUpdatedDate,
            Sepa = sepaInfo
        };

        // Assert
        ibanCountry.TwoLetterISORegionName.Should().Be(twoLetterIsoRegionName.ToUpper());
        ibanCountry.Iban.Should().BeSameAs(ibanPatternDescriptor);
        ibanCountry.Bank.Should().BeSameAs(bankPatternDescriptor);
        ibanCountry.Bban.Should().BeSameAs(bbanPatternDescriptor);
        ibanCountry.Branch.Should().BeSameAs(branchPatternDescriptor);
        ibanCountry.DisplayName.Should().Be(displayName);
        ibanCountry.DomesticAccountNumberExample.Should().Be(domesticAccountNumberExample);
        ibanCountry.EnglishName.Should().Be(englishName);
        ibanCountry.IncludedCountries.Should().BeEquivalentTo(includedCountries, opts => opts.WithStrictOrdering());
        ibanCountry.LastUpdatedDate.Should().Be(lastUpdatedDate);
        ibanCountry.Sepa.Should().BeSameAs(sepaInfo);
    }
}
