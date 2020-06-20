using System;
using System.Collections.Generic;
using FluentAssertions;
using IbanNet.Validation;
using Xunit;

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
            _sut.Count.Should().Be(77);
        }

        public static IEnumerable<object[]> GetExpectedDefinitions()
        {
            var validationFactory = new SwiftStructureValidationFactory();

            yield return new object[]
            {
                new IbanCountry("AD")
                {
                    DisplayName = "Andorra",
                    EnglishName = "Andorra",
                    Iban = new IbanStructure
                    {
                        Length = 24,
                        Structure = "AD2!n4!n4!n12!c",
                        ValidationFactory = validationFactory,
                        Example = "AD1200012030200359100100",
                        EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                    },
                    Bban = new BbanStructure
                    {
                        Position = 4,
                        Length = 20,
                        Structure = "4!n4!n12!c",
                        ValidationFactory = validationFactory,
                        Example = "00012030200359100100"
                    },
                    Bank = new BankStructure
                    {
                        Position = 4,
                        Length = 4,
                        Structure = "4!n",
                        ValidationFactory = validationFactory,
                        Example = "0001",
                    },
                    Branch = new BranchStructure
                    {
                        Position = 8,
                        Length = 4,
                        Structure = "4!n",
                        ValidationFactory = validationFactory,
                        Example = "2030",
                    },
                    Sepa = new SepaInfo { IsMember = false, },
                    DomesticAccountNumberExample = "2030200359100100",
                    LastUpdatedDate = new DateTimeOffset(2009, 11, 1, 0, 0, 0, TimeSpan.Zero)
                }
            };

            yield return new object[]
            {
                new IbanCountry("XK")
                {
                    DisplayName = "Kosovo",
                    EnglishName = "Kosovo",
                    Iban = new IbanStructure
                    {
                        Length = 20,
                        Structure = "XK2!n4!n10!n2!n",
                        ValidationFactory = validationFactory,
                        Example = "XK051212012345678906",
                        EffectiveDate = new DateTimeOffset(2014, 9, 1, 0, 0, 0, TimeSpan.Zero)
                    },
                    Bban = new BbanStructure
                    {
                        Position = 4,
                        Length = 16,
                        Structure = "4!n10!n2!n",
                        ValidationFactory = validationFactory,
                        Example = "1212012345678906"
                    },
                    Bank = new BankStructure
                    {
                        Position = 4,
                        Length = 2,
                        Structure = "2!n",
                        ValidationFactory = validationFactory,
                        Example = "12",
                    },
                    Branch = new BranchStructure
                    {
                        Position = 6,
                        Length = 2,
                        Structure = "2!n",
                        ValidationFactory = validationFactory,
                        Example = "12",
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
