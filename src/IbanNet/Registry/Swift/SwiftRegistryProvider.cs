using System.CodeDom.Compiler;
using System.Collections;
using System.Diagnostics;
using IbanNet.Registry.Patterns;

namespace IbanNet.Registry.Swift;

/// <summary>
/// This IBAN registry provider contains IBAN/BBAN/SEPA information for all known IBAN countries.
/// </summary>
/// <remarks>
/// Generated from: swift_iban_registry_202307.r95.txt
/// </remarks>
[GeneratedCode("SwiftRegistryProviderT4", "1.1-r95")]
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
            NativeName = "Andorra",
            EnglishName = "Andorra",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("AD"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "2030200359100100",
            LastUpdatedDate = new DateTimeOffset(2021, 3, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // United Arab Emirates (The)
        yield return new IbanCountry("AE")
        {
            NativeName = "الإمارات العربية المتحدة",
            EnglishName = "United Arab Emirates (The)",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("AE"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "1234567890123456",
            LastUpdatedDate = new DateTimeOffset(2015, 2, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Albania
        yield return new IbanCountry("AL")
        {
            NativeName = "Shqipëri",
            EnglishName = "Albania",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("AL"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "0000000235698741",
            LastUpdatedDate = new DateTimeOffset(2011, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Austria
        yield return new IbanCountry("AT")
        {
            NativeName = "Österreich",
            EnglishName = "Austria",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("AT"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "BLZ 19043 Kto 234573201",
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Azerbaijan
        yield return new IbanCountry("AZ")
        {
            NativeName = "Азәрбајҹан",
            EnglishName = "Azerbaijan",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("AZ"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "NABZ00000000137010001944",
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Bosnia and Herzegovina
        yield return new IbanCountry("BA")
        {
            NativeName = "Bosna i Hercegovina",
            EnglishName = "Bosnia and Herzegovina",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("BA"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "199-044-00012002-79",
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Belgium
        yield return new IbanCountry("BE")
        {
            NativeName = "België",
            EnglishName = "Belgium",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("BE"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "BE68 5390 0754 7034",
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Bulgaria
        yield return new IbanCountry("BG")
        {
            NativeName = "България",
            EnglishName = "Bulgaria",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("BG"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "",
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Bahrain
        yield return new IbanCountry("BH")
        {
            NativeName = "البحرين",
            EnglishName = "Bahrain",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("BH"),
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
            Bank = new BankStructure(new SwiftPattern("4!a"), 4)
            {
                Example = "BMAG"
            },
            Sepa = new SepaInfo
            {
                IsMember = false
            },
            DomesticAccountNumberExample = "00001299123456",
            LastUpdatedDate = new DateTimeOffset(2012, 1, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Burundi
        yield return new IbanCountry("BI")
        {
            NativeName = "Burundi",
            EnglishName = "Burundi",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("BI"),
                new(AsciiCategory.Digit, 2, 2),
                new(AsciiCategory.Digit, 5, 5),
                new(AsciiCategory.Digit, 5, 5),
                new(AsciiCategory.Digit, 11, 11),
                new(AsciiCategory.Digit, 2, 2),
            }))
            {
                Example = "BI4210000100010000332045181",
                EffectiveDate = new DateTimeOffset(2021, 10, 1, 0, 0, 0, TimeSpan.Zero)
            },
            Bban = new BbanStructure(new SwiftPattern("5!n5!n11!n2!n"), 4)
            {
                Example = "10000100010000332045181"
            },
            Bank = new BankStructure(new SwiftPattern("5!n"), 4)
            {
                Example = "10000"
            },
            Branch = new BranchStructure(new SwiftPattern("5!n"), 9)
            {
                Example = "10001"
            },
            Sepa = new SepaInfo
            {
                IsMember = false
            },
            DomesticAccountNumberExample = "00003320451 81",
            LastUpdatedDate = new DateTimeOffset(2021, 10, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Brazil
        yield return new IbanCountry("BR")
        {
            NativeName = "Brasil",
            EnglishName = "Brazil",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("BR"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "0009795493C1",
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Republic of Belarus
        yield return new IbanCountry("BY")
        {
            NativeName = "Беларусь",
            EnglishName = "Republic of Belarus",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("BY"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "3600 0000 0000 0Z00 AB00",
            LastUpdatedDate = new DateTimeOffset(2017, 3, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Switzerland
        yield return new IbanCountry("CH")
        {
            NativeName = "Svizzera",
            EnglishName = "Switzerland",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("CH"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "762 1162-3852.957",
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Costa Rica
        yield return new IbanCountry("CR")
        {
            NativeName = "Costa Rica",
            EnglishName = "Costa Rica",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("CR"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "02001026284066",
            LastUpdatedDate = new DateTimeOffset(2019, 1, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Cyprus
        yield return new IbanCountry("CY")
        {
            NativeName = "Κύπρος",
            EnglishName = "Cyprus",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("CY"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "0000001200527600",
            LastUpdatedDate = new DateTimeOffset(2009, 8, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Czechia
        yield return new IbanCountry("CZ")
        {
            NativeName = "Česko",
            EnglishName = "Czechia",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("CZ"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "19-2000145399/0800",
            LastUpdatedDate = new DateTimeOffset(2016, 12, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Germany
        yield return new IbanCountry("DE")
        {
            NativeName = "Deutschland",
            EnglishName = "Germany",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("DE"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "532013000",
            LastUpdatedDate = new DateTimeOffset(2011, 1, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Djibouti
        yield return new IbanCountry("DJ")
        {
            NativeName = "Yabuuti",
            EnglishName = "Djibouti",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("DJ"),
                new(AsciiCategory.Digit, 2, 2),
                new(AsciiCategory.Digit, 5, 5),
                new(AsciiCategory.Digit, 5, 5),
                new(AsciiCategory.Digit, 11, 11),
                new(AsciiCategory.Digit, 2, 2),
            }))
            {
                Example = "DJ2100010000000154000100186",
                EffectiveDate = new DateTimeOffset(2022, 4, 1, 0, 0, 0, TimeSpan.Zero)
            },
            Bban = new BbanStructure(new SwiftPattern("5!n5!n11!n2!n"), 4)
            {
                Example = "00010000000154000100186"
            },
            Bank = new BankStructure(new SwiftPattern("5!n"), 4)
            {
                Example = "00010"
            },
            Branch = new BranchStructure(new SwiftPattern("5!n"), 9)
            {
                Example = "00000"
            },
            Sepa = new SepaInfo
            {
                IsMember = false
            },
            DomesticAccountNumberExample = "0154000100186",
            LastUpdatedDate = new DateTimeOffset(2022, 5, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Denmark
        yield return new IbanCountry("DK")
        {
            NativeName = "Danmark",
            EnglishName = "Denmark",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("DK"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "0040 0440116243",
            LastUpdatedDate = new DateTimeOffset(2018, 11, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Dominican Republic
        yield return new IbanCountry("DO")
        {
            NativeName = "República Dominicana",
            EnglishName = "Dominican Republic",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("DO"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "00000001212453611324",
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Estonia
        yield return new IbanCountry("EE")
        {
            NativeName = "Eesti",
            EnglishName = "Estonia",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("EE"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "221020145685",
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Egypt
        yield return new IbanCountry("EG")
        {
            NativeName = "مصر",
            EnglishName = "Egypt",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("EG"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "000263180002",
            LastUpdatedDate = new DateTimeOffset(2020, 1, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Spain
        yield return new IbanCountry("ES")
        {
            NativeName = "España",
            EnglishName = "Spain",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("ES"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "2100 0418 45 0200051332",
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Finland
        yield return new IbanCountry("FI")
        {
            NativeName = "Suomi",
            EnglishName = "Finland",
            IncludedCountries = new[]
            {
                    "AX"
            },
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("FI"),
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
                        "AX"
                }
            },
            DomesticAccountNumberExample = "",
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Falkland Islands
        yield return new IbanCountry("FK")
        {
            NativeName = "Falkland Islands",
            EnglishName = "Falkland Islands",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("FK"),
                new(AsciiCategory.Digit, 2, 2),
                new(AsciiCategory.UppercaseLetter, 2, 2),
                new(AsciiCategory.Digit, 12, 12),
            }))
            {
                Example = "FK88SC123456789012",
                EffectiveDate = new DateTimeOffset(2023, 7, 1, 0, 0, 0, TimeSpan.Zero)
            },
            Bban = new BbanStructure(new SwiftPattern("2!a12!n"), 4)
            {
                Example = "SC123456789012"
            },
            Bank = new BankStructure(new SwiftPattern("2!a"), 4)
            {
                Example = "SC"
            },
            Sepa = new SepaInfo
            {
                IsMember = false
            },
            DomesticAccountNumberExample = "123456789012",
            LastUpdatedDate = new DateTimeOffset(2023, 7, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Faroe Islands
        yield return new IbanCountry("FO")
        {
            NativeName = "Føroyar",
            EnglishName = "Faroe Islands",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("FO"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "6460 0001631634",
            LastUpdatedDate = new DateTimeOffset(2017, 2, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // France
        yield return new IbanCountry("FR")
        {
            NativeName = "France",
            EnglishName = "France",
            IncludedCountries = new[]
            {
                    "GF", "GP", "MQ", "RE", "PF", "TF", "YT", "NC", "BL", "MF", "PM", "WF"
            },
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("FR"),
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
            Branch = new BranchStructure(new SwiftPattern("5!n"), 9)
            {
                Example = "01005"
            },
            Sepa = new SepaInfo
            {
                IsMember = true,
                IncludedCountries = new[]
                {
                        "GF", "GP", "MQ", "YT", "RE", "PM", "BL", "MF"
                }
            },
            DomesticAccountNumberExample = "20041 01005 0500013M026 06",
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // United Kingdom
        yield return new IbanCountry("GB")
        {
            NativeName = "United Kingdom",
            EnglishName = "United Kingdom",
            IncludedCountries = new[]
            {
                    "IM", "JE", "GG"
            },
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("GB"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "60-16-13 31926819",
            LastUpdatedDate = new DateTimeOffset(2017, 5, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Georgia
        yield return new IbanCountry("GE")
        {
            NativeName = "საქართველო",
            EnglishName = "Georgia",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("GE"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "0000000101904917",
            LastUpdatedDate = new DateTimeOffset(2023, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Gibraltar
        yield return new IbanCountry("GI")
        {
            NativeName = "Gibraltar",
            EnglishName = "Gibraltar",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("GI"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "0000 00007099 453",
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Greenland
        yield return new IbanCountry("GL")
        {
            NativeName = "Kalaallit Nunaat",
            EnglishName = "Greenland",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("GL"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "6471 0001000206",
            LastUpdatedDate = new DateTimeOffset(2018, 11, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Greece
        yield return new IbanCountry("GR")
        {
            NativeName = "Ελλάδα",
            EnglishName = "Greece",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("GR"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "01250000000012300695",
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Guatemala
        yield return new IbanCountry("GT")
        {
            NativeName = "Guatemala",
            EnglishName = "Guatemala",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("GT"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "01020000001210029690",
            LastUpdatedDate = new DateTimeOffset(2016, 10, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Croatia
        yield return new IbanCountry("HR")
        {
            NativeName = "Hrvatska",
            EnglishName = "Croatia",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("HR"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "1001005-1863000160",
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Hungary
        yield return new IbanCountry("HU")
        {
            NativeName = "Magyarország",
            EnglishName = "Hungary",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("HU"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "11773016-11111018-00000000",
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Ireland
        yield return new IbanCountry("IE")
        {
            NativeName = "Ireland",
            EnglishName = "Ireland",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("IE"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "93-11-52 12345678",
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Israel
        yield return new IbanCountry("IL")
        {
            NativeName = "ישראל",
            EnglishName = "Israel",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("IL"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "10-800-99999999",
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Iraq
        yield return new IbanCountry("IQ")
        {
            NativeName = "العراق",
            EnglishName = "Iraq",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("IQ"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "123456789012",
            LastUpdatedDate = new DateTimeOffset(2016, 11, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Iceland
        yield return new IbanCountry("IS")
        {
            NativeName = "Ísland",
            EnglishName = "Iceland",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("IS"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "0159-26-007654-551073-0339",
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Italy
        yield return new IbanCountry("IT")
        {
            NativeName = "Italia",
            EnglishName = "Italy",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("IT"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "X 05428 11101 000000123456",
            LastUpdatedDate = new DateTimeOffset(2013, 3, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Jordan
        yield return new IbanCountry("JO")
        {
            NativeName = "الأردن",
            EnglishName = "Jordan",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("JO"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "0001310000302",
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Kuwait
        yield return new IbanCountry("KW")
        {
            NativeName = "الكويت",
            EnglishName = "Kuwait",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("KW"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "1234560101",
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Kazakhstan
        yield return new IbanCountry("KZ")
        {
            NativeName = "Қазақстан",
            EnglishName = "Kazakhstan",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("KZ"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "KZ86 125K ZT50 0410 0100",
            LastUpdatedDate = new DateTimeOffset(2016, 3, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Lebanon
        yield return new IbanCountry("LB")
        {
            NativeName = "لبنان",
            EnglishName = "Lebanon",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("LB"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "01 001 901229114",
            LastUpdatedDate = new DateTimeOffset(2010, 1, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Saint Lucia
        yield return new IbanCountry("LC")
        {
            NativeName = "St. Lucia",
            EnglishName = "Saint Lucia",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("LC"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "0001 0001 0012 0012 0002 3015",
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Liechtenstein
        yield return new IbanCountry("LI")
        {
            NativeName = "Liechtenstein",
            EnglishName = "Liechtenstein",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("LI"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "8810 2324013AA",
            LastUpdatedDate = new DateTimeOffset(2012, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Lithuania
        yield return new IbanCountry("LT")
        {
            NativeName = "Lietuva",
            EnglishName = "Lithuania",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("LT"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "",
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Luxembourg
        yield return new IbanCountry("LU")
        {
            NativeName = "Lëtzebuerg",
            EnglishName = "Luxembourg",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("LU"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "",
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Latvia
        yield return new IbanCountry("LV")
        {
            NativeName = "Latvija",
            EnglishName = "Latvia",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("LV"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "LV80 BANK 0000 4351 9500 1",
            LastUpdatedDate = new DateTimeOffset(2009, 1, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Libya
        yield return new IbanCountry("LY")
        {
            NativeName = "ليبيا",
            EnglishName = "Libya",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("LY"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "000020100120361",
            LastUpdatedDate = new DateTimeOffset(2020, 9, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Monaco
        yield return new IbanCountry("MC")
        {
            NativeName = "Monaco",
            EnglishName = "Monaco",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("MC"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "0011111000h",
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Moldova
        yield return new IbanCountry("MD")
        {
            NativeName = "Republica Moldova",
            EnglishName = "Moldova",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("MD"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "000225100013104168",
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Montenegro
        yield return new IbanCountry("ME")
        {
            NativeName = "Crna Gora",
            EnglishName = "Montenegro",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("ME"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "505 0000123456789 51",
            LastUpdatedDate = new DateTimeOffset(2010, 5, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Macedonia
        yield return new IbanCountry("MK")
        {
            NativeName = "Северна Македонија",
            EnglishName = "Macedonia",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("MK"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "MK07 300 0000000424 25",
            LastUpdatedDate = new DateTimeOffset(2011, 1, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Mongolia
        yield return new IbanCountry("MN")
        {
            NativeName = "Монгол",
            EnglishName = "Mongolia",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("MN"),
                new(AsciiCategory.Digit, 2, 2),
                new(AsciiCategory.Digit, 4, 4),
                new(AsciiCategory.Digit, 12, 12),
            }))
            {
                Example = "MN121234123456789123",
                EffectiveDate = new DateTimeOffset(2023, 4, 1, 0, 0, 0, TimeSpan.Zero)
            },
            Bban = new BbanStructure(new SwiftPattern("4!n12!n"), 4)
            {
                Example = "1234123456789123"
            },
            Bank = new BankStructure(new SwiftPattern("4!n"), 4)
            {
                Example = "1234"
            },
            Sepa = new SepaInfo
            {
                IsMember = false
            },
            DomesticAccountNumberExample = "1234 5678 9123",
            LastUpdatedDate = new DateTimeOffset(2023, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Mauritania
        yield return new IbanCountry("MR")
        {
            NativeName = "موريتانيا",
            EnglishName = "Mauritania",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("MR"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "00020 00101 00001234567 53",
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Malta
        yield return new IbanCountry("MT")
        {
            NativeName = "Malta",
            EnglishName = "Malta",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("MT"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "12345MTLCAST001S",
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Mauritius
        yield return new IbanCountry("MU")
        {
            NativeName = "Mauritius",
            EnglishName = "Mauritius",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("MU"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "MU17 BOMM 0101 1010 3030 0200 000M UR",
            LastUpdatedDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Nicaragua
        yield return new IbanCountry("NI")
        {
            NativeName = "Nicaragua",
            EnglishName = "Nicaragua",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("NI"),
                new(AsciiCategory.Digit, 2, 2),
                new(AsciiCategory.UppercaseLetter, 4, 4),
                new(AsciiCategory.Digit, 20, 20),
            }))
            {
                Example = "NI45BAPR00000013000003558124",
                EffectiveDate = new DateTimeOffset(2023, 4, 1, 0, 0, 0, TimeSpan.Zero)
            },
            Bban = new BbanStructure(new SwiftPattern("4!a20!n"), 4)
            {
                Example = "BAPR00000013000003558124"
            },
            Bank = new BankStructure(new SwiftPattern("4!a"), 4)
            {
                Example = "BAPR"
            },
            Sepa = new SepaInfo
            {
                IsMember = false
            },
            DomesticAccountNumberExample = "00000013000003558124",
            LastUpdatedDate = new DateTimeOffset(2023, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Netherlands (The)
        yield return new IbanCountry("NL")
        {
            NativeName = "Nederland",
            EnglishName = "Netherlands (The)",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("NL"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "041 71 64 300",
            LastUpdatedDate = new DateTimeOffset(2020, 9, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Norway
        yield return new IbanCountry("NO")
        {
            NativeName = "Noreg",
            EnglishName = "Norway",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("NO"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "8601 11 17947",
            LastUpdatedDate = new DateTimeOffset(2009, 8, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Pakistan
        yield return new IbanCountry("PK")
        {
            NativeName = "پاکستان",
            EnglishName = "Pakistan",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("PK"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "00260101036360",
            LastUpdatedDate = new DateTimeOffset(2012, 12, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Poland
        yield return new IbanCountry("PL")
        {
            NativeName = "Polska",
            EnglishName = "Poland",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("PL"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "61 1090 1014 0000 0712 1981 2874",
            LastUpdatedDate = new DateTimeOffset(2016, 10, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Palestine, State of
        yield return new IbanCountry("PS")
        {
            NativeName = "السلطة الفلسطينية",
            EnglishName = "Palestine, State of",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("PS"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "400123456702",
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Portugal
        yield return new IbanCountry("PT")
        {
            NativeName = "Portugal",
            EnglishName = "Portugal",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("PT"),
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
                        "AZ", "MA"
                }
            },
            DomesticAccountNumberExample = "0002.0123.12345678901.54",
            LastUpdatedDate = new DateTimeOffset(2014, 1, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Qatar
        yield return new IbanCountry("QA")
        {
            NativeName = "قطر",
            EnglishName = "Qatar",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("QA"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "00001234567890ABCDEFG",
            LastUpdatedDate = new DateTimeOffset(2014, 1, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Romania
        yield return new IbanCountry("RO")
        {
            NativeName = "România",
            EnglishName = "Romania",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("RO"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "RO49 AAAA 1B31 0075 9384 0000",
            LastUpdatedDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Serbia
        yield return new IbanCountry("RS")
        {
            NativeName = "Srbija",
            EnglishName = "Serbia",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("RS"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "260-0056010016113-79",
            LastUpdatedDate = new DateTimeOffset(2017, 3, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Saudi Arabia
        yield return new IbanCountry("SA")
        {
            NativeName = "المملكة العربية السعودية",
            EnglishName = "Saudi Arabia",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("SA"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "608010167519",
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Seychelles
        yield return new IbanCountry("SC")
        {
            NativeName = "Seychelles",
            EnglishName = "Seychelles",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("SC"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "0000000000001497",
            LastUpdatedDate = new DateTimeOffset(2019, 10, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Sudan
        yield return new IbanCountry("SD")
        {
            NativeName = "السودان",
            EnglishName = "Sudan",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("SD"),
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
            Sepa = new SepaInfo
            {
                IsMember = false
            },
            DomesticAccountNumberExample = "010501234001",
            LastUpdatedDate = new DateTimeOffset(2021, 10, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Sweden
        yield return new IbanCountry("SE")
        {
            NativeName = "Sverige",
            EnglishName = "Sweden",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("SE"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "1234 12 3456 1",
            LastUpdatedDate = new DateTimeOffset(2009, 8, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Slovenia
        yield return new IbanCountry("SI")
        {
            NativeName = "Slovenija",
            EnglishName = "Slovenia",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("SI"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "2633 0001 2039 086",
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Slovakia
        yield return new IbanCountry("SK")
        {
            NativeName = "Slovensko",
            EnglishName = "Slovakia",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("SK"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "19-8742637541/1200",
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // San Marino
        yield return new IbanCountry("SM")
        {
            NativeName = "San Marino",
            EnglishName = "San Marino",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("SM"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "",
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Somalia
        yield return new IbanCountry("SO")
        {
            NativeName = "الصومال",
            EnglishName = "Somalia",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("SO"),
                new(AsciiCategory.Digit, 2, 2),
                new(AsciiCategory.Digit, 4, 4),
                new(AsciiCategory.Digit, 3, 3),
                new(AsciiCategory.Digit, 12, 12),
            }))
            {
                Example = "SO211000001001000100141",
                EffectiveDate = new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero)
            },
            Bban = new BbanStructure(new SwiftPattern("4!n3!n12!n"), 4)
            {
                Example = "1000001001000100141"
            },
            Bank = new BankStructure(new SwiftPattern("4!n"), 4)
            {
                Example = "1000"
            },
            Branch = new BranchStructure(new SwiftPattern("3!n"), 8)
            {
                Example = "001"
            },
            Sepa = new SepaInfo
            {
                IsMember = false
            },
            DomesticAccountNumberExample = "001000100141",
            LastUpdatedDate = new DateTimeOffset(2023, 2, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Sao Tome and Principe
        yield return new IbanCountry("ST")
        {
            NativeName = "São Tomé e Príncipe",
            EnglishName = "Sao Tome and Principe",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("ST"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "0051845310146",
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // El Salvador
        yield return new IbanCountry("SV")
        {
            NativeName = "El Salvador",
            EnglishName = "El Salvador",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("SV"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "00000000000000700025",
            LastUpdatedDate = new DateTimeOffset(2021, 3, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Timor-Leste
        yield return new IbanCountry("TL")
        {
            NativeName = "Timor-Leste",
            EnglishName = "Timor-Leste",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("TL"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "008 00123456789101 57",
            LastUpdatedDate = new DateTimeOffset(2014, 11, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Tunisia
        yield return new IbanCountry("TN")
        {
            NativeName = "تونس",
            EnglishName = "Tunisia",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("TN"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "10 006 0351835984788 31",
            LastUpdatedDate = new DateTimeOffset(2016, 5, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Turkey
        yield return new IbanCountry("TR")
        {
            NativeName = "Türkiye",
            EnglishName = "Turkey",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("TR"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "",
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Ukraine
        yield return new IbanCountry("UA")
        {
            NativeName = "Україна",
            EnglishName = "Ukraine",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("UA"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "26007233566001",
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Vatican City State
        yield return new IbanCountry("VA")
        {
            NativeName = "Città del Vaticano",
            EnglishName = "Vatican City State",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("VA"),
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
                IsMember = true
            },
            DomesticAccountNumberExample = "123000012345678",
            LastUpdatedDate = new DateTimeOffset(2018, 12, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Virgin Islands
        yield return new IbanCountry("VG")
        {
            NativeName = "British Virgin Islands",
            EnglishName = "Virgin Islands",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("VG"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "00000 12 345 678 901",
            LastUpdatedDate = new DateTimeOffset(2014, 6, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Kosovo
        yield return new IbanCountry("XK")
        {
            NativeName = "Kosovë",
            EnglishName = "Kosovo",
            Iban = new IbanStructure(new SwiftPattern(new PatternToken[]
            {
                new("XK"),
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
                IsMember = false
            },
            DomesticAccountNumberExample = "1212 0123456789 06",
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // ReSharper restore StringLiteralTypo
        // ReSharper restore CommentTypo
    }
}
