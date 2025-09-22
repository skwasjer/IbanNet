using IbanNet.Registry.Swift;

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

    public static IEnumerable<object[]> GetExpectedDefinitions()
    {
        yield return
        [
            new IbanCountry("AD")
            {
                DisplayName = "Andorra",
                NativeName = "Andorra",
                EnglishName = "Andorra",
                Iban = new IbanStructure(new SwiftPattern("AD2!n4!n4!n12!c"))
                {
                    Example = "AD1200012030200359100100",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!n4!n12!c"), 4)
                {
                    Example = "00012030200359100100"
                },
                Bank = new BankStructure(new SwiftPattern("4!n"), 4)
                {
                    Example = "0001"
                },
                Branch = new BranchStructure(new SwiftPattern("4!n"), 8)
                {
                    Example = "2030"
                },
                Sepa = new SepaInfo { IsMember = true },
                DomesticAccountNumberExample = "2030200359100100",
                LastUpdatedDate = new DateTimeOffset(2021, 3, 1, 0, 0, 0, TimeSpan.Zero),
                EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
            }
        ];

        yield return
        [
            new IbanCountry("XK")
            {
                DisplayName = "Kosovë",
                NativeName = "Kosovë",
                EnglishName = "Kosovo",
                Iban = new IbanStructure(new SwiftPattern("XK2!n4!n10!n2!n"))
                {
                    Example = "XK051212012345678906",
                    EffectiveDate = new DateTimeOffset(2014, 9, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!n10!n2!n"), 4)
                {
                    Example = "1212012345678906"
                },
                Bank = new BankStructure(new SwiftPattern("2!n"), 4)
                {
                    Example = "12"
                },
                Branch = new BranchStructure(new SwiftPattern("2!n"), 6)
                {
                    Example = "12"
                },
                Sepa = new SepaInfo { IsMember = false },
                DomesticAccountNumberExample = "1212 0123456789 06",
                LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero),
                EffectiveDate = new DateTimeOffset(2014, 9, 1, 0, 0, 0, TimeSpan.Zero)
            }
        ];
    }

    [Theory]
    [MemberData(nameof(GetExpectedDefinitions))]
    public void When_definitions_are_loaded_should_contain(IbanCountry expectedIbanCountry)
    {
        _sut.Should()
            .Contain(c => c.TwoLetterISORegionName == expectedIbanCountry.TwoLetterISORegionName)
            .Which
            .Should()
            .BeEquivalentTo(expectedIbanCountry);
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
        act.Should().Throw<NotSupportedException>()
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
