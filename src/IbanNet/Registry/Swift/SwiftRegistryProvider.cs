using System.CodeDom.Compiler;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using IbanNet.Extensions;

namespace IbanNet.Registry.Swift;

/// <summary>
/// This IBAN registry provider contains IBAN/BBAN/SEPA information for all known IBAN countries.
/// </summary>
/// <remarks>
/// Generated from: swift_iban_registry_202412.r99.txt
/// </remarks>
[GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
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
            Iban = new IbanStructure(new Patterns.AD())
            {
                Example = "AD1200012030200359100100"
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
            LastUpdatedDate = new DateTimeOffset(2021, 3, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // United Arab Emirates (The)
        yield return new IbanCountry("AE")
        {
            NativeName = "الإمارات العربية المتحدة",
            EnglishName = "United Arab Emirates (The)",
            Iban = new IbanStructure(new Patterns.AE())
            {
                Example = "AE070331234567890123456"
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
            LastUpdatedDate = new DateTimeOffset(2015, 2, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2011, 10, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Albania
        yield return new IbanCountry("AL")
        {
            NativeName = "Shqipëri",
            EnglishName = "Albania",
            Iban = new IbanStructure(new Patterns.AL())
            {
                Example = "AL47212110090000000235698741"
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
            LastUpdatedDate = new DateTimeOffset(2011, 4, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2009, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Austria
        yield return new IbanCountry("AT")
        {
            NativeName = "Österreich",
            EnglishName = "Austria",
            Iban = new IbanStructure(new Patterns.AT())
            {
                Example = "AT611904300234573201"
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
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Azerbaijan
        yield return new IbanCountry("AZ")
        {
            NativeName = "Азәрбајҹан",
            EnglishName = "Azerbaijan",
            Iban = new IbanStructure(new Patterns.AZ())
            {
                Example = "AZ21NABZ00000000137010001944"
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
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2013, 1, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Bosnia and Herzegovina
        yield return new IbanCountry("BA")
        {
            NativeName = "Bosna i Hercegovina",
            EnglishName = "Bosnia and Herzegovina",
            Iban = new IbanStructure(new Patterns.BA())
            {
                Example = "BA391290079401028494"
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
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Belgium
        yield return new IbanCountry("BE")
        {
            NativeName = "België",
            EnglishName = "Belgium",
            Iban = new IbanStructure(new Patterns.BE())
            {
                Example = "BE68539007547034"
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
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Bulgaria
        yield return new IbanCountry("BG")
        {
            NativeName = "България",
            EnglishName = "Bulgaria",
            Iban = new IbanStructure(new Patterns.BG())
            {
                Example = "BG80BNBG96611020345678"
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
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Bahrain
        yield return new IbanCountry("BH")
        {
            NativeName = "البحرين",
            EnglishName = "Bahrain",
            Iban = new IbanStructure(new Patterns.BH())
            {
                Example = "BH67BMAG00001299123456"
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
            LastUpdatedDate = new DateTimeOffset(2012, 1, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2012, 1, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Burundi
        yield return new IbanCountry("BI")
        {
            NativeName = "Burundi",
            EnglishName = "Burundi",
            Iban = new IbanStructure(new Patterns.BI())
            {
                Example = "BI4210000100010000332045181"
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
            LastUpdatedDate = new DateTimeOffset(2021, 10, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2021, 10, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Brazil
        yield return new IbanCountry("BR")
        {
            NativeName = "Brasil",
            EnglishName = "Brazil",
            Iban = new IbanStructure(new Patterns.BR())
            {
                Example = "BR1800360305000010009795493C1"
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
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2013, 7, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Republic of Belarus
        yield return new IbanCountry("BY")
        {
            NativeName = "Беларусь",
            EnglishName = "Republic of Belarus",
            Iban = new IbanStructure(new Patterns.BY())
            {
                Example = "BY13NBRB3600900000002Z00AB00"
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
            LastUpdatedDate = new DateTimeOffset(2024, 2, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2017, 7, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Switzerland
        yield return new IbanCountry("CH")
        {
            NativeName = "Svizzera",
            EnglishName = "Switzerland",
            Iban = new IbanStructure(new Patterns.CH())
            {
                Example = "CH9300762011623852957"
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
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Costa Rica
        yield return new IbanCountry("CR")
        {
            NativeName = "Costa Rica",
            EnglishName = "Costa Rica",
            Iban = new IbanStructure(new Patterns.CR())
            {
                Example = "CR05015202001026284066"
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
            LastUpdatedDate = new DateTimeOffset(2019, 1, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2011, 6, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Cyprus
        yield return new IbanCountry("CY")
        {
            NativeName = "Κύπρος",
            EnglishName = "Cyprus",
            Iban = new IbanStructure(new Patterns.CY())
            {
                Example = "CY17002001280000001200527600"
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
            LastUpdatedDate = new DateTimeOffset(2009, 8, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Czechia
        yield return new IbanCountry("CZ")
        {
            NativeName = "Česko",
            EnglishName = "Czechia",
            Iban = new IbanStructure(new Patterns.CZ())
            {
                Example = "CZ6508000000192000145399"
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
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Germany
        yield return new IbanCountry("DE")
        {
            NativeName = "Deutschland",
            EnglishName = "Germany",
            Iban = new IbanStructure(new Patterns.DE())
            {
                Example = "DE89370400440532013000"
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
            LastUpdatedDate = new DateTimeOffset(2011, 1, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 7, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Djibouti
        yield return new IbanCountry("DJ")
        {
            NativeName = "Yabuuti",
            EnglishName = "Djibouti",
            Iban = new IbanStructure(new Patterns.DJ())
            {
                Example = "DJ2100010000000154000100186"
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
            LastUpdatedDate = new DateTimeOffset(2022, 5, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2022, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Denmark
        yield return new IbanCountry("DK")
        {
            NativeName = "Danmark",
            EnglishName = "Denmark",
            Iban = new IbanStructure(new Patterns.DK())
            {
                Example = "DK5000400440116243"
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
            LastUpdatedDate = new DateTimeOffset(2018, 11, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Dominican Republic
        yield return new IbanCountry("DO")
        {
            NativeName = "República Dominicana",
            EnglishName = "Dominican Republic",
            Iban = new IbanStructure(new Patterns.DO())
            {
                Example = "DO28BAGR00000001212453611324"
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
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2010, 12, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Estonia
        yield return new IbanCountry("EE")
        {
            NativeName = "Eesti",
            EnglishName = "Estonia",
            Iban = new IbanStructure(new Patterns.EE())
            {
                Example = "EE382200221020145685"
            },
            Bban = new BbanStructure(new SwiftPattern("2!n14!n"), 4)
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
            LastUpdatedDate = new DateTimeOffset(2024, 12, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Egypt
        yield return new IbanCountry("EG")
        {
            NativeName = "مصر",
            EnglishName = "Egypt",
            Iban = new IbanStructure(new Patterns.EG())
            {
                Example = "EG380019000500000000263180002"
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
            LastUpdatedDate = new DateTimeOffset(2020, 1, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2021, 1, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Spain
        yield return new IbanCountry("ES")
        {
            NativeName = "España",
            EnglishName = "Spain",
            Iban = new IbanStructure(new Patterns.ES())
            {
                Example = "ES9121000418450200051332"
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
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Finland
        yield return new IbanCountry("FI")
        {
            NativeName = "Suomi",
            EnglishName = "Finland",
            IncludedCountries =
            [
                    "AX"
            ],
            Iban = new IbanStructure(new Patterns.FI())
            {
                Example = "FI2112345600000785"
            },
            Bban = new BbanStructure(new SwiftPattern("6!n8!n"), 4)
            {
                Example = "12345600000785"
            },
            Bank = new BankStructure(new SwiftPattern("6!n"), 4)
            {
                Example = "123456"
            },
            Sepa = new SepaInfo
            {
                IsMember = true,
                IncludedCountries =
                [
                        "AX"
                ]
            },
            DomesticAccountNumberExample = "",
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2011, 12, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Falkland Islands
        yield return new IbanCountry("FK")
        {
            NativeName = "Falkland Islands",
            EnglishName = "Falkland Islands",
            Iban = new IbanStructure(new Patterns.FK())
            {
                Example = "FK88SC123456789012"
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
            LastUpdatedDate = new DateTimeOffset(2023, 7, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2023, 7, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Faroe Islands
        yield return new IbanCountry("FO")
        {
            NativeName = "Føroyar",
            EnglishName = "Faroe Islands",
            Iban = new IbanStructure(new Patterns.FO())
            {
                Example = "FO6264600001631634"
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
            LastUpdatedDate = new DateTimeOffset(2017, 2, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // France
        yield return new IbanCountry("FR")
        {
            NativeName = "France",
            EnglishName = "France",
            IncludedCountries =
            [
                    "GF", "GP", "MQ", "RE", "PF", "TF", "YT", "NC", "BL", "MF", "PM", "WF"
            ],
            Iban = new IbanStructure(new Patterns.FR())
            {
                Example = "FR1420041010050500013M02606"
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
                IncludedCountries =
                [
                        "GF", "GP", "MQ", "YT", "RE", "PM", "BL", "MF"
                ]
            },
            DomesticAccountNumberExample = "20041 01005 0500013M026 06",
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // United Kingdom
        yield return new IbanCountry("GB")
        {
            NativeName = "United Kingdom",
            EnglishName = "United Kingdom",
            IncludedCountries =
            [
                    "IM", "JE", "GG"
            ],
            Iban = new IbanStructure(new Patterns.GB())
            {
                Example = "GB29NWBK60161331926819"
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
            LastUpdatedDate = new DateTimeOffset(2017, 5, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Georgia
        yield return new IbanCountry("GE")
        {
            NativeName = "საქართველო",
            EnglishName = "Georgia",
            Iban = new IbanStructure(new Patterns.GE())
            {
                Example = "GE29NB0000000101904917"
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
            LastUpdatedDate = new DateTimeOffset(2023, 4, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2010, 5, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Gibraltar
        yield return new IbanCountry("GI")
        {
            NativeName = "Gibraltar",
            EnglishName = "Gibraltar",
            Iban = new IbanStructure(new Patterns.GI())
            {
                Example = "GI75NWBK000000007099453"
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
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Greenland
        yield return new IbanCountry("GL")
        {
            NativeName = "Kalaallit Nunaat",
            EnglishName = "Greenland",
            Iban = new IbanStructure(new Patterns.GL())
            {
                Example = "GL8964710001000206"
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
            LastUpdatedDate = new DateTimeOffset(2017, 2, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Greece
        yield return new IbanCountry("GR")
        {
            NativeName = "Ελλάδα",
            EnglishName = "Greece",
            Iban = new IbanStructure(new Patterns.GR())
            {
                Example = "GR1601101250000000012300695"
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
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Guatemala
        yield return new IbanCountry("GT")
        {
            NativeName = "Guatemala",
            EnglishName = "Guatemala",
            Iban = new IbanStructure(new Patterns.GT())
            {
                Example = "GT82TRAJ01020000001210029690"
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
            LastUpdatedDate = new DateTimeOffset(2016, 10, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Honduras
        yield return new IbanCountry("HN")
        {
            NativeName = "Honduras",
            EnglishName = "Honduras",
            Iban = new IbanStructure(new Patterns.HN())
            {
                Example = "HN88CABF00000000000250005469"
            },
            Bban = new BbanStructure(new SwiftPattern("4!a20!n"), 4)
            {
                Example = "CABF00000000000250005469"
            },
            Bank = new BankStructure(new SwiftPattern("4!a"), 4)
            {
                Example = "CABF"
            },
            Sepa = new SepaInfo
            {
                IsMember = false
            },
            DomesticAccountNumberExample = "250005469",
            LastUpdatedDate = new DateTimeOffset(2024, 12, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2024, 10, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Croatia
        yield return new IbanCountry("HR")
        {
            NativeName = "Hrvatska",
            EnglishName = "Croatia",
            Iban = new IbanStructure(new Patterns.HR())
            {
                Example = "HR1210010051863000160"
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
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Hungary
        yield return new IbanCountry("HU")
        {
            NativeName = "Magyarország",
            EnglishName = "Hungary",
            Iban = new IbanStructure(new Patterns.HU())
            {
                Example = "HU42117730161111101800000000"
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
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Ireland
        yield return new IbanCountry("IE")
        {
            NativeName = "Ireland",
            EnglishName = "Ireland",
            Iban = new IbanStructure(new Patterns.IE())
            {
                Example = "IE29AIBK93115212345678"
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
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Israel
        yield return new IbanCountry("IL")
        {
            NativeName = "ישראל",
            EnglishName = "Israel",
            Iban = new IbanStructure(new Patterns.IL())
            {
                Example = "IL620108000000099999999"
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
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 7, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Iraq
        yield return new IbanCountry("IQ")
        {
            NativeName = "العراق",
            EnglishName = "Iraq",
            Iban = new IbanStructure(new Patterns.IQ())
            {
                Example = "IQ98NBIQ850123456789012"
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
            LastUpdatedDate = new DateTimeOffset(2016, 11, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2017, 1, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Iceland
        yield return new IbanCountry("IS")
        {
            NativeName = "Ísland",
            EnglishName = "Iceland",
            Iban = new IbanStructure(new Patterns.IS())
            {
                Example = "IS140159260076545510730339"
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
                IsMember = true
            },
            DomesticAccountNumberExample = "0159-26-007654-551073-0339",
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Italy
        yield return new IbanCountry("IT")
        {
            NativeName = "Italia",
            EnglishName = "Italy",
            Iban = new IbanStructure(new Patterns.IT())
            {
                Example = "IT60X0542811101000000123456"
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
            LastUpdatedDate = new DateTimeOffset(2013, 3, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 7, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Jordan
        yield return new IbanCountry("JO")
        {
            NativeName = "الأردن",
            EnglishName = "Jordan",
            Iban = new IbanStructure(new Patterns.JO())
            {
                Example = "JO94CBJO0010000000000131000302"
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
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2014, 2, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Kuwait
        yield return new IbanCountry("KW")
        {
            NativeName = "الكويت",
            EnglishName = "Kuwait",
            Iban = new IbanStructure(new Patterns.KW())
            {
                Example = "KW81CBKU0000000000001234560101"
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
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2011, 1, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Kazakhstan
        yield return new IbanCountry("KZ")
        {
            NativeName = "Қазақстан",
            EnglishName = "Kazakhstan",
            Iban = new IbanStructure(new Patterns.KZ())
            {
                Example = "KZ86125KZT5004100100"
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
            LastUpdatedDate = new DateTimeOffset(2016, 3, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2010, 9, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Lebanon
        yield return new IbanCountry("LB")
        {
            NativeName = "لبنان",
            EnglishName = "Lebanon",
            Iban = new IbanStructure(new Patterns.LB())
            {
                Example = "LB62099900000001001901229114"
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
            LastUpdatedDate = new DateTimeOffset(2010, 1, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2010, 1, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Saint Lucia
        yield return new IbanCountry("LC")
        {
            NativeName = "St. Lucia",
            EnglishName = "Saint Lucia",
            Iban = new IbanStructure(new Patterns.LC())
            {
                Example = "LC55HEMM000100010012001200023015"
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
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Liechtenstein
        yield return new IbanCountry("LI")
        {
            NativeName = "Liechtenstein",
            EnglishName = "Liechtenstein",
            Iban = new IbanStructure(new Patterns.LI())
            {
                Example = "LI21088100002324013AA"
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
            LastUpdatedDate = new DateTimeOffset(2012, 4, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Lithuania
        yield return new IbanCountry("LT")
        {
            NativeName = "Lietuva",
            EnglishName = "Lithuania",
            Iban = new IbanStructure(new Patterns.LT())
            {
                Example = "LT121000011101001000"
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
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Luxembourg
        yield return new IbanCountry("LU")
        {
            NativeName = "Lëtzebuerg",
            EnglishName = "Luxembourg",
            Iban = new IbanStructure(new Patterns.LU())
            {
                Example = "LU280019400644750000"
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
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Latvia
        yield return new IbanCountry("LV")
        {
            NativeName = "Latvija",
            EnglishName = "Latvia",
            Iban = new IbanStructure(new Patterns.LV())
            {
                Example = "LV80BANK0000435195001"
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
            LastUpdatedDate = new DateTimeOffset(2009, 1, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Libya
        yield return new IbanCountry("LY")
        {
            NativeName = "ليبيا",
            EnglishName = "Libya",
            Iban = new IbanStructure(new Patterns.LY())
            {
                Example = "LY83002048000020100120361"
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
            LastUpdatedDate = new DateTimeOffset(2020, 9, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2021, 1, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Monaco
        yield return new IbanCountry("MC")
        {
            NativeName = "Monaco",
            EnglishName = "Monaco",
            Iban = new IbanStructure(new Patterns.MC())
            {
                Example = "MC5811222000010123456789030"
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
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2008, 1, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Moldova
        yield return new IbanCountry("MD")
        {
            NativeName = "Republica Moldova",
            EnglishName = "Moldova",
            Iban = new IbanStructure(new Patterns.MD())
            {
                Example = "MD24AG000225100013104168"
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
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2016, 1, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Montenegro
        yield return new IbanCountry("ME")
        {
            NativeName = "Crna Gora",
            EnglishName = "Montenegro",
            Iban = new IbanStructure(new Patterns.ME())
            {
                Example = "ME25505000012345678951"
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
            LastUpdatedDate = new DateTimeOffset(2010, 5, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Macedonia
        yield return new IbanCountry("MK")
        {
            NativeName = "Северна Македонија",
            EnglishName = "Macedonia",
            Iban = new IbanStructure(new Patterns.MK())
            {
                Example = "MK07250120000058984"
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
            LastUpdatedDate = new DateTimeOffset(2011, 1, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Mongolia
        yield return new IbanCountry("MN")
        {
            NativeName = "Монгол",
            EnglishName = "Mongolia",
            Iban = new IbanStructure(new Patterns.MN())
            {
                Example = "MN121234123456789123"
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
            LastUpdatedDate = new DateTimeOffset(2023, 4, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2023, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Mauritania
        yield return new IbanCountry("MR")
        {
            NativeName = "موريتانيا",
            EnglishName = "Mauritania",
            Iban = new IbanStructure(new Patterns.MR())
            {
                Example = "MR1300020001010000123456753"
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
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2012, 1, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Malta
        yield return new IbanCountry("MT")
        {
            NativeName = "Malta",
            EnglishName = "Malta",
            Iban = new IbanStructure(new Patterns.MT())
            {
                Example = "MT84MALT011000012345MTLCAST001S"
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
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Mauritius
        yield return new IbanCountry("MU")
        {
            NativeName = "Mauritius",
            EnglishName = "Mauritius",
            Iban = new IbanStructure(new Patterns.MU())
            {
                Example = "MU17BOMM0101101030300200000MUR"
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
            LastUpdatedDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Nicaragua
        yield return new IbanCountry("NI")
        {
            NativeName = "Nicaragua",
            EnglishName = "Nicaragua",
            Iban = new IbanStructure(new Patterns.NI())
            {
                Example = "NI45BAPR00000013000003558124"
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
            LastUpdatedDate = new DateTimeOffset(2024, 12, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2023, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Netherlands (The)
        yield return new IbanCountry("NL")
        {
            NativeName = "Nederland",
            EnglishName = "Netherlands (The)",
            Iban = new IbanStructure(new Patterns.NL())
            {
                Example = "NL91ABNA0417164300"
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
            LastUpdatedDate = new DateTimeOffset(2020, 9, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Norway
        yield return new IbanCountry("NO")
        {
            NativeName = "Noreg",
            EnglishName = "Norway",
            Iban = new IbanStructure(new Patterns.NO())
            {
                Example = "NO9386011117947"
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
            LastUpdatedDate = new DateTimeOffset(2009, 8, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Oman
        yield return new IbanCountry("OM")
        {
            NativeName = "عمان",
            EnglishName = "Oman",
            Iban = new IbanStructure(new Patterns.OM())
            {
                Example = "OM810180000001299123456"
            },
            Bban = new BbanStructure(new SwiftPattern("3!n16!c"), 4)
            {
                Example = "0180000001299123456"
            },
            Bank = new BankStructure(new SwiftPattern("3!n"), 4)
            {
                Example = "018"
            },
            Sepa = new SepaInfo
            {
                IsMember = false
            },
            DomesticAccountNumberExample = "0000001299123456",
            LastUpdatedDate = new DateTimeOffset(2024, 2, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2024, 3, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Pakistan
        yield return new IbanCountry("PK")
        {
            NativeName = "پاکستان",
            EnglishName = "Pakistan",
            Iban = new IbanStructure(new Patterns.PK())
            {
                Example = "PK36SCBL0000001123456702"
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
            LastUpdatedDate = new DateTimeOffset(2012, 12, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2012, 12, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Poland
        yield return new IbanCountry("PL")
        {
            NativeName = "Polska",
            EnglishName = "Poland",
            Iban = new IbanStructure(new Patterns.PL())
            {
                Example = "PL61109010140000071219812874"
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
            LastUpdatedDate = new DateTimeOffset(2016, 10, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Palestine, State of
        yield return new IbanCountry("PS")
        {
            NativeName = "السلطة الفلسطينية",
            EnglishName = "Palestine, State of",
            Iban = new IbanStructure(new Patterns.PS())
            {
                Example = "PS92PALS000000000400123456702"
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
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2012, 7, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Portugal
        yield return new IbanCountry("PT")
        {
            NativeName = "Portugal",
            EnglishName = "Portugal",
            Iban = new IbanStructure(new Patterns.PT())
            {
                Example = "PT50000201231234567890154"
            },
            Bban = new BbanStructure(new SwiftPattern("4!n4!n11!n2!n"), 4)
            {
                Example = "000201231234567890154"
            },
            Bank = new BankStructure(new SwiftPattern("4!n"), 4)
            {
                Example = "0002"
            },
            Branch = new BranchStructure(new SwiftPattern("4!n"), 8)
            {
                Example = "0123"
            },
            Sepa = new SepaInfo
            {
                IsMember = true,
                IncludedCountries =
                [
                        "AZ", "MA"
                ]
            },
            DomesticAccountNumberExample = "0002.0123.12345678901.54",
            LastUpdatedDate = new DateTimeOffset(2024, 7, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Qatar
        yield return new IbanCountry("QA")
        {
            NativeName = "قطر",
            EnglishName = "Qatar",
            Iban = new IbanStructure(new Patterns.QA())
            {
                Example = "QA58DOHB00001234567890ABCDEFG"
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
            LastUpdatedDate = new DateTimeOffset(2014, 1, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2014, 1, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Romania
        yield return new IbanCountry("RO")
        {
            NativeName = "România",
            EnglishName = "Romania",
            Iban = new IbanStructure(new Patterns.RO())
            {
                Example = "RO49AAAA1B31007593840000"
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
            LastUpdatedDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Serbia
        yield return new IbanCountry("RS")
        {
            NativeName = "Srbija",
            EnglishName = "Serbia",
            Iban = new IbanStructure(new Patterns.RS())
            {
                Example = "RS35260005601001611379"
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
            LastUpdatedDate = new DateTimeOffset(2017, 3, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Saudi Arabia
        yield return new IbanCountry("SA")
        {
            NativeName = "المملكة العربية السعودية",
            EnglishName = "Saudi Arabia",
            Iban = new IbanStructure(new Patterns.SA())
            {
                Example = "SA0380000000608010167519"
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
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2016, 7, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Seychelles
        yield return new IbanCountry("SC")
        {
            NativeName = "Seychelles",
            EnglishName = "Seychelles",
            Iban = new IbanStructure(new Patterns.SC())
            {
                Example = "SC18SSCB11010000000000001497USD"
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
            LastUpdatedDate = new DateTimeOffset(2019, 10, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2016, 10, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Sudan
        yield return new IbanCountry("SD")
        {
            NativeName = "السودان",
            EnglishName = "Sudan",
            Iban = new IbanStructure(new Patterns.SD())
            {
                Example = "SD2129010501234001"
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
            LastUpdatedDate = new DateTimeOffset(2021, 10, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2021, 7, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Sweden
        yield return new IbanCountry("SE")
        {
            NativeName = "Sverige",
            EnglishName = "Sweden",
            Iban = new IbanStructure(new Patterns.SE())
            {
                Example = "SE4550000000058398257466"
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
            LastUpdatedDate = new DateTimeOffset(2009, 8, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Slovenia
        yield return new IbanCountry("SI")
        {
            NativeName = "Slovenija",
            EnglishName = "Slovenia",
            Iban = new IbanStructure(new Patterns.SI())
            {
                Example = "SI56263300012039086"
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
            LastUpdatedDate = new DateTimeOffset(2016, 10, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Slovakia
        yield return new IbanCountry("SK")
        {
            NativeName = "Slovensko",
            EnglishName = "Slovakia",
            Iban = new IbanStructure(new Patterns.SK())
            {
                Example = "SK3112000000198742637541"
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
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // San Marino
        yield return new IbanCountry("SM")
        {
            NativeName = "San Marino",
            EnglishName = "San Marino",
            Iban = new IbanStructure(new Patterns.SM())
            {
                Example = "SM86U0322509800000000270100"
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
            LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 8, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Somalia
        yield return new IbanCountry("SO")
        {
            NativeName = "الصومال",
            EnglishName = "Somalia",
            Iban = new IbanStructure(new Patterns.SO())
            {
                Example = "SO211000001001000100141"
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
            LastUpdatedDate = new DateTimeOffset(2023, 2, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Sao Tome and Principe
        yield return new IbanCountry("ST")
        {
            NativeName = "São Tomé e Príncipe",
            EnglishName = "Sao Tome and Principe",
            Iban = new IbanStructure(new Patterns.ST())
            {
                Example = "ST23000100010051845310146"
            },
            Bban = new BbanStructure(new SwiftPattern("8!n11!n2!n"), 4)
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
            LastUpdatedDate = new DateTimeOffset(2020, 5, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2020, 3, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // El Salvador
        yield return new IbanCountry("SV")
        {
            NativeName = "El Salvador",
            EnglishName = "El Salvador",
            Iban = new IbanStructure(new Patterns.SV())
            {
                Example = "SV62CENR00000000000000700025"
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
            LastUpdatedDate = new DateTimeOffset(2021, 3, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2016, 12, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Timor-Leste
        yield return new IbanCountry("TL")
        {
            NativeName = "Timor-Leste",
            EnglishName = "Timor-Leste",
            Iban = new IbanStructure(new Patterns.TL())
            {
                Example = "TL380080012345678910157"
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
            LastUpdatedDate = new DateTimeOffset(2014, 11, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2014, 9, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Tunisia
        yield return new IbanCountry("TN")
        {
            NativeName = "تونس",
            EnglishName = "Tunisia",
            Iban = new IbanStructure(new Patterns.TN())
            {
                Example = "TN5910006035183598478831"
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
            LastUpdatedDate = new DateTimeOffset(2016, 5, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Turkey
        yield return new IbanCountry("TR")
        {
            NativeName = "Türkiye",
            EnglishName = "Turkey",
            Iban = new IbanStructure(new Patterns.TR())
            {
                Example = "TR330006100519786457841326"
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
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Ukraine
        yield return new IbanCountry("UA")
        {
            NativeName = "Україна",
            EnglishName = "Ukraine",
            Iban = new IbanStructure(new Patterns.UA())
            {
                Example = "UA213223130000026007233566001"
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
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2016, 2, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Vatican City State
        yield return new IbanCountry("VA")
        {
            NativeName = "Città del Vaticano",
            EnglishName = "Vatican City State",
            Iban = new IbanStructure(new Patterns.VA())
            {
                Example = "VA59001123000012345678"
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
            LastUpdatedDate = new DateTimeOffset(2018, 12, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2019, 3, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Virgin Islands
        yield return new IbanCountry("VG")
        {
            NativeName = "British Virgin Islands",
            EnglishName = "Virgin Islands",
            Iban = new IbanStructure(new Patterns.VG())
            {
                Example = "VG96VPVG0000012345678901"
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
            LastUpdatedDate = new DateTimeOffset(2014, 6, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2012, 4, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Kosovo
        yield return new IbanCountry("XK")
        {
            NativeName = "Kosovë",
            EnglishName = "Kosovo",
            Iban = new IbanStructure(new Patterns.XK())
            {
                Example = "XK051212012345678906"
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
            LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2014, 9, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // Yemen
        yield return new IbanCountry("YE")
        {
            NativeName = "اليمن",
            EnglishName = "Yemen",
            Iban = new IbanStructure(new Patterns.YE())
            {
                Example = "YE15CBYE0001018861234567891234"
            },
            Bban = new BbanStructure(new SwiftPattern("4!a4!n18!c"), 4)
            {
                Example = "CBYE0001018861234567891234"
            },
            Bank = new BankStructure(new SwiftPattern("4!a"), 4)
            {
                Example = "CBYE"
            },
            Branch = new BranchStructure(new SwiftPattern("4!n"), 8)
            {
                Example = "0001"
            },
            Sepa = new SepaInfo
            {
                IsMember = false
            },
            DomesticAccountNumberExample = "018861234567891234",
            LastUpdatedDate = new DateTimeOffset(2024, 7, 1, 0, 0, 0, TimeSpan.Zero),
            EffectiveDate = new DateTimeOffset(2024, 7, 1, 0, 0, 0, TimeSpan.Zero)
        };

        // ReSharper restore StringLiteralTypo
        // ReSharper restore CommentTypo
    }

    [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
    private static class Patterns
    {

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class AD() : SwiftPattern("AD2!n4!n4!n12!c", 24, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'A'
                    && value[pos++] == 'D'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class AE() : SwiftPattern("AE2!n3!n16!n", 23, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'A'
                    && value[pos++] == 'E'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class AL() : SwiftPattern("AL2!n8!n16!c", 28, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'A'
                    && value[pos++] == 'L'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class AT() : SwiftPattern("AT2!n5!n11!n", 20, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'A'
                    && value[pos++] == 'T'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class AZ() : SwiftPattern("AZ2!n4!a20!c", 28, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'A'
                    && value[pos++] == 'Z'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class BA() : SwiftPattern("BA2!n3!n3!n8!n2!n", 20, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'B'
                    && value[pos++] == 'A'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class BE() : SwiftPattern("BE2!n3!n7!n2!n", 16, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'B'
                    && value[pos++] == 'E'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class BG() : SwiftPattern("BG2!n4!a4!n2!n8!c", 22, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'B'
                    && value[pos++] == 'G'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class BH() : SwiftPattern("BH2!n4!a14!c", 22, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'B'
                    && value[pos++] == 'H'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class BI() : SwiftPattern("BI2!n5!n5!n11!n2!n", 27, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'B'
                    && value[pos++] == 'I'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class BR() : SwiftPattern("BR2!n8!n5!n10!n1!a1!c", 29, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'B'
                    && value[pos++] == 'R'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class BY() : SwiftPattern("BY2!n4!c4!n16!c", 28, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'B'
                    && value[pos++] == 'Y'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class CH() : SwiftPattern("CH2!n5!n12!c", 21, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'C'
                    && value[pos++] == 'H'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class CR() : SwiftPattern("CR2!n4!n14!n", 22, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'C'
                    && value[pos++] == 'R'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class CY() : SwiftPattern("CY2!n3!n5!n16!c", 28, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'C'
                    && value[pos++] == 'Y'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class CZ() : SwiftPattern("CZ2!n4!n6!n10!n", 24, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'C'
                    && value[pos++] == 'Z'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class DE() : SwiftPattern("DE2!n8!n10!n", 22, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'D'
                    && value[pos++] == 'E'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class DJ() : SwiftPattern("DJ2!n5!n5!n11!n2!n", 27, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'D'
                    && value[pos++] == 'J'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class DK() : SwiftPattern("DK2!n4!n9!n1!n", 18, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'D'
                    && value[pos++] == 'K'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class DO() : SwiftPattern("DO2!n4!c20!n", 28, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'D'
                    && value[pos++] == 'O'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class EE() : SwiftPattern("EE2!n2!n14!n", 20, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'E'
                    && value[pos++] == 'E'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class EG() : SwiftPattern("EG2!n4!n4!n17!n", 29, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'E'
                    && value[pos++] == 'G'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class ES() : SwiftPattern("ES2!n4!n4!n1!n1!n10!n", 24, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'E'
                    && value[pos++] == 'S'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class FI() : SwiftPattern("FI2!n3!n11!n", 18, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'F'
                    && value[pos++] == 'I'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class FK() : SwiftPattern("FK2!n2!a12!n", 18, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'F'
                    && value[pos++] == 'K'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class FO() : SwiftPattern("FO2!n4!n9!n1!n", 18, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'F'
                    && value[pos++] == 'O'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class FR() : SwiftPattern("FR2!n5!n5!n11!c2!n", 27, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'F'
                    && value[pos++] == 'R'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class GB() : SwiftPattern("GB2!n4!a6!n8!n", 22, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'G'
                    && value[pos++] == 'B'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class GE() : SwiftPattern("GE2!n2!a16!n", 22, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'G'
                    && value[pos++] == 'E'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class GI() : SwiftPattern("GI2!n4!a15!c", 23, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'G'
                    && value[pos++] == 'I'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class GL() : SwiftPattern("GL2!n4!n9!n1!n", 18, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'G'
                    && value[pos++] == 'L'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class GR() : SwiftPattern("GR2!n3!n4!n16!c", 27, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'G'
                    && value[pos++] == 'R'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class GT() : SwiftPattern("GT2!n4!c20!c", 28, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'G'
                    && value[pos++] == 'T'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class HN() : SwiftPattern("HN2!n4!a20!n", 28, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'H'
                    && value[pos++] == 'N'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class HR() : SwiftPattern("HR2!n7!n10!n", 21, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'H'
                    && value[pos++] == 'R'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class HU() : SwiftPattern("HU2!n3!n4!n1!n15!n1!n", 28, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'H'
                    && value[pos++] == 'U'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class IE() : SwiftPattern("IE2!n4!a6!n8!n", 22, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'I'
                    && value[pos++] == 'E'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class IL() : SwiftPattern("IL2!n3!n3!n13!n", 23, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'I'
                    && value[pos++] == 'L'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class IQ() : SwiftPattern("IQ2!n4!a3!n12!n", 23, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'I'
                    && value[pos++] == 'Q'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class IS() : SwiftPattern("IS2!n4!n2!n6!n10!n", 26, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'I'
                    && value[pos++] == 'S'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class IT() : SwiftPattern("IT2!n1!a5!n5!n12!c", 27, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'I'
                    && value[pos++] == 'T'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class JO() : SwiftPattern("JO2!n4!a4!n18!c", 30, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'J'
                    && value[pos++] == 'O'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class KW() : SwiftPattern("KW2!n4!a22!c", 30, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'K'
                    && value[pos++] == 'W'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class KZ() : SwiftPattern("KZ2!n3!n13!c", 20, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'K'
                    && value[pos++] == 'Z'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class LB() : SwiftPattern("LB2!n4!n20!c", 28, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'L'
                    && value[pos++] == 'B'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class LC() : SwiftPattern("LC2!n4!a24!c", 32, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'L'
                    && value[pos++] == 'C'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class LI() : SwiftPattern("LI2!n5!n12!c", 21, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'L'
                    && value[pos++] == 'I'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class LT() : SwiftPattern("LT2!n5!n11!n", 20, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'L'
                    && value[pos++] == 'T'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class LU() : SwiftPattern("LU2!n3!n13!c", 20, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'L'
                    && value[pos++] == 'U'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class LV() : SwiftPattern("LV2!n4!a13!c", 21, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'L'
                    && value[pos++] == 'V'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class LY() : SwiftPattern("LY2!n3!n3!n15!n", 25, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'L'
                    && value[pos++] == 'Y'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class MC() : SwiftPattern("MC2!n5!n5!n11!c2!n", 27, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'M'
                    && value[pos++] == 'C'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class MD() : SwiftPattern("MD2!n2!c18!c", 24, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'M'
                    && value[pos++] == 'D'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class ME() : SwiftPattern("ME2!n3!n13!n2!n", 22, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'M'
                    && value[pos++] == 'E'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class MK() : SwiftPattern("MK2!n3!n10!c2!n", 19, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'M'
                    && value[pos++] == 'K'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class MN() : SwiftPattern("MN2!n4!n12!n", 20, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'M'
                    && value[pos++] == 'N'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class MR() : SwiftPattern("MR2!n5!n5!n11!n2!n", 27, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'M'
                    && value[pos++] == 'R'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class MT() : SwiftPattern("MT2!n4!a5!n18!c", 31, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'M'
                    && value[pos++] == 'T'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class MU() : SwiftPattern("MU2!n4!a2!n2!n12!n3!n3!a", 30, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'M'
                    && value[pos++] == 'U'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class NI() : SwiftPattern("NI2!n4!a20!n", 28, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'N'
                    && value[pos++] == 'I'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class NL() : SwiftPattern("NL2!n4!a10!n", 18, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'N'
                    && value[pos++] == 'L'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class NO() : SwiftPattern("NO2!n4!n6!n1!n", 15, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'N'
                    && value[pos++] == 'O'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class OM() : SwiftPattern("OM2!n3!n16!c", 23, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'O'
                    && value[pos++] == 'M'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class PK() : SwiftPattern("PK2!n4!a16!c", 24, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'P'
                    && value[pos++] == 'K'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class PL() : SwiftPattern("PL2!n8!n16!n", 28, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'P'
                    && value[pos++] == 'L'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class PS() : SwiftPattern("PS2!n4!a21!c", 29, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'P'
                    && value[pos++] == 'S'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class PT() : SwiftPattern("PT2!n4!n4!n11!n2!n", 25, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'P'
                    && value[pos++] == 'T'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class QA() : SwiftPattern("QA2!n4!a21!c", 29, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'Q'
                    && value[pos++] == 'A'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class RO() : SwiftPattern("RO2!n4!a16!c", 24, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'R'
                    && value[pos++] == 'O'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class RS() : SwiftPattern("RS2!n3!n13!n2!n", 22, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'R'
                    && value[pos++] == 'S'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class SA() : SwiftPattern("SA2!n2!n18!c", 24, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'S'
                    && value[pos++] == 'A'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class SC() : SwiftPattern("SC2!n4!a2!n2!n16!n3!a", 31, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'S'
                    && value[pos++] == 'C'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class SD() : SwiftPattern("SD2!n2!n12!n", 18, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'S'
                    && value[pos++] == 'D'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class SE() : SwiftPattern("SE2!n3!n16!n1!n", 24, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'S'
                    && value[pos++] == 'E'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class SI() : SwiftPattern("SI2!n5!n8!n2!n", 19, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'S'
                    && value[pos++] == 'I'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class SK() : SwiftPattern("SK2!n4!n6!n10!n", 24, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'S'
                    && value[pos++] == 'K'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class SM() : SwiftPattern("SM2!n1!a5!n5!n12!c", 27, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'S'
                    && value[pos++] == 'M'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class SO() : SwiftPattern("SO2!n4!n3!n12!n", 23, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'S'
                    && value[pos++] == 'O'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class ST() : SwiftPattern("ST2!n8!n11!n2!n", 25, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'S'
                    && value[pos++] == 'T'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class SV() : SwiftPattern("SV2!n4!a20!n", 28, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'S'
                    && value[pos++] == 'V'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class TL() : SwiftPattern("TL2!n3!n14!n2!n", 23, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'T'
                    && value[pos++] == 'L'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class TN() : SwiftPattern("TN2!n2!n3!n13!n2!n", 24, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'T'
                    && value[pos++] == 'N'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class TR() : SwiftPattern("TR2!n5!n1!n16!c", 26, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'T'
                    && value[pos++] == 'R'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class UA() : SwiftPattern("UA2!n6!n19!c", 29, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'U'
                    && value[pos++] == 'A'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class VA() : SwiftPattern("VA2!n3!n15!n", 22, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'V'
                    && value[pos++] == 'A'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class VG() : SwiftPattern("VG2!n4!a16!n", 24, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'V'
                    && value[pos++] == 'G'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class XK() : SwiftPattern("XK2!n4!n10!n2!n", 20, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'X'
                    && value[pos++] == 'K'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }

        [GeneratedCode("SwiftRegistryProviderT4", "1.1-r99")]
        internal sealed class YE() : SwiftPattern("YE2!n4!a4!n18!c", 30, true)
        {
#if USE_SPANS
            internal override bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
#else
            internal override bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
#endif
            {
                int pos = 0;
                if (value.Length == MaxLength
                    && value[pos++] == 'Y'
                    && value[pos++] == 'E'
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsUpperAsciiLetter()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAsciiDigit()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    && value[pos++].IsAlphaNumeric()
                    )
                {
                    errorPos = null;
                    return true;
                }

                errorPos = pos - 1;
                return false;
            }
        }
    }
}
