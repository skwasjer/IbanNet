using IbanNet.Registry.Swift;

namespace IbanNet.Registry
{
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
            _sut.TryGetValue(countryCode, out IbanCountry country).Should().BeTrue();
            country.Should().NotBeNull();
            country.TwoLetterISORegionName.Should().Be(countryCode.ToUpperInvariant());
        }

        public static IEnumerable<object[]> GetExpectedDefinitions()
        {
            yield return new object[]
            {
                new IbanCountry("AD")
                {
                    DisplayName = "Andorra",
                    NativeName = "Andorra",
                    EnglishName = "Andorra",
                    Iban = new IbanStructure(new IbanSwiftPattern("AD2!n4!n4!n12!c"))
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
                    LastUpdatedDate = new DateTimeOffset(2021, 3, 1, 0, 0, 0, TimeSpan.Zero)
                }
            };

            yield return new object[]
            {
                new IbanCountry("XK")
                {
                    DisplayName = "Kosovë",
                    NativeName = "Kosovë",
                    EnglishName = "Kosovo",
                    Iban = new IbanStructure(new IbanSwiftPattern("XK2!n4!n10!n2!n"))
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
                    Sepa = new SepaInfo { IsMember = false, },
                    DomesticAccountNumberExample = "1212 0123456789 06",
                    LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
                }
            };
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
    }
}
