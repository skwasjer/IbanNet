﻿using IbanNet.Registry.Swift;

namespace IbanNet.Registry;

public class IbanRegistryTests
{
    private readonly IbanRegistry _sut;

    public IbanRegistryTests()
    {
        _sut = new IbanRegistry
        {
            Providers =
            {
                new SwiftRegistryProvider()
    }
        };
    }

    [Fact]
    public void When_definitions_are_loaded_should_contain_exactly_n_items()
    {
        _sut.Count.Should().Be(new SwiftRegistryProvider().Count);
    }

    [Theory]
    [InlineData("nl")]
    [InlineData("GB")]
    [InlineData("fR")]
    public void Given_mixed_case_countryCode_when_trying_to_get_it_should_return(string countryCode)
    {
        _sut.TryGetValue(countryCode, out IbanCountry? country).Should().BeTrue();
        country.Should().NotBeNull();
        country!.TwoLetterISORegionName.Should().Be(countryCode.ToUpperInvariant());
    }

    [Theory]
    [ClassData(typeof(ExpectedDefinitionsSubset))]
    public void When_definitions_are_loaded_should_contain(IbanCountry expectedIbanCountry)
    {
        IbanCountry? actual = _sut.Should()
            .Contain(c => c.TwoLetterISORegionName == expectedIbanCountry.TwoLetterISORegionName)
            .Which;
        actual.Should().BeEquivalentTo(expectedIbanCountry);
        actual.Iban.Pattern.ToString().Should().Be(expectedIbanCountry.Iban.Pattern.ToString());
        actual.Bban.Pattern.ToString().Should().Be(expectedIbanCountry.Bban.Pattern.ToString());
        actual.Bank.Pattern.ToString().Should().Be(expectedIbanCountry.Bank.Pattern.ToString());
        actual.Branch.Pattern.ToString().Should().Be(expectedIbanCountry.Branch.Pattern.ToString());
    }

    [Fact]
    public void Given_that_registry_is_not_yet_hydrated_when_adding_provider_it_should_not_throw()
    {
        var sut = new IbanRegistry
        {
            Providers =
            {
                new SwiftRegistryProvider()
            }
        };

        // Act
        Action act = () => sut.Providers.Add(Substitute.For<IIbanRegistryProvider>());

        // Assert
        act.Should().NotThrow();
        sut.Providers.Count.Should().Be(2);
    }

    [Fact]
    public void Given_that_registry_has_been_hydrated_when_adding_another_provider_it_should_throw()
    {
        var sut = new IbanRegistry
        {
            Providers =
            {
                new SwiftRegistryProvider()
            }
        };

        // Act
        sut.Count.Should().BeGreaterThan(0); // Hydrate
        Action act = () => sut.Providers.Add(Substitute.For<IIbanRegistryProvider>());

        // Assert
        act.Should()
            .Throw<NotSupportedException>()
            .WithMessage("Collection is read-only.");
        sut.Providers.Should().HaveCount(1);
    }

    [Fact]
    public void Given_that_registry_has_been_hydrated_when_adding_another_provider_to_the_list_reference_it_should_not_affect_the_provider_property()
    {
        var sut = new IbanRegistry()
        {
            Providers =
            {
                new SwiftRegistryProvider()
            }
        };

        // Act
        IList<IIbanRegistryProvider> providerRef = sut.Providers;
        sut.Count.Should().BeGreaterThan(0); // Hydrate
        providerRef.Add(Substitute.For<IIbanRegistryProvider>());

        // Assert
        sut.Providers.Should().HaveCount(1);
    }

    [Fact]
    public void Registry_should_be_ordered_by_country_code()
    {
        var reg = new IbanRegistry { Providers = { new SwiftRegistryProvider() } };

        // Act
        var countryCodes = reg.Select(c => c.TwoLetterISORegionName).ToList();

        // Assert
        countryCodes.Should().BeInAscendingOrder();
    }
}
