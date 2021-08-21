using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using IbanNet.Registry.Patterns;

namespace IbanNet.Registry.Swift
{
    /// <summary>
    /// This IBAN registry provider contains IBAN/BBAN/SEPA information for all known IBAN countries.
    /// </summary>
    /// <remarks>
    /// Generated from: swift_iban_registry_202106.r90.txt
    /// </remarks>
    [GeneratedCode("SwiftRegistryProviderT4", "1.1-r90")]
    public class SwiftRegistryProvider : IIbanRegistryProvider
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ICollection<IbanCountry>? _countries;

        /// <inheritdoc />
        public IEnumerator<IbanCountry> GetEnumerator()
        {
            _countries = _countries ??= Load().ToList();

            return _countries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        // ReSharper disable once UseCollectionCountProperty - justification: need to init _countries first.
        public int Count => _countries?.Count ?? this.Count();

        private static IEnumerable<IbanCountry> Load()
        {
            // ReSharper disable CommentTypo
            // ReSharper disable StringLiteralTypo

            // Andorra
            yield return new IbanCountry("AD")
            {
                DisplayName = "Andorra",
                EnglishName = "Andorra",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 4, 4),
                    new(AsciiCategory.Digit, 4, 4),
                    new(AsciiCategory.AlphaNumeric, 12, 12),
                }))
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
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "2030200359100100",
                LastUpdatedDate = new DateTimeOffset(2021, 3, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // United Arab Emirates (The)
            yield return new IbanCountry("AE")
            {
                DisplayName = "United Arab Emirates (The)",
                EnglishName = "United Arab Emirates (The)",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 3, 3),
                    new(AsciiCategory.Digit, 16, 16),
                }))
                {
                    Example = "AE070331234567890123456",
                    EffectiveDate = new DateTimeOffset(2011, 10, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("3!n16!n"), 4)
                {
                    Example = "0331234567890123456"
                },
                Bank = new BankStructure(new SwiftPattern("3!n"), 4)
                {
                    Example = "033"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "1234567890123456",
                LastUpdatedDate = new DateTimeOffset(2015, 2, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Albania
            yield return new IbanCountry("AL")
            {
                DisplayName = "Albania",
                EnglishName = "Albania",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 8, 8),
                    new(AsciiCategory.AlphaNumeric, 16, 16),
                }))
                {
                    Example = "AL47212110090000000235698741",
                    EffectiveDate = new DateTimeOffset(2009, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("8!n16!c"), 4)
                {
                    Example = "212110090000000235698741"
                },
                Bank = new BankStructure(new SwiftPattern("8!n"), 4)
                {
                    Example = "21211009"
                },
                Branch = new BranchStructure(new SwiftPattern("5!n"), 7)
                {
                    Example = "1100"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "0000000235698741",
                LastUpdatedDate = new DateTimeOffset(2011, 4, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Austria
            yield return new IbanCountry("AT")
            {
                DisplayName = "Austria",
                EnglishName = "Austria",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 5, 5),
                    new(AsciiCategory.Digit, 11, 11),
                }))
                {
                    Example = "AT611904300234573201",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("5!n11!n"), 4)
                {
                    Example = "1904300234573201"
                },
                Bank = new BankStructure(new SwiftPattern("5!n"), 4)
                {
                    Example = "19043"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "BLZ 19043 Kto 234573201",
                LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Azerbaijan
            yield return new IbanCountry("AZ")
            {
                DisplayName = "Azerbaijan",
                EnglishName = "Azerbaijan",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.UppercaseLetter, 4, 4),
                    new(AsciiCategory.AlphaNumeric, 20, 20),
                }))
                {
                    Example = "AZ21NABZ00000000137010001944",
                    EffectiveDate = new DateTimeOffset(2013, 1, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!a20!c"), 4)
                {
                    Example = "NABZ00000000137010001944"
                },
                Bank = new BankStructure(new SwiftPattern("4!a"), 4)
                {
                    Example = "NABZ"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "NABZ00000000137010001944",
                LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Bosnia and Herzegovina
            yield return new IbanCountry("BA")
            {
                DisplayName = "Bosnia and Herzegovina",
                EnglishName = "Bosnia and Herzegovina",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 3, 3),
                    new(AsciiCategory.Digit, 3, 3),
                    new(AsciiCategory.Digit, 8, 8),
                    new(AsciiCategory.Digit, 2, 2),
                }))
                {
                    Example = "BA391290079401028494",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("3!n3!n8!n2!n"), 4)
                {
                    Example = "1990440001200279"
                },
                Bank = new BankStructure(new SwiftPattern("3!n"), 4)
                {
                    Example = "199"
                },
                Branch = new BranchStructure(new SwiftPattern("3!n"), 7)
                {
                    Example = "044"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "199-044-00012002-79",
                LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Belgium
            yield return new IbanCountry("BE")
            {
                DisplayName = "Belgium",
                EnglishName = "Belgium",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 3, 3),
                    new(AsciiCategory.Digit, 7, 7),
                    new(AsciiCategory.Digit, 2, 2),
                }))
                {
                    Example = "BE68539007547034",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("3!n7!n2!n"), 4)
                {
                    Example = "539007547034"
                },
                Bank = new BankStructure(new SwiftPattern("3!n"), 4)
                {
                    Example = "539"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "BE68 5390 0754 7034",
                LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Bulgaria
            yield return new IbanCountry("BG")
            {
                DisplayName = "Bulgaria",
                EnglishName = "Bulgaria",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.UppercaseLetter, 4, 4),
                    new(AsciiCategory.Digit, 4, 4),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.AlphaNumeric, 8, 8),
                }))
                {
                    Example = "BG80BNBG96611020345678",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!a4!n2!n8!c"), 4)
                {
                    Example = "BNBG96611020345678"
                },
                Bank = new BankStructure(new SwiftPattern("4!a"), 4)
                {
                    Example = "BNBG"
                },
                Branch = new BranchStructure(new SwiftPattern("4!n"), 8)
                {
                    Example = "9661"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "",
                LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Bahrain
            yield return new IbanCountry("BH")
            {
                DisplayName = "Bahrain",
                EnglishName = "Bahrain",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.UppercaseLetter, 4, 4),
                    new(AsciiCategory.AlphaNumeric, 14, 14),
                }))
                {
                    Example = "BH67BMAG00001299123456",
                    EffectiveDate = new DateTimeOffset(2012, 1, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!a14!c"), 4)
                {
                    Example = "BMAG00001299123456"
                },
                Bank = new BankStructure(new SwiftPattern("4!n"), 4)
                {
                    Example = "BMAG"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "00001299123456",
                LastUpdatedDate = new DateTimeOffset(2012, 1, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Brazil
            yield return new IbanCountry("BR")
            {
                DisplayName = "Brazil",
                EnglishName = "Brazil",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 8, 8),
                    new(AsciiCategory.Digit, 5, 5),
                    new(AsciiCategory.Digit, 10, 10),
                    new(AsciiCategory.UppercaseLetter, 1, 1),
                    new(AsciiCategory.AlphaNumeric, 1, 1),
                }))
                {
                    Example = "BR1800360305000010009795493C1",
                    EffectiveDate = new DateTimeOffset(2013, 7, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("8!n5!n10!n1!a1!c"), 4)
                {
                    Example = "00360305000010009795493P1"
                },
                Bank = new BankStructure(new SwiftPattern("8!n"), 4)
                {
                    Example = "00360305"
                },
                Branch = new BranchStructure(new SwiftPattern("5!n"), 12)
                {
                    Example = "00001"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "0009795493C1",
                LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Republic of Belarus
            yield return new IbanCountry("BY")
            {
                DisplayName = "Republic of Belarus",
                EnglishName = "Republic of Belarus",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.AlphaNumeric, 4, 4),
                    new(AsciiCategory.Digit, 4, 4),
                    new(AsciiCategory.AlphaNumeric, 16, 16),
                }))
                {
                    Example = "BY13NBRB3600900000002Z00AB00",
                    EffectiveDate = new DateTimeOffset(2017, 7, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!c4!n16!c"), 4)
                {
                    Example = "NBRB3600900000002Z00AB00"
                },
                Bank = new BankStructure(new SwiftPattern("4!c"), 4)
                {
                    Example = "NBRB"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "3600 0000 0000 0Z00 AB00",
                LastUpdatedDate = new DateTimeOffset(2017, 3, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Switzerland
            yield return new IbanCountry("CH")
            {
                DisplayName = "Switzerland",
                EnglishName = "Switzerland",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 5, 5),
                    new(AsciiCategory.AlphaNumeric, 12, 12),
                }))
                {
                    Example = "CH9300762011623852957",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("5!n12!c"), 4)
                {
                    Example = "00762011623852957"
                },
                Bank = new BankStructure(new SwiftPattern("5!n"), 4)
                {
                    Example = "00762"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "762 1162-3852.957",
                LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Costa Rica
            yield return new IbanCountry("CR")
            {
                DisplayName = "Costa Rica",
                EnglishName = "Costa Rica",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 4, 4),
                    new(AsciiCategory.Digit, 14, 14),
                }))
                {
                    Example = "CR05015202001026284066",
                    EffectiveDate = new DateTimeOffset(2011, 6, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!n14!n"), 4)
                {
                    Example = "15202001026284066"
                },
                Bank = new BankStructure(new SwiftPattern("4!n"), 4)
                {
                    Example = "0152"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "02001026284066",
                LastUpdatedDate = new DateTimeOffset(2019, 1, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Cyprus
            yield return new IbanCountry("CY")
            {
                DisplayName = "Cyprus",
                EnglishName = "Cyprus",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 3, 3),
                    new(AsciiCategory.Digit, 5, 5),
                    new(AsciiCategory.AlphaNumeric, 16, 16),
                }))
                {
                    Example = "CY17002001280000001200527600",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("3!n5!n16!c"), 4)
                {
                    Example = "002001280000001200527600"
                },
                Bank = new BankStructure(new SwiftPattern("3!n"), 4)
                {
                    Example = "002"
                },
                Branch = new BranchStructure(new SwiftPattern("5!n"), 7)
                {
                    Example = "00128"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "0000001200527600",
                LastUpdatedDate = new DateTimeOffset(2009, 8, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Czechia
            yield return new IbanCountry("CZ")
            {
                DisplayName = "Czechia",
                EnglishName = "Czechia",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 4, 4),
                    new(AsciiCategory.Digit, 6, 6),
                    new(AsciiCategory.Digit, 10, 10),
                }))
                {
                    Example = "CZ6508000000192000145399",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!n6!n10!n"), 4)
                {
                    Example = "08000000192000145399"
                },
                Bank = new BankStructure(new SwiftPattern("4!n"), 4)
                {
                    Example = "0800"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "19-2000145399/0800",
                LastUpdatedDate = new DateTimeOffset(2016, 12, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Germany
            yield return new IbanCountry("DE")
            {
                DisplayName = "Germany",
                EnglishName = "Germany",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 8, 8),
                    new(AsciiCategory.Digit, 10, 10),
                }))
                {
                    Example = "DE89370400440532013000",
                    EffectiveDate = new DateTimeOffset(2007, 7, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("8!n10!n"), 4)
                {
                    Example = "370400440532013000"
                },
                Bank = new BankStructure(new SwiftPattern("8!n"), 4)
                {
                    Example = "37040044"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "532013000",
                LastUpdatedDate = new DateTimeOffset(2011, 1, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Denmark
            yield return new IbanCountry("DK")
            {
                DisplayName = "Denmark",
                EnglishName = "Denmark",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 4, 4),
                    new(AsciiCategory.Digit, 9, 9),
                    new(AsciiCategory.Digit, 1, 1),
                }))
                {
                    Example = "DK5000400440116243",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!n9!n1!n"), 4)
                {
                    Example = "00400440116243"
                },
                Bank = new BankStructure(new SwiftPattern("4!n"), 4)
                {
                    Example = "0040"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "0040 0440116243",
                LastUpdatedDate = new DateTimeOffset(2018, 11, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Dominican Republic
            yield return new IbanCountry("DO")
            {
                DisplayName = "Dominican Republic",
                EnglishName = "Dominican Republic",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.AlphaNumeric, 4, 4),
                    new(AsciiCategory.Digit, 20, 20),
                }))
                {
                    Example = "DO28BAGR00000001212453611324",
                    EffectiveDate = new DateTimeOffset(2010, 12, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!c20!n"), 4)
                {
                    Example = "BAGR00000001212453611324"
                },
                Bank = new BankStructure(new SwiftPattern("4!c"), 4)
                {
                    Example = "BAGR"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "00000001212453611324",
                LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Estonia
            yield return new IbanCountry("EE")
            {
                DisplayName = "Estonia",
                EnglishName = "Estonia",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 11, 11),
                    new(AsciiCategory.Digit, 1, 1),
                }))
                {
                    Example = "EE382200221020145685",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("2!n2!n11!n1!n"), 4)
                {
                    Example = "2200221020145685"
                },
                Bank = new BankStructure(new SwiftPattern("2!n"), 4)
                {
                    Example = "22"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "221020145685",
                LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Egypt
            yield return new IbanCountry("EG")
            {
                DisplayName = "Egypt",
                EnglishName = "Egypt",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 4, 4),
                    new(AsciiCategory.Digit, 4, 4),
                    new(AsciiCategory.Digit, 17, 17),
                }))
                {
                    Example = "EG380019000500000000263180002",
                    EffectiveDate = new DateTimeOffset(2021, 1, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!n4!n17!n"), 4)
                {
                    Example = "0019000500000000263180002"
                },
                Bank = new BankStructure(new SwiftPattern("4!n"), 4)
                {
                    Example = "0019"
                },
                Branch = new BranchStructure(new SwiftPattern("4!n"), 8)
                {
                    Example = "0005"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "000263180002",
                LastUpdatedDate = new DateTimeOffset(2020, 1, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Spain
            yield return new IbanCountry("ES")
            {
                DisplayName = "Spain",
                EnglishName = "Spain",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 4, 4),
                    new(AsciiCategory.Digit, 4, 4),
                    new(AsciiCategory.Digit, 1, 1),
                    new(AsciiCategory.Digit, 1, 1),
                    new(AsciiCategory.Digit, 10, 10),
                }))
                {
                    Example = "ES9121000418450200051332",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!n4!n1!n1!n10!n"), 4)
                {
                    Example = "21000418450200051332"
                },
                Bank = new BankStructure(new SwiftPattern("4!n"), 4)
                {
                    Example = "2100"
                },
                Branch = new BranchStructure(new SwiftPattern("4!n"), 8)
                {
                    Example = "0418"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "2100 0418 45 0200051332",
                LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Finland
            yield return new IbanCountry("FI")
            {
                DisplayName = "Finland",
                EnglishName = "Finland",
                IncludedCountries = new[]
                {
                    "fi-AX"
                },
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 3, 3),
                    new(AsciiCategory.Digit, 11, 11),
                }))
                {
                    Example = "FI2112345600000785",
                    EffectiveDate = new DateTimeOffset(2011, 12, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("3!n11!n"), 4)
                {
                    Example = ""
                },
                Bank = new BankStructure(new SwiftPattern("3!n"), 4)
                {
                    Example = "123"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                    IncludedCountries = new[]
                    {
                        "fi-AX"
                    }
                },
                DomesticAccountNumberExample = "",
                LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Faroe Islands
            yield return new IbanCountry("FO")
            {
                DisplayName = "Faroe Islands",
                EnglishName = "Faroe Islands",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 4, 4),
                    new(AsciiCategory.Digit, 9, 9),
                    new(AsciiCategory.Digit, 1, 1),
                }))
                {
                    Example = "FO6264600001631634",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!n9!n1!n"), 4)
                {
                    Example = "64600001631634"
                },
                Bank = new BankStructure(new SwiftPattern("4!n"), 4)
                {
                    Example = "6460"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "6460 0001631634",
                LastUpdatedDate = new DateTimeOffset(2017, 2, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // France
            yield return new IbanCountry("FR")
            {
                DisplayName = "France",
                EnglishName = "France",
                IncludedCountries = new[]
                {
                    "fr-GF", "fr-GP", "fr-MQ", "fr-RE", "fr-PF", "fr-TF", "fr-YT", "fr-NC", "fr-BL", "fr-MF", "fr-PM", "fr-WF"
                },
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 5, 5),
                    new(AsciiCategory.Digit, 5, 5),
                    new(AsciiCategory.AlphaNumeric, 11, 11),
                    new(AsciiCategory.Digit, 2, 2),
                }))
                {
                    Example = "FR1420041010050500013M02606",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("5!n5!n11!c2!n"), 4)
                {
                    Example = "20041010050500013M02606"
                },
                Bank = new BankStructure(new SwiftPattern("5!n"), 4)
                {
                    Example = "20041"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                    IncludedCountries = new[]
                    {
                        "fr-GF", "fr-GP", "fr-MQ", "fr-YT", "fr-RE", "fr-PM", "fr-BL", "fr-MF"
                    }
                },
                DomesticAccountNumberExample = "20041 01005 0500013M026 06",
                LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // United Kingdom
            yield return new IbanCountry("GB")
            {
                DisplayName = "United Kingdom",
                EnglishName = "United Kingdom",
                IncludedCountries = new[]
                {
                    "gb-IM", "gb-JE", "gb-GG"
                },
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.UppercaseLetter, 4, 4),
                    new(AsciiCategory.Digit, 6, 6),
                    new(AsciiCategory.Digit, 8, 8),
                }))
                {
                    Example = "GB29NWBK60161331926819",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!a6!n8!n"), 4)
                {
                    Example = "NWBK60161331926819"
                },
                Bank = new BankStructure(new SwiftPattern("4!a"), 4)
                {
                    Example = "NWBK"
                },
                Branch = new BranchStructure(new SwiftPattern("6!n"), 8)
                {
                    Example = "601613"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "60-16-13 31926819",
                LastUpdatedDate = new DateTimeOffset(2017, 5, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Georgia
            yield return new IbanCountry("GE")
            {
                DisplayName = "Georgia",
                EnglishName = "Georgia",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 16, 16),
                }))
                {
                    Example = "GE29NB0000000101904917",
                    EffectiveDate = new DateTimeOffset(2010, 5, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("2!a16!n"), 4)
                {
                    Example = "NB0000000101904917"
                },
                Bank = new BankStructure(new SwiftPattern("2!a"), 4)
                {
                    Example = "NB"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "0000000101904917",
                LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Gibraltar
            yield return new IbanCountry("GI")
            {
                DisplayName = "Gibraltar",
                EnglishName = "Gibraltar",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.UppercaseLetter, 4, 4),
                    new(AsciiCategory.AlphaNumeric, 15, 15),
                }))
                {
                    Example = "GI75NWBK000000007099453",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!a15!c"), 4)
                {
                    Example = "NWBK000000007099453"
                },
                Bank = new BankStructure(new SwiftPattern("4!a"), 4)
                {
                    Example = "NWBK"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "0000 00007099 453",
                LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Greenland
            yield return new IbanCountry("GL")
            {
                DisplayName = "Greenland",
                EnglishName = "Greenland",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 4, 4),
                    new(AsciiCategory.Digit, 9, 9),
                    new(AsciiCategory.Digit, 1, 1),
                }))
                {
                    Example = "GL8964710001000206",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!n9!n1!n"), 4)
                {
                    Example = "64710001000206"
                },
                Bank = new BankStructure(new SwiftPattern("4!n"), 4)
                {
                    Example = "6471"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "6471 0001000206",
                LastUpdatedDate = new DateTimeOffset(2018, 11, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Greece
            yield return new IbanCountry("GR")
            {
                DisplayName = "Greece",
                EnglishName = "Greece",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 3, 3),
                    new(AsciiCategory.Digit, 4, 4),
                    new(AsciiCategory.AlphaNumeric, 16, 16),
                }))
                {
                    Example = "GR1601101250000000012300695",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("3!n4!n16!c"), 4)
                {
                    Example = "01101250000000012300695"
                },
                Bank = new BankStructure(new SwiftPattern("3!n"), 4)
                {
                    Example = "011"
                },
                Branch = new BranchStructure(new SwiftPattern("4!n"), 7)
                {
                    Example = "0125"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "01250000000012300695",
                LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Guatemala
            yield return new IbanCountry("GT")
            {
                DisplayName = "Guatemala",
                EnglishName = "Guatemala",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.AlphaNumeric, 4, 4),
                    new(AsciiCategory.AlphaNumeric, 20, 20),
                }))
                {
                    Example = "GT82TRAJ01020000001210029690",
                    EffectiveDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!c20!c"), 4)
                {
                    Example = "TRAJ01020000001210029690"
                },
                Bank = new BankStructure(new SwiftPattern("4!c"), 4)
                {
                    Example = "TRAJ"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "01020000001210029690",
                LastUpdatedDate = new DateTimeOffset(2016, 10, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Croatia
            yield return new IbanCountry("HR")
            {
                DisplayName = "Croatia",
                EnglishName = "Croatia",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 7, 7),
                    new(AsciiCategory.Digit, 10, 10),
                }))
                {
                    Example = "HR1210010051863000160",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("7!n10!n"), 4)
                {
                    Example = "10010051863000160"
                },
                Bank = new BankStructure(new SwiftPattern("7!n"), 4)
                {
                    Example = "1001005"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "1001005-1863000160",
                LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Hungary
            yield return new IbanCountry("HU")
            {
                DisplayName = "Hungary",
                EnglishName = "Hungary",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 3, 3),
                    new(AsciiCategory.Digit, 4, 4),
                    new(AsciiCategory.Digit, 1, 1),
                    new(AsciiCategory.Digit, 15, 15),
                    new(AsciiCategory.Digit, 1, 1),
                }))
                {
                    Example = "HU42117730161111101800000000",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("3!n4!n1!n15!n1!n"), 4)
                {
                    Example = "117730161111101800000000"
                },
                Bank = new BankStructure(new SwiftPattern("3!n"), 4)
                {
                    Example = "117"
                },
                Branch = new BranchStructure(new SwiftPattern("4!n"), 7)
                {
                    Example = "7301"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "11773016-11111018-00000000",
                LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Ireland
            yield return new IbanCountry("IE")
            {
                DisplayName = "Ireland",
                EnglishName = "Ireland",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.UppercaseLetter, 4, 4),
                    new(AsciiCategory.Digit, 6, 6),
                    new(AsciiCategory.Digit, 8, 8),
                }))
                {
                    Example = "IE29AIBK93115212345678",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!a6!n8!n"), 4)
                {
                    Example = "AIBK93115212345678"
                },
                Bank = new BankStructure(new SwiftPattern("4!a"), 4)
                {
                    Example = "AIBK"
                },
                Branch = new BranchStructure(new SwiftPattern("6!n"), 8)
                {
                    Example = "931152"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "93-11-52 12345678",
                LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Israel
            yield return new IbanCountry("IL")
            {
                DisplayName = "Israel",
                EnglishName = "Israel",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 3, 3),
                    new(AsciiCategory.Digit, 3, 3),
                    new(AsciiCategory.Digit, 13, 13),
                }))
                {
                    Example = "IL620108000000099999999",
                    EffectiveDate = new DateTimeOffset(2007, 7, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("3!n3!n13!n"), 4)
                {
                    Example = "010800000099999999"
                },
                Bank = new BankStructure(new SwiftPattern("3!n"), 4)
                {
                    Example = "010"
                },
                Branch = new BranchStructure(new SwiftPattern("3!n"), 7)
                {
                    Example = "800"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "10-800-99999999",
                LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Iraq
            yield return new IbanCountry("IQ")
            {
                DisplayName = "Iraq",
                EnglishName = "Iraq",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.UppercaseLetter, 4, 4),
                    new(AsciiCategory.Digit, 3, 3),
                    new(AsciiCategory.Digit, 12, 12),
                }))
                {
                    Example = "IQ98NBIQ850123456789012",
                    EffectiveDate = new DateTimeOffset(2017, 1, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!a3!n12!n"), 4)
                {
                    Example = "NBIQ850123456789012"
                },
                Bank = new BankStructure(new SwiftPattern("4!a"), 4)
                {
                    Example = "NBIQ"
                },
                Branch = new BranchStructure(new SwiftPattern("3!n"), 8)
                {
                    Example = "850"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "123456789012",
                LastUpdatedDate = new DateTimeOffset(2016, 11, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Iceland
            yield return new IbanCountry("IS")
            {
                DisplayName = "Iceland",
                EnglishName = "Iceland",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 4, 4),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 6, 6),
                    new(AsciiCategory.Digit, 10, 10),
                }))
                {
                    Example = "IS140159260076545510730339",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!n2!n6!n10!n"), 4)
                {
                    Example = "0159260076545510730339"
                },
                Bank = new BankStructure(new SwiftPattern("2!n"), 4)
                {
                    Example = "01"
                },
                Branch = new BranchStructure(new SwiftPattern("2!n"), 6)
                {
                    Example = "59"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "0159-26-007654-551073-0339",
                LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Italy
            yield return new IbanCountry("IT")
            {
                DisplayName = "Italy",
                EnglishName = "Italy",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.UppercaseLetter, 1, 1),
                    new(AsciiCategory.Digit, 5, 5),
                    new(AsciiCategory.Digit, 5, 5),
                    new(AsciiCategory.AlphaNumeric, 12, 12),
                }))
                {
                    Example = "IT60X0542811101000000123456",
                    EffectiveDate = new DateTimeOffset(2007, 7, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("1!a5!n5!n12!c"), 4)
                {
                    Example = "X0542811101000000123456"
                },
                Bank = new BankStructure(new SwiftPattern("5!n"), 5)
                {
                    Example = "05428"
                },
                Branch = new BranchStructure(new SwiftPattern("5!n"), 10)
                {
                    Example = "11101"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "X 05428 11101 000000123456",
                LastUpdatedDate = new DateTimeOffset(2013, 3, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Jordan
            yield return new IbanCountry("JO")
            {
                DisplayName = "Jordan",
                EnglishName = "Jordan",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.UppercaseLetter, 4, 4),
                    new(AsciiCategory.Digit, 4, 4),
                    new(AsciiCategory.AlphaNumeric, 18, 18),
                }))
                {
                    Example = "JO94CBJO0010000000000131000302",
                    EffectiveDate = new DateTimeOffset(2014, 2, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!a4!n18!c"), 4)
                {
                    Example = "CBJO0010000000000131000302"
                },
                Bank = new BankStructure(new SwiftPattern("4!n"), 8)
                {
                    Example = "CBJO"
                },
                Branch = new BranchStructure(new SwiftPattern("4!n"), 8)
                {
                    Example = ""
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "0001310000302",
                LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Kuwait
            yield return new IbanCountry("KW")
            {
                DisplayName = "Kuwait",
                EnglishName = "Kuwait",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.UppercaseLetter, 4, 4),
                    new(AsciiCategory.AlphaNumeric, 22, 22),
                }))
                {
                    Example = "KW81CBKU0000000000001234560101",
                    EffectiveDate = new DateTimeOffset(2011, 1, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!a22!c"), 4)
                {
                    Example = "CBKU0000000000001234560101"
                },
                Bank = new BankStructure(new SwiftPattern("4!a"), 4)
                {
                    Example = "CBKU"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "1234560101",
                LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Kazakhstan
            yield return new IbanCountry("KZ")
            {
                DisplayName = "Kazakhstan",
                EnglishName = "Kazakhstan",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 3, 3),
                    new(AsciiCategory.AlphaNumeric, 13, 13),
                }))
                {
                    Example = "KZ86125KZT5004100100",
                    EffectiveDate = new DateTimeOffset(2010, 9, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("3!n13!c"), 4)
                {
                    Example = "125KZT5004100100"
                },
                Bank = new BankStructure(new SwiftPattern("3!n"), 4)
                {
                    Example = "125"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "KZ86 125K ZT50 0410 0100",
                LastUpdatedDate = new DateTimeOffset(2016, 3, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Lebanon
            yield return new IbanCountry("LB")
            {
                DisplayName = "Lebanon",
                EnglishName = "Lebanon",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 4, 4),
                    new(AsciiCategory.AlphaNumeric, 20, 20),
                }))
                {
                    Example = "LB62099900000001001901229114",
                    EffectiveDate = new DateTimeOffset(2010, 1, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!n20!c"), 4)
                {
                    Example = "099900000001001901229114"
                },
                Bank = new BankStructure(new SwiftPattern("4!a"), 4)
                {
                    Example = "0999"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "01 001 901229114",
                LastUpdatedDate = new DateTimeOffset(2010, 1, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Saint Lucia
            yield return new IbanCountry("LC")
            {
                DisplayName = "Saint Lucia",
                EnglishName = "Saint Lucia",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.UppercaseLetter, 4, 4),
                    new(AsciiCategory.AlphaNumeric, 24, 24),
                }))
                {
                    Example = "LC55HEMM000100010012001200023015",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!a24!c"), 4)
                {
                    Example = "HEMM000100010012001200023015"
                },
                Bank = new BankStructure(new SwiftPattern("4!a"), 4)
                {
                    Example = "HEMM"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "0001 0001 0012 0012 0002 3015",
                LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Liechtenstein
            yield return new IbanCountry("LI")
            {
                DisplayName = "Liechtenstein",
                EnglishName = "Liechtenstein",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 5, 5),
                    new(AsciiCategory.AlphaNumeric, 12, 12),
                }))
                {
                    Example = "LI21088100002324013AA",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("5!n12!c"), 4)
                {
                    Example = "088100002324013AA"
                },
                Bank = new BankStructure(new SwiftPattern("5!n"), 4)
                {
                    Example = "08810"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "8810 2324013AA",
                LastUpdatedDate = new DateTimeOffset(2012, 4, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Lithuania
            yield return new IbanCountry("LT")
            {
                DisplayName = "Lithuania",
                EnglishName = "Lithuania",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 5, 5),
                    new(AsciiCategory.Digit, 11, 11),
                }))
                {
                    Example = "LT121000011101001000",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("5!n11!n"), 4)
                {
                    Example = "1000011101001000"
                },
                Bank = new BankStructure(new SwiftPattern("5!n"), 4)
                {
                    Example = "10000"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "",
                LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Luxembourg
            yield return new IbanCountry("LU")
            {
                DisplayName = "Luxembourg",
                EnglishName = "Luxembourg",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 3, 3),
                    new(AsciiCategory.AlphaNumeric, 13, 13),
                }))
                {
                    Example = "LU280019400644750000",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("3!n13!c"), 4)
                {
                    Example = "0019400644750000"
                },
                Bank = new BankStructure(new SwiftPattern("3!n"), 4)
                {
                    Example = "001"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "",
                LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Latvia
            yield return new IbanCountry("LV")
            {
                DisplayName = "Latvia",
                EnglishName = "Latvia",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.UppercaseLetter, 4, 4),
                    new(AsciiCategory.AlphaNumeric, 13, 13),
                }))
                {
                    Example = "LV80BANK0000435195001",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!a13!c"), 4)
                {
                    Example = "BANK0000435195001"
                },
                Bank = new BankStructure(new SwiftPattern("4!a"), 4)
                {
                    Example = "BANK"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "LV80 BANK 0000 4351 9500 1",
                LastUpdatedDate = new DateTimeOffset(2009, 1, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Libya
            yield return new IbanCountry("LY")
            {
                DisplayName = "Libya",
                EnglishName = "Libya",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 3, 3),
                    new(AsciiCategory.Digit, 3, 3),
                    new(AsciiCategory.Digit, 15, 15),
                }))
                {
                    Example = "LY83002048000020100120361",
                    EffectiveDate = new DateTimeOffset(2021, 1, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("3!n3!n15!n"), 4)
                {
                    Example = "002048000020100120361"
                },
                Bank = new BankStructure(new SwiftPattern("3!n"), 4)
                {
                    Example = "002"
                },
                Branch = new BranchStructure(new SwiftPattern("3!n"), 7)
                {
                    Example = "048"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "000020100120361",
                LastUpdatedDate = new DateTimeOffset(2020, 9, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Monaco
            yield return new IbanCountry("MC")
            {
                DisplayName = "Monaco",
                EnglishName = "Monaco",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 5, 5),
                    new(AsciiCategory.Digit, 5, 5),
                    new(AsciiCategory.AlphaNumeric, 11, 11),
                    new(AsciiCategory.Digit, 2, 2),
                }))
                {
                    Example = "MC5811222000010123456789030",
                    EffectiveDate = new DateTimeOffset(2008, 1, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("5!n5!n11!c2!n"), 4)
                {
                    Example = "11222000010123456789030"
                },
                Bank = new BankStructure(new SwiftPattern("5!n"), 4)
                {
                    Example = "11222"
                },
                Branch = new BranchStructure(new SwiftPattern("5!n"), 9)
                {
                    Example = "00001"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "0011111000h",
                LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Moldova
            yield return new IbanCountry("MD")
            {
                DisplayName = "Moldova",
                EnglishName = "Moldova",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.AlphaNumeric, 2, 2),
                    new(AsciiCategory.AlphaNumeric, 18, 18),
                }))
                {
                    Example = "MD24AG000225100013104168",
                    EffectiveDate = new DateTimeOffset(2016, 1, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("2!c18!c"), 4)
                {
                    Example = "AG000225100013104168"
                },
                Bank = new BankStructure(new SwiftPattern("2!c"), 4)
                {
                    Example = "AG"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "000225100013104168",
                LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Montenegro
            yield return new IbanCountry("ME")
            {
                DisplayName = "Montenegro",
                EnglishName = "Montenegro",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 3, 3),
                    new(AsciiCategory.Digit, 13, 13),
                    new(AsciiCategory.Digit, 2, 2),
                }))
                {
                    Example = "ME25505000012345678951",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("3!n13!n2!n"), 4)
                {
                    Example = "505000012345678951"
                },
                Bank = new BankStructure(new SwiftPattern("3!n"), 4)
                {
                    Example = "505"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "505 0000123456789 51",
                LastUpdatedDate = new DateTimeOffset(2010, 5, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Macedonia
            yield return new IbanCountry("MK")
            {
                DisplayName = "Macedonia",
                EnglishName = "Macedonia",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 3, 3),
                    new(AsciiCategory.AlphaNumeric, 10, 10),
                    new(AsciiCategory.Digit, 2, 2),
                }))
                {
                    Example = "MK07250120000058984",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("3!n10!c2!n"), 4)
                {
                    Example = "250120000058984"
                },
                Bank = new BankStructure(new SwiftPattern("3!n"), 4)
                {
                    Example = "300"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "MK07 300 0000000424 25",
                LastUpdatedDate = new DateTimeOffset(2011, 1, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Mauritania
            yield return new IbanCountry("MR")
            {
                DisplayName = "Mauritania",
                EnglishName = "Mauritania",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 5, 5),
                    new(AsciiCategory.Digit, 5, 5),
                    new(AsciiCategory.Digit, 11, 11),
                    new(AsciiCategory.Digit, 2, 2),
                }))
                {
                    Example = "MR1300020001010000123456753",
                    EffectiveDate = new DateTimeOffset(2012, 1, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("5!n5!n11!n2!n"), 4)
                {
                    Example = "00020001010000123456753"
                },
                Bank = new BankStructure(new SwiftPattern("5!n"), 4)
                {
                    Example = "00020"
                },
                Branch = new BranchStructure(new SwiftPattern("5!n"), 9)
                {
                    Example = "00101"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "00020 00101 00001234567 53",
                LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Malta
            yield return new IbanCountry("MT")
            {
                DisplayName = "Malta",
                EnglishName = "Malta",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.UppercaseLetter, 4, 4),
                    new(AsciiCategory.Digit, 5, 5),
                    new(AsciiCategory.AlphaNumeric, 18, 18),
                }))
                {
                    Example = "MT84MALT011000012345MTLCAST001S",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!a5!n18!c"), 4)
                {
                    Example = "MALT011000012345MTLCAST001S"
                },
                Bank = new BankStructure(new SwiftPattern("4!a"), 4)
                {
                    Example = "MALT"
                },
                Branch = new BranchStructure(new SwiftPattern("5!n"), 8)
                {
                    Example = "01100"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "12345MTLCAST001S",
                LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Mauritius
            yield return new IbanCountry("MU")
            {
                DisplayName = "Mauritius",
                EnglishName = "Mauritius",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.UppercaseLetter, 4, 4),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 12, 12),
                    new(AsciiCategory.Digit, 3, 3),
                    new(AsciiCategory.UppercaseLetter, 3, 3),
                }))
                {
                    Example = "MU17BOMM0101101030300200000MUR",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!a2!n2!n12!n3!n3!a"), 4)
                {
                    Example = "BOMM0101101030300200000MUR"
                },
                Bank = new BankStructure(new SwiftPattern("6!c"), 4)
                {
                    Example = "BOMM01"
                },
                Branch = new BranchStructure(new SwiftPattern("2!n"), 10)
                {
                    Example = "01"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "MU17 BOMM 0101 1010 3030 0200 000M UR",
                LastUpdatedDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Netherlands (The)
            yield return new IbanCountry("NL")
            {
                DisplayName = "Netherlands (The)",
                EnglishName = "Netherlands (The)",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.UppercaseLetter, 4, 4),
                    new(AsciiCategory.Digit, 10, 10),
                }))
                {
                    Example = "NL91ABNA0417164300",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!a10!n"), 4)
                {
                    Example = "ABNA0417164300"
                },
                Bank = new BankStructure(new SwiftPattern("4!a"), 4)
                {
                    Example = "ABNA"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "041 71 64 300",
                LastUpdatedDate = new DateTimeOffset(2020, 9, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Norway
            yield return new IbanCountry("NO")
            {
                DisplayName = "Norway",
                EnglishName = "Norway",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 4, 4),
                    new(AsciiCategory.Digit, 6, 6),
                    new(AsciiCategory.Digit, 1, 1),
                }))
                {
                    Example = "NO9386011117947",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!n6!n1!n"), 4)
                {
                    Example = "86011117947"
                },
                Bank = new BankStructure(new SwiftPattern("4!n"), 4)
                {
                    Example = "8601"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "8601 11 17947",
                LastUpdatedDate = new DateTimeOffset(2009, 8, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Pakistan
            yield return new IbanCountry("PK")
            {
                DisplayName = "Pakistan",
                EnglishName = "Pakistan",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.UppercaseLetter, 4, 4),
                    new(AsciiCategory.AlphaNumeric, 16, 16),
                }))
                {
                    Example = "PK36SCBL0000001123456702",
                    EffectiveDate = new DateTimeOffset(2012, 12, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!a16!c"), 4)
                {
                    Example = "SCBL0000001123456702"
                },
                Bank = new BankStructure(new SwiftPattern("4!a"), 4)
                {
                    Example = "SCBL"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "00260101036360",
                LastUpdatedDate = new DateTimeOffset(2012, 12, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Poland
            yield return new IbanCountry("PL")
            {
                DisplayName = "Poland",
                EnglishName = "Poland",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 8, 8),
                    new(AsciiCategory.Digit, 16, 16),
                }))
                {
                    Example = "PL61109010140000071219812874",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("8!n16!n"), 4)
                {
                    Example = "109010140000071219812874"
                },
                Branch = new BranchStructure(new SwiftPattern("8!n"), 4)
                {
                    Example = "10901014"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "61 1090 1014 0000 0712 1981 2874",
                LastUpdatedDate = new DateTimeOffset(2016, 10, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Palestine, State of
            yield return new IbanCountry("PS")
            {
                DisplayName = "Palestine, State of",
                EnglishName = "Palestine, State of",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.UppercaseLetter, 4, 4),
                    new(AsciiCategory.AlphaNumeric, 21, 21),
                }))
                {
                    Example = "PS92PALS000000000400123456702",
                    EffectiveDate = new DateTimeOffset(2012, 7, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!a21!c"), 4)
                {
                    Example = "PALS000000000400123456702"
                },
                Bank = new BankStructure(new SwiftPattern("4!a"), 4)
                {
                    Example = "PALS"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "400123456702",
                LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Portugal
            yield return new IbanCountry("PT")
            {
                DisplayName = "Portugal",
                EnglishName = "Portugal",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 4, 4),
                    new(AsciiCategory.Digit, 4, 4),
                    new(AsciiCategory.Digit, 11, 11),
                    new(AsciiCategory.Digit, 2, 2),
                }))
                {
                    Example = "PT50000201231234567890154",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!n4!n11!n2!n"), 4)
                {
                    Example = "000201231234567890154"
                },
                Bank = new BankStructure(new SwiftPattern("4!n"), 4)
                {
                    Example = "0002"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                    IncludedCountries = new[]
                    {
                        "pt-Az", "pt-Ma"
                    }
                },
                DomesticAccountNumberExample = "0002.0123.12345678901.54",
                LastUpdatedDate = new DateTimeOffset(2014, 1, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Qatar
            yield return new IbanCountry("QA")
            {
                DisplayName = "Qatar",
                EnglishName = "Qatar",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.UppercaseLetter, 4, 4),
                    new(AsciiCategory.AlphaNumeric, 21, 21),
                }))
                {
                    Example = "QA58DOHB00001234567890ABCDEFG",
                    EffectiveDate = new DateTimeOffset(2014, 1, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!a21!c"), 4)
                {
                    Example = "DOHB00001234567890ABCDEFG"
                },
                Bank = new BankStructure(new SwiftPattern("4!a"), 4)
                {
                    Example = "DOHB"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "00001234567890ABCDEFG",
                LastUpdatedDate = new DateTimeOffset(2014, 1, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Romania
            yield return new IbanCountry("RO")
            {
                DisplayName = "Romania",
                EnglishName = "Romania",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.UppercaseLetter, 4, 4),
                    new(AsciiCategory.AlphaNumeric, 16, 16),
                }))
                {
                    Example = "RO49AAAA1B31007593840000",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!a16!c"), 4)
                {
                    Example = "AAAA1B31007593840000"
                },
                Bank = new BankStructure(new SwiftPattern("4!a"), 4)
                {
                    Example = "AAAA"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "RO49 AAAA 1B31 0075 9384 0000",
                LastUpdatedDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Serbia
            yield return new IbanCountry("RS")
            {
                DisplayName = "Serbia",
                EnglishName = "Serbia",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 3, 3),
                    new(AsciiCategory.Digit, 13, 13),
                    new(AsciiCategory.Digit, 2, 2),
                }))
                {
                    Example = "RS35260005601001611379",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("3!n13!n2!n"), 4)
                {
                    Example = "260005601001611379"
                },
                Bank = new BankStructure(new SwiftPattern("3!n"), 4)
                {
                    Example = "260"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "260-0056010016113-79",
                LastUpdatedDate = new DateTimeOffset(2017, 3, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Saudi Arabia
            yield return new IbanCountry("SA")
            {
                DisplayName = "Saudi Arabia",
                EnglishName = "Saudi Arabia",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.AlphaNumeric, 18, 18),
                }))
                {
                    Example = "SA0380000000608010167519",
                    EffectiveDate = new DateTimeOffset(2016, 7, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("2!n18!c"), 4)
                {
                    Example = "80000000608010167519"
                },
                Bank = new BankStructure(new SwiftPattern("2!n"), 4)
                {
                    Example = "80"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "608010167519",
                LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Seychelles
            yield return new IbanCountry("SC")
            {
                DisplayName = "Seychelles",
                EnglishName = "Seychelles",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.UppercaseLetter, 4, 4),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 16, 16),
                    new(AsciiCategory.UppercaseLetter, 3, 3),
                }))
                {
                    Example = "SC18SSCB11010000000000001497USD",
                    EffectiveDate = new DateTimeOffset(2016, 10, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!a2!n2!n16!n3!a"), 4)
                {
                    Example = "SSCB11010000000000001497USD"
                },
                Bank = new BankStructure(new SwiftPattern("4!a2!n"), 4)
                {
                    Example = "SSCB11"
                },
                Branch = new BranchStructure(new SwiftPattern("2!n"), 10)
                {
                    Example = "01"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "0000000000001497",
                LastUpdatedDate = new DateTimeOffset(2019, 10, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Sudan
            yield return new IbanCountry("SD")
            {
                DisplayName = "Sudan",
                EnglishName = "Sudan",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 12, 12),
                }))
                {
                    Example = "SD2129010501234001",
                    EffectiveDate = new DateTimeOffset(2021, 7, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("2!n12!n"), 4)
                {
                    Example = "29010501234001"
                },
                Bank = new BankStructure(new SwiftPattern("2!n"), 4)
                {
                    Example = "29"
                },
                Branch = new BranchStructure(new SwiftPattern("12!n"), 6)
                {
                    Example = "010501234001"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "010501234001",
                LastUpdatedDate = new DateTimeOffset(2021, 6, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Sweden
            yield return new IbanCountry("SE")
            {
                DisplayName = "Sweden",
                EnglishName = "Sweden",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 3, 3),
                    new(AsciiCategory.Digit, 16, 16),
                    new(AsciiCategory.Digit, 1, 1),
                }))
                {
                    Example = "SE4550000000058398257466",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("3!n16!n1!n"), 4)
                {
                    Example = "50000000058398257466"
                },
                Bank = new BankStructure(new SwiftPattern("3!n"), 4)
                {
                    Example = "123"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "1234 12 3456 1",
                LastUpdatedDate = new DateTimeOffset(2009, 8, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Slovenia
            yield return new IbanCountry("SI")
            {
                DisplayName = "Slovenia",
                EnglishName = "Slovenia",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 5, 5),
                    new(AsciiCategory.Digit, 8, 8),
                    new(AsciiCategory.Digit, 2, 2),
                }))
                {
                    Example = "SI56263300012039086",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("5!n8!n2!n"), 4)
                {
                    Example = "263300012039086"
                },
                Bank = new BankStructure(new SwiftPattern("5!n"), 4)
                {
                    Example = "26330"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "2633 0001 2039 086",
                LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Slovakia
            yield return new IbanCountry("SK")
            {
                DisplayName = "Slovakia",
                EnglishName = "Slovakia",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 4, 4),
                    new(AsciiCategory.Digit, 6, 6),
                    new(AsciiCategory.Digit, 10, 10),
                }))
                {
                    Example = "SK3112000000198742637541",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!n6!n10!n"), 4)
                {
                    Example = "12000000198742637541"
                },
                Bank = new BankStructure(new SwiftPattern("4!n"), 4)
                {
                    Example = "1200"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "19-8742637541/1200",
                LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // San Marino
            yield return new IbanCountry("SM")
            {
                DisplayName = "San Marino",
                EnglishName = "San Marino",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.UppercaseLetter, 1, 1),
                    new(AsciiCategory.Digit, 5, 5),
                    new(AsciiCategory.Digit, 5, 5),
                    new(AsciiCategory.AlphaNumeric, 12, 12),
                }))
                {
                    Example = "SM86U0322509800000000270100",
                    EffectiveDate = new DateTimeOffset(2007, 8, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("1!a5!n5!n12!c"), 4)
                {
                    Example = "U0322509800000000270100"
                },
                Bank = new BankStructure(new SwiftPattern("5!n"), 5)
                {
                    Example = "03225"
                },
                Branch = new BranchStructure(new SwiftPattern("5!n"), 10)
                {
                    Example = "09800"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "",
                LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Sao Tome and Principe
            yield return new IbanCountry("ST")
            {
                DisplayName = "Sao Tome and Principe",
                EnglishName = "Sao Tome and Principe",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 4, 4),
                    new(AsciiCategory.Digit, 4, 4),
                    new(AsciiCategory.Digit, 11, 11),
                    new(AsciiCategory.Digit, 2, 2),
                }))
                {
                    Example = "ST23000100010051845310146",
                    EffectiveDate = new DateTimeOffset(2015, 9, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!n4!n11!n2!n"), 4)
                {
                    Example = "000100010051845310146"
                },
                Bank = new BankStructure(new SwiftPattern("4!n"), 4)
                {
                    Example = "0001"
                },
                Branch = new BranchStructure(new SwiftPattern("4!n"), 8)
                {
                    Example = "0001"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "0051845310146",
                LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // El Salvador
            yield return new IbanCountry("SV")
            {
                DisplayName = "El Salvador",
                EnglishName = "El Salvador",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.UppercaseLetter, 4, 4),
                    new(AsciiCategory.Digit, 20, 20),
                }))
                {
                    Example = "SV62CENR00000000000000700025",
                    EffectiveDate = new DateTimeOffset(2016, 12, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!a20!n"), 4)
                {
                    Example = "CENR00000000000000700025"
                },
                Bank = new BankStructure(new SwiftPattern("4!a"), 4)
                {
                    Example = "CENR"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "00000000000000700025",
                LastUpdatedDate = new DateTimeOffset(2021, 3, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Timor-Leste
            yield return new IbanCountry("TL")
            {
                DisplayName = "Timor-Leste",
                EnglishName = "Timor-Leste",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 3, 3),
                    new(AsciiCategory.Digit, 14, 14),
                    new(AsciiCategory.Digit, 2, 2),
                }))
                {
                    Example = "TL380080012345678910157",
                    EffectiveDate = new DateTimeOffset(2014, 9, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("3!n14!n2!n"), 4)
                {
                    Example = "0080012345678910157"
                },
                Bank = new BankStructure(new SwiftPattern("3!n"), 4)
                {
                    Example = "008"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "008 00123456789101 57",
                LastUpdatedDate = new DateTimeOffset(2014, 11, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Tunisia
            yield return new IbanCountry("TN")
            {
                DisplayName = "Tunisia",
                EnglishName = "Tunisia",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 3, 3),
                    new(AsciiCategory.Digit, 13, 13),
                    new(AsciiCategory.Digit, 2, 2),
                }))
                {
                    Example = "TN5910006035183598478831",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("2!n3!n13!n2!n"), 4)
                {
                    Example = "10006035183598478831"
                },
                Bank = new BankStructure(new SwiftPattern("2!n"), 4)
                {
                    Example = "10"
                },
                Branch = new BranchStructure(new SwiftPattern("3!n"), 6)
                {
                    Example = "006"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "10 006 0351835984788 31",
                LastUpdatedDate = new DateTimeOffset(2016, 5, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Turkey
            yield return new IbanCountry("TR")
            {
                DisplayName = "Turkey",
                EnglishName = "Turkey",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 5, 5),
                    new(AsciiCategory.Digit, 1, 1),
                    new(AsciiCategory.AlphaNumeric, 16, 16),
                }))
                {
                    Example = "TR330006100519786457841326",
                    EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("5!n1!n16!c"), 4)
                {
                    Example = "0006100519786457841326"
                },
                Bank = new BankStructure(new SwiftPattern("5!n"), 4)
                {
                    Example = "00061"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "",
                LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Ukraine
            yield return new IbanCountry("UA")
            {
                DisplayName = "Ukraine",
                EnglishName = "Ukraine",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 6, 6),
                    new(AsciiCategory.AlphaNumeric, 19, 19),
                }))
                {
                    Example = "UA213223130000026007233566001",
                    EffectiveDate = new DateTimeOffset(2016, 2, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("6!n19!c"), 4)
                {
                    Example = "3223130000026007233566001"
                },
                Bank = new BankStructure(new SwiftPattern("6!n"), 4)
                {
                    Example = "322313"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "26007233566001",
                LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Vatican City State
            yield return new IbanCountry("VA")
            {
                DisplayName = "Vatican City State",
                EnglishName = "Vatican City State",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 3, 3),
                    new(AsciiCategory.Digit, 15, 15),
                }))
                {
                    Example = "VA59001123000012345678",
                    EffectiveDate = new DateTimeOffset(2019, 3, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("3!n15!n"), 4)
                {
                    Example = "001123000012345678"
                },
                Bank = new BankStructure(new SwiftPattern("3!n"), 4)
                {
                    Example = "001"
                },
                Sepa = new SepaInfo
                {
                    IsMember = true,
                },
                DomesticAccountNumberExample = "123000012345678",
                LastUpdatedDate = new DateTimeOffset(2018, 12, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Virgin Islands
            yield return new IbanCountry("VG")
            {
                DisplayName = "Virgin Islands",
                EnglishName = "Virgin Islands",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.UppercaseLetter, 4, 4),
                    new(AsciiCategory.Digit, 16, 16),
                }))
                {
                    Example = "VG96VPVG0000012345678901",
                    EffectiveDate = new DateTimeOffset(2012, 4, 1, 0, 0, 0, TimeSpan.Zero)
                },
                Bban = new BbanStructure(new SwiftPattern("4!a16!n"), 4)
                {
                    Example = "VPVG0000012345678901"
                },
                Bank = new BankStructure(new SwiftPattern("4!a"), 4)
                {
                    Example = "VPVG"
                },
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "00000 12 345 678 901",
                LastUpdatedDate = new DateTimeOffset(2014, 6, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // Kosovo
            yield return new IbanCountry("XK")
            {
                DisplayName = "Kosovo",
                EnglishName = "Kosovo",
                Iban = new IbanStructure(new IbanSwiftPattern(new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 4, 4),
                    new(AsciiCategory.Digit, 10, 10),
                    new(AsciiCategory.Digit, 2, 2),
                }))
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
                Sepa = new SepaInfo
                {
                    IsMember = false,
                },
                DomesticAccountNumberExample = "1212 0123456789 06",
                LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
            };

            // ReSharper restore StringLiteralTypo
            // ReSharper restore CommentTypo
        }
    }
}

