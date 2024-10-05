﻿using System.CodeDom.Compiler;
using System.Collections;
using System.Diagnostics;

namespace IbanNet.Registry.Wikipedia;

/// <summary>
/// This IBAN registry provider is derived from Wikipedia.
/// </summary>
/// <remarks>
/// <para>Note: this provider does not conform to the official spec, and is provided as an add-on. Use at your own risk.</para>
/// <para>
/// Generated from: https://en.wikipedia.org/wiki/International_Bank_Account_Number
/// Page ID: 15253
/// Rev ID: 1249179146
/// </para>
/// </remarks>
[GeneratedCode("WikiRegistryProviderT4", "1.15253-1249179146")]
public class WikipediaRegistryProvider : IIbanRegistryProvider
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
        yield return new IbanCountry("AD")
        {
            NativeName = "Andorra",
            EnglishName = "Andorra",
            Iban = new IbanStructure(new IbanWikipediaPattern("AD", "8n,12c")),
            Bban = new BbanStructure(new WikipediaPattern("8n,12c"), 4)
        };

        yield return new IbanCountry("AE")
        {
            NativeName = "الإمارات العربية المتحدة",
            EnglishName = "United Arab Emirates",
            Iban = new IbanStructure(new IbanWikipediaPattern("AE", "3n,16n")),
            Bban = new BbanStructure(new WikipediaPattern("3n,16n"), 4)
        };

        yield return new IbanCountry("AL")
        {
            NativeName = "Shqipëri",
            EnglishName = "Albania",
            Iban = new IbanStructure(new IbanWikipediaPattern("AL", "8n,16c")),
            Bban = new BbanStructure(new WikipediaPattern("8n,16c"), 4)
        };

        yield return new IbanCountry("AO")
        {
            NativeName = "Angóla",
            EnglishName = "Angola",
            Iban = new IbanStructure(new IbanWikipediaPattern("AO", "21n")),
            Bban = new BbanStructure(new WikipediaPattern("21n"), 4)
        };

        yield return new IbanCountry("AT")
        {
            NativeName = "Österreich",
            EnglishName = "Austria",
            Iban = new IbanStructure(new IbanWikipediaPattern("AT", "16n")),
            Bban = new BbanStructure(new WikipediaPattern("16n"), 4)
        };

        yield return new IbanCountry("AZ")
        {
            NativeName = "Азәрбајҹан",
            EnglishName = "Azerbaijan",
            Iban = new IbanStructure(new IbanWikipediaPattern("AZ", "4a,20c")),
            Bban = new BbanStructure(new WikipediaPattern("4a,20c"), 4)
        };

        yield return new IbanCountry("BA")
        {
            NativeName = "Bosna i Hercegovina",
            EnglishName = "Bosnia and Herzegovina",
            Iban = new IbanStructure(new IbanWikipediaPattern("BA", "16n")),
            Bban = new BbanStructure(new WikipediaPattern("16n"), 4)
        };

        yield return new IbanCountry("BE")
        {
            NativeName = "België",
            EnglishName = "Belgium",
            Iban = new IbanStructure(new IbanWikipediaPattern("BE", "12n")),
            Bban = new BbanStructure(new WikipediaPattern("12n"), 4)
        };

        yield return new IbanCountry("BF")
        {
            NativeName = "Burkibaa Faaso",
            EnglishName = "Burkina Faso",
            Iban = new IbanStructure(new IbanWikipediaPattern("BF", "2c,22n")),
            Bban = new BbanStructure(new WikipediaPattern("2c,22n"), 4)
        };

        yield return new IbanCountry("BG")
        {
            NativeName = "България",
            EnglishName = "Bulgaria",
            Iban = new IbanStructure(new IbanWikipediaPattern("BG", "4a,6n,8c")),
            Bban = new BbanStructure(new WikipediaPattern("4a,6n,8c"), 4)
        };

        yield return new IbanCountry("BH")
        {
            NativeName = "البحرين",
            EnglishName = "Bahrain",
            Iban = new IbanStructure(new IbanWikipediaPattern("BH", "4a,14c")),
            Bban = new BbanStructure(new WikipediaPattern("4a,14c"), 4)
        };

        yield return new IbanCountry("BI")
        {
            NativeName = "Burundi",
            EnglishName = "Burundi",
            Iban = new IbanStructure(new IbanWikipediaPattern("BI", "5n,5n,11n,2n")),
            Bban = new BbanStructure(new WikipediaPattern("5n,5n,11n,2n"), 4)
        };

        yield return new IbanCountry("BJ")
        {
            NativeName = "Bénin",
            EnglishName = "Benin",
            Iban = new IbanStructure(new IbanWikipediaPattern("BJ", "2c,22n")),
            Bban = new BbanStructure(new WikipediaPattern("2c,22n"), 4)
        };

        yield return new IbanCountry("BR")
        {
            NativeName = "Brasil",
            EnglishName = "Brazil",
            Iban = new IbanStructure(new IbanWikipediaPattern("BR", "23n,1a,1c")),
            Bban = new BbanStructure(new WikipediaPattern("23n,1a,1c"), 4)
        };

        yield return new IbanCountry("BY")
        {
            NativeName = "Беларусь",
            EnglishName = "Belarus",
            Iban = new IbanStructure(new IbanWikipediaPattern("BY", "4c,4n,16c")),
            Bban = new BbanStructure(new WikipediaPattern("4c,4n,16c"), 4)
        };

        yield return new IbanCountry("CF")
        {
            NativeName = "République centrafricaine",
            EnglishName = "Central African Republic",
            Iban = new IbanStructure(new IbanWikipediaPattern("CF", "23n")),
            Bban = new BbanStructure(new WikipediaPattern("23n"), 4)
        };

        yield return new IbanCountry("CG")
        {
            NativeName = "Congo",
            EnglishName = "Congo, Republic of the",
            Iban = new IbanStructure(new IbanWikipediaPattern("CG", "23n")),
            Bban = new BbanStructure(new WikipediaPattern("23n"), 4)
        };

        yield return new IbanCountry("CH")
        {
            NativeName = "Svizzera",
            EnglishName = "Switzerland",
            Iban = new IbanStructure(new IbanWikipediaPattern("CH", "5n,12c")),
            Bban = new BbanStructure(new WikipediaPattern("5n,12c"), 4)
        };

        yield return new IbanCountry("CI")
        {
            NativeName = "Côte d’Ivoire",
            EnglishName = "Côte d'Ivoire",
            Iban = new IbanStructure(new IbanWikipediaPattern("CI", "2a,22n")),
            Bban = new BbanStructure(new WikipediaPattern("2a,22n"), 4)
        };

        yield return new IbanCountry("CM")
        {
            NativeName = "Kàmàlûŋ",
            EnglishName = "Cameroon",
            Iban = new IbanStructure(new IbanWikipediaPattern("CM", "23n")),
            Bban = new BbanStructure(new WikipediaPattern("23n"), 4)
        };

        yield return new IbanCountry("CR")
        {
            NativeName = "Costa Rica",
            EnglishName = "Costa Rica",
            Iban = new IbanStructure(new IbanWikipediaPattern("CR", "18n")),
            Bban = new BbanStructure(new WikipediaPattern("18n"), 4)
        };

        yield return new IbanCountry("CV")
        {
            NativeName = "Kabu Verdi",
            EnglishName = "Cabo Verde",
            Iban = new IbanStructure(new IbanWikipediaPattern("CV", "21n")),
            Bban = new BbanStructure(new WikipediaPattern("21n"), 4)
        };

        yield return new IbanCountry("CY")
        {
            NativeName = "Κύπρος",
            EnglishName = "Cyprus",
            Iban = new IbanStructure(new IbanWikipediaPattern("CY", "8n,16c")),
            Bban = new BbanStructure(new WikipediaPattern("8n,16c"), 4)
        };

        yield return new IbanCountry("CZ")
        {
            NativeName = "Česko",
            EnglishName = "Czech Republic",
            Iban = new IbanStructure(new IbanWikipediaPattern("CZ", "20n")),
            Bban = new BbanStructure(new WikipediaPattern("20n"), 4)
        };

        yield return new IbanCountry("DE")
        {
            NativeName = "Deutschland",
            EnglishName = "Germany",
            Iban = new IbanStructure(new IbanWikipediaPattern("DE", "18n")),
            Bban = new BbanStructure(new WikipediaPattern("18n"), 4)
        };

        yield return new IbanCountry("DJ")
        {
            NativeName = "Yabuuti",
            EnglishName = "Djibouti",
            Iban = new IbanStructure(new IbanWikipediaPattern("DJ", "5n,5n,11n,2n")),
            Bban = new BbanStructure(new WikipediaPattern("5n,5n,11n,2n"), 4)
        };

        yield return new IbanCountry("DK")
        {
            NativeName = "Danmark",
            EnglishName = "Denmark",
            Iban = new IbanStructure(new IbanWikipediaPattern("DK", "14n")),
            Bban = new BbanStructure(new WikipediaPattern("14n"), 4)
        };

        yield return new IbanCountry("DO")
        {
            NativeName = "República Dominicana",
            EnglishName = "Dominican Republic",
            Iban = new IbanStructure(new IbanWikipediaPattern("DO", "4c,20n")),
            Bban = new BbanStructure(new WikipediaPattern("4c,20n"), 4)
        };

        yield return new IbanCountry("DZ")
        {
            NativeName = "الجزائر",
            EnglishName = "Algeria",
            Iban = new IbanStructure(new IbanWikipediaPattern("DZ", "22n")),
            Bban = new BbanStructure(new WikipediaPattern("22n"), 4)
        };

        yield return new IbanCountry("EE")
        {
            NativeName = "Eesti",
            EnglishName = "Estonia",
            Iban = new IbanStructure(new IbanWikipediaPattern("EE", "16n")),
            Bban = new BbanStructure(new WikipediaPattern("16n"), 4)
        };

        yield return new IbanCountry("EG")
        {
            NativeName = "مصر",
            EnglishName = "Egypt",
            Iban = new IbanStructure(new IbanWikipediaPattern("EG", "25n")),
            Bban = new BbanStructure(new WikipediaPattern("25n"), 4)
        };

        yield return new IbanCountry("ES")
        {
            NativeName = "España",
            EnglishName = "Spain",
            Iban = new IbanStructure(new IbanWikipediaPattern("ES", "20n")),
            Bban = new BbanStructure(new WikipediaPattern("20n"), 4)
        };

        yield return new IbanCountry("FI")
        {
            NativeName = "Suomi",
            EnglishName = "Finland",
            Iban = new IbanStructure(new IbanWikipediaPattern("FI", "14n")),
            Bban = new BbanStructure(new WikipediaPattern("14n"), 4)
        };

        yield return new IbanCountry("FK")
        {
            NativeName = "Falkland Islands",
            EnglishName = "Falkland Islands",
            Iban = new IbanStructure(new IbanWikipediaPattern("FK", "2a,12n")),
            Bban = new BbanStructure(new WikipediaPattern("2a,12n"), 4)
        };

        yield return new IbanCountry("FO")
        {
            NativeName = "Føroyar",
            EnglishName = "Faroe Islands",
            Iban = new IbanStructure(new IbanWikipediaPattern("FO", "14n")),
            Bban = new BbanStructure(new WikipediaPattern("14n"), 4)
        };

        yield return new IbanCountry("FR")
        {
            NativeName = "France",
            EnglishName = "France",
            Iban = new IbanStructure(new IbanWikipediaPattern("FR", "10n,11c,2n")),
            Bban = new BbanStructure(new WikipediaPattern("10n,11c,2n"), 4)
        };

        yield return new IbanCountry("GA")
        {
            NativeName = "Gabon",
            EnglishName = "Gabon",
            Iban = new IbanStructure(new IbanWikipediaPattern("GA", "23n")),
            Bban = new BbanStructure(new WikipediaPattern("23n"), 4)
        };

        yield return new IbanCountry("GB")
        {
            NativeName = "United Kingdom",
            EnglishName = "United Kingdom",
            Iban = new IbanStructure(new IbanWikipediaPattern("GB", "4a,14n")),
            Bban = new BbanStructure(new WikipediaPattern("4a,14n"), 4)
        };

        yield return new IbanCountry("GE")
        {
            NativeName = "საქართველო",
            EnglishName = "Georgia",
            Iban = new IbanStructure(new IbanWikipediaPattern("GE", "2a,16n")),
            Bban = new BbanStructure(new WikipediaPattern("2a,16n"), 4)
        };

        yield return new IbanCountry("GI")
        {
            NativeName = "Gibraltar",
            EnglishName = "Gibraltar",
            Iban = new IbanStructure(new IbanWikipediaPattern("GI", "4a,15c")),
            Bban = new BbanStructure(new WikipediaPattern("4a,15c"), 4)
        };

        yield return new IbanCountry("GL")
        {
            NativeName = "Kalaallit Nunaat",
            EnglishName = "Greenland",
            Iban = new IbanStructure(new IbanWikipediaPattern("GL", "14n")),
            Bban = new BbanStructure(new WikipediaPattern("14n"), 4)
        };

        yield return new IbanCountry("GQ")
        {
            NativeName = "Guinea Ecuatorial",
            EnglishName = "Equatorial Guinea",
            Iban = new IbanStructure(new IbanWikipediaPattern("GQ", "23n")),
            Bban = new BbanStructure(new WikipediaPattern("23n"), 4)
        };

        yield return new IbanCountry("GR")
        {
            NativeName = "Ελλάδα",
            EnglishName = "Greece",
            Iban = new IbanStructure(new IbanWikipediaPattern("GR", "7n,16c")),
            Bban = new BbanStructure(new WikipediaPattern("7n,16c"), 4)
        };

        yield return new IbanCountry("GT")
        {
            NativeName = "Guatemala",
            EnglishName = "Guatemala",
            Iban = new IbanStructure(new IbanWikipediaPattern("GT", "4c,20c")),
            Bban = new BbanStructure(new WikipediaPattern("4c,20c"), 4)
        };

        yield return new IbanCountry("GW")
        {
            NativeName = "Gine-Bisaawo",
            EnglishName = "Guinea-Bissau",
            Iban = new IbanStructure(new IbanWikipediaPattern("GW", "2c,19n")),
            Bban = new BbanStructure(new WikipediaPattern("2c,19n"), 4)
        };

        yield return new IbanCountry("HN")
        {
            NativeName = "Honduras",
            EnglishName = "Honduras",
            Iban = new IbanStructure(new IbanWikipediaPattern("HN", "4a,20n")),
            Bban = new BbanStructure(new WikipediaPattern("4a,20n"), 4)
        };

        yield return new IbanCountry("HR")
        {
            NativeName = "Hrvatska",
            EnglishName = "Croatia",
            Iban = new IbanStructure(new IbanWikipediaPattern("HR", "17n")),
            Bban = new BbanStructure(new WikipediaPattern("17n"), 4)
        };

        yield return new IbanCountry("HU")
        {
            NativeName = "Magyarország",
            EnglishName = "Hungary",
            Iban = new IbanStructure(new IbanWikipediaPattern("HU", "24n")),
            Bban = new BbanStructure(new WikipediaPattern("24n"), 4)
        };

        yield return new IbanCountry("IE")
        {
            NativeName = "Ireland",
            EnglishName = "Ireland",
            Iban = new IbanStructure(new IbanWikipediaPattern("IE", "4a,6n,8n")),
            Bban = new BbanStructure(new WikipediaPattern("4a,6n,8n"), 4)
        };

        yield return new IbanCountry("IL")
        {
            NativeName = "ישראל",
            EnglishName = "Israel",
            Iban = new IbanStructure(new IbanWikipediaPattern("IL", "19n")),
            Bban = new BbanStructure(new WikipediaPattern("19n"), 4)
        };

        yield return new IbanCountry("IQ")
        {
            NativeName = "العراق",
            EnglishName = "Iraq",
            Iban = new IbanStructure(new IbanWikipediaPattern("IQ", "4a,15n")),
            Bban = new BbanStructure(new WikipediaPattern("4a,15n"), 4)
        };

        yield return new IbanCountry("IR")
        {
            NativeName = "ایران",
            EnglishName = "Iran",
            Iban = new IbanStructure(new IbanWikipediaPattern("IR", "22n")),
            Bban = new BbanStructure(new WikipediaPattern("22n"), 4)
        };

        yield return new IbanCountry("IS")
        {
            NativeName = "Ísland",
            EnglishName = "Iceland",
            Iban = new IbanStructure(new IbanWikipediaPattern("IS", "22n")),
            Bban = new BbanStructure(new WikipediaPattern("22n"), 4)
        };

        yield return new IbanCountry("IT")
        {
            NativeName = "Italia",
            EnglishName = "Italy",
            Iban = new IbanStructure(new IbanWikipediaPattern("IT", "1a,10n,12c")),
            Bban = new BbanStructure(new WikipediaPattern("1a,10n,12c"), 4)
        };

        yield return new IbanCountry("JO")
        {
            NativeName = "الأردن",
            EnglishName = "Jordan",
            Iban = new IbanStructure(new IbanWikipediaPattern("JO", "4a,4n,18c")),
            Bban = new BbanStructure(new WikipediaPattern("4a,4n,18c"), 4)
        };

        yield return new IbanCountry("KM")
        {
            NativeName = "جزر القمر",
            EnglishName = "Comoros",
            Iban = new IbanStructure(new IbanWikipediaPattern("KM", "23n")),
            Bban = new BbanStructure(new WikipediaPattern("23n"), 4)
        };

        yield return new IbanCountry("KW")
        {
            NativeName = "الكويت",
            EnglishName = "Kuwait",
            Iban = new IbanStructure(new IbanWikipediaPattern("KW", "4a,22c")),
            Bban = new BbanStructure(new WikipediaPattern("4a,22c"), 4)
        };

        yield return new IbanCountry("KZ")
        {
            NativeName = "Қазақстан",
            EnglishName = "Kazakhstan",
            Iban = new IbanStructure(new IbanWikipediaPattern("KZ", "3n,13c")),
            Bban = new BbanStructure(new WikipediaPattern("3n,13c"), 4)
        };

        yield return new IbanCountry("LB")
        {
            NativeName = "لبنان",
            EnglishName = "Lebanon",
            Iban = new IbanStructure(new IbanWikipediaPattern("LB", "4n,20c")),
            Bban = new BbanStructure(new WikipediaPattern("4n,20c"), 4)
        };

        yield return new IbanCountry("LC")
        {
            NativeName = "St. Lucia",
            EnglishName = "Saint Lucia",
            Iban = new IbanStructure(new IbanWikipediaPattern("LC", "4a,24c")),
            Bban = new BbanStructure(new WikipediaPattern("4a,24c"), 4)
        };

        yield return new IbanCountry("LI")
        {
            NativeName = "Liechtenstein",
            EnglishName = "Liechtenstein",
            Iban = new IbanStructure(new IbanWikipediaPattern("LI", "5n,12c")),
            Bban = new BbanStructure(new WikipediaPattern("5n,12c"), 4)
        };

        yield return new IbanCountry("LT")
        {
            NativeName = "Lietuva",
            EnglishName = "Lithuania",
            Iban = new IbanStructure(new IbanWikipediaPattern("LT", "16n")),
            Bban = new BbanStructure(new WikipediaPattern("16n"), 4)
        };

        yield return new IbanCountry("LU")
        {
            NativeName = "Lëtzebuerg",
            EnglishName = "Luxembourg",
            Iban = new IbanStructure(new IbanWikipediaPattern("LU", "3n,13c")),
            Bban = new BbanStructure(new WikipediaPattern("3n,13c"), 4)
        };

        yield return new IbanCountry("LV")
        {
            NativeName = "Latvija",
            EnglishName = "Latvia",
            Iban = new IbanStructure(new IbanWikipediaPattern("LV", "4a,13c")),
            Bban = new BbanStructure(new WikipediaPattern("4a,13c"), 4)
        };

        yield return new IbanCountry("LY")
        {
            NativeName = "ليبيا",
            EnglishName = "Libya",
            Iban = new IbanStructure(new IbanWikipediaPattern("LY", "21n")),
            Bban = new BbanStructure(new WikipediaPattern("21n"), 4)
        };

        yield return new IbanCountry("MA")
        {
            NativeName = "المملكة المغربية",
            EnglishName = "Morocco",
            Iban = new IbanStructure(new IbanWikipediaPattern("MA", "24n")),
            Bban = new BbanStructure(new WikipediaPattern("24n"), 4)
        };

        yield return new IbanCountry("MC")
        {
            NativeName = "Monaco",
            EnglishName = "Monaco",
            Iban = new IbanStructure(new IbanWikipediaPattern("MC", "10n,11c,2n")),
            Bban = new BbanStructure(new WikipediaPattern("10n,11c,2n"), 4)
        };

        yield return new IbanCountry("MD")
        {
            NativeName = "Republica Moldova",
            EnglishName = "Moldova",
            Iban = new IbanStructure(new IbanWikipediaPattern("MD", "2c,18c")),
            Bban = new BbanStructure(new WikipediaPattern("2c,18c"), 4)
        };

        yield return new IbanCountry("ME")
        {
            NativeName = "Crna Gora",
            EnglishName = "Montenegro",
            Iban = new IbanStructure(new IbanWikipediaPattern("ME", "18n")),
            Bban = new BbanStructure(new WikipediaPattern("18n"), 4)
        };

        yield return new IbanCountry("MG")
        {
            NativeName = "Madagascar",
            EnglishName = "Madagascar",
            Iban = new IbanStructure(new IbanWikipediaPattern("MG", "23n")),
            Bban = new BbanStructure(new WikipediaPattern("23n"), 4)
        };

        yield return new IbanCountry("MK")
        {
            NativeName = "Северна Македонија",
            EnglishName = "North Macedonia",
            Iban = new IbanStructure(new IbanWikipediaPattern("MK", "3n,10c,2n")),
            Bban = new BbanStructure(new WikipediaPattern("3n,10c,2n"), 4)
        };

        yield return new IbanCountry("ML")
        {
            NativeName = "Mali",
            EnglishName = "Mali",
            Iban = new IbanStructure(new IbanWikipediaPattern("ML", "2c,22n")),
            Bban = new BbanStructure(new WikipediaPattern("2c,22n"), 4)
        };

        yield return new IbanCountry("MN")
        {
            NativeName = "Монгол",
            EnglishName = "Mongolia",
            Iban = new IbanStructure(new IbanWikipediaPattern("MN", "4n,12n")),
            Bban = new BbanStructure(new WikipediaPattern("4n,12n"), 4)
        };

        yield return new IbanCountry("MR")
        {
            NativeName = "موريتانيا",
            EnglishName = "Mauritania",
            Iban = new IbanStructure(new IbanWikipediaPattern("MR", "23n")),
            Bban = new BbanStructure(new WikipediaPattern("23n"), 4)
        };

        yield return new IbanCountry("MT")
        {
            NativeName = "Malta",
            EnglishName = "Malta",
            Iban = new IbanStructure(new IbanWikipediaPattern("MT", "4a,5n,18c")),
            Bban = new BbanStructure(new WikipediaPattern("4a,5n,18c"), 4)
        };

        yield return new IbanCountry("MU")
        {
            NativeName = "Mauritius",
            EnglishName = "Mauritius",
            Iban = new IbanStructure(new IbanWikipediaPattern("MU", "4a,19n,3a")),
            Bban = new BbanStructure(new WikipediaPattern("4a,19n,3a"), 4)
        };

        yield return new IbanCountry("MZ")
        {
            NativeName = "Umozambiki",
            EnglishName = "Mozambique",
            Iban = new IbanStructure(new IbanWikipediaPattern("MZ", "21n")),
            Bban = new BbanStructure(new WikipediaPattern("21n"), 4)
        };

        yield return new IbanCountry("NE")
        {
            NativeName = "Nižer",
            EnglishName = "Niger",
            Iban = new IbanStructure(new IbanWikipediaPattern("NE", "2a,22n")),
            Bban = new BbanStructure(new WikipediaPattern("2a,22n"), 4)
        };

        yield return new IbanCountry("NI")
        {
            NativeName = "Nicaragua",
            EnglishName = "Nicaragua",
            Iban = new IbanStructure(new IbanWikipediaPattern("NI", "4a,20n")),
            Bban = new BbanStructure(new WikipediaPattern("4a,20n"), 4)
        };

        yield return new IbanCountry("NL")
        {
            NativeName = "Nederland",
            EnglishName = "Netherlands",
            Iban = new IbanStructure(new IbanWikipediaPattern("NL", "4a,10n")),
            Bban = new BbanStructure(new WikipediaPattern("4a,10n"), 4)
        };

        yield return new IbanCountry("NO")
        {
            NativeName = "Noreg",
            EnglishName = "Norway",
            Iban = new IbanStructure(new IbanWikipediaPattern("NO", "11n")),
            Bban = new BbanStructure(new WikipediaPattern("11n"), 4)
        };

        yield return new IbanCountry("OM")
        {
            NativeName = "عمان",
            EnglishName = "Oman",
            Iban = new IbanStructure(new IbanWikipediaPattern("OM", "3n,16c")),
            Bban = new BbanStructure(new WikipediaPattern("3n,16c"), 4)
        };

        yield return new IbanCountry("PK")
        {
            NativeName = "پاکستان",
            EnglishName = "Pakistan",
            Iban = new IbanStructure(new IbanWikipediaPattern("PK", "4a,16c")),
            Bban = new BbanStructure(new WikipediaPattern("4a,16c"), 4)
        };

        yield return new IbanCountry("PL")
        {
            NativeName = "Polska",
            EnglishName = "Poland",
            Iban = new IbanStructure(new IbanWikipediaPattern("PL", "24n")),
            Bban = new BbanStructure(new WikipediaPattern("24n"), 4)
        };

        yield return new IbanCountry("PS")
        {
            NativeName = "السلطة الفلسطينية",
            EnglishName = "Palestinian territories",
            Iban = new IbanStructure(new IbanWikipediaPattern("PS", "4a,21c")),
            Bban = new BbanStructure(new WikipediaPattern("4a,21c"), 4)
        };

        yield return new IbanCountry("PT")
        {
            NativeName = "Portugal",
            EnglishName = "Portugal",
            Iban = new IbanStructure(new IbanWikipediaPattern("PT", "21n")),
            Bban = new BbanStructure(new WikipediaPattern("21n"), 4)
        };

        yield return new IbanCountry("QA")
        {
            NativeName = "قطر",
            EnglishName = "Qatar",
            Iban = new IbanStructure(new IbanWikipediaPattern("QA", "4a,21c")),
            Bban = new BbanStructure(new WikipediaPattern("4a,21c"), 4)
        };

        yield return new IbanCountry("RO")
        {
            NativeName = "România",
            EnglishName = "Romania",
            Iban = new IbanStructure(new IbanWikipediaPattern("RO", "4a,16c")),
            Bban = new BbanStructure(new WikipediaPattern("4a,16c"), 4)
        };

        yield return new IbanCountry("RS")
        {
            NativeName = "Srbija",
            EnglishName = "Serbia",
            Iban = new IbanStructure(new IbanWikipediaPattern("RS", "18n")),
            Bban = new BbanStructure(new WikipediaPattern("18n"), 4)
        };

        yield return new IbanCountry("SA")
        {
            NativeName = "المملكة العربية السعودية",
            EnglishName = "Saudi Arabia",
            Iban = new IbanStructure(new IbanWikipediaPattern("SA", "2n,18c")),
            Bban = new BbanStructure(new WikipediaPattern("2n,18c"), 4)
        };

        yield return new IbanCountry("SC")
        {
            NativeName = "Seychelles",
            EnglishName = "Seychelles",
            Iban = new IbanStructure(new IbanWikipediaPattern("SC", "4a,20n,3a")),
            Bban = new BbanStructure(new WikipediaPattern("4a,20n,3a"), 4)
        };

        yield return new IbanCountry("SD")
        {
            NativeName = "السودان",
            EnglishName = "Sudan",
            Iban = new IbanStructure(new IbanWikipediaPattern("SD", "14n")),
            Bban = new BbanStructure(new WikipediaPattern("14n"), 4)
        };

        yield return new IbanCountry("SE")
        {
            NativeName = "Sverige",
            EnglishName = "Sweden",
            Iban = new IbanStructure(new IbanWikipediaPattern("SE", "20n")),
            Bban = new BbanStructure(new WikipediaPattern("20n"), 4)
        };

        yield return new IbanCountry("SI")
        {
            NativeName = "Slovenija",
            EnglishName = "Slovenia",
            Iban = new IbanStructure(new IbanWikipediaPattern("SI", "15n")),
            Bban = new BbanStructure(new WikipediaPattern("15n"), 4)
        };

        yield return new IbanCountry("SK")
        {
            NativeName = "Slovensko",
            EnglishName = "Slovakia",
            Iban = new IbanStructure(new IbanWikipediaPattern("SK", "20n")),
            Bban = new BbanStructure(new WikipediaPattern("20n"), 4)
        };

        yield return new IbanCountry("SM")
        {
            NativeName = "San Marino",
            EnglishName = "San Marino",
            Iban = new IbanStructure(new IbanWikipediaPattern("SM", "1a,10n,12c")),
            Bban = new BbanStructure(new WikipediaPattern("1a,10n,12c"), 4)
        };

        yield return new IbanCountry("SN")
        {
            NativeName = "Senegaal",
            EnglishName = "Senegal",
            Iban = new IbanStructure(new IbanWikipediaPattern("SN", "2a,22n")),
            Bban = new BbanStructure(new WikipediaPattern("2a,22n"), 4)
        };

        yield return new IbanCountry("SO")
        {
            NativeName = "الصومال",
            EnglishName = "Somalia",
            Iban = new IbanStructure(new IbanWikipediaPattern("SO", "4n,3n,12n")),
            Bban = new BbanStructure(new WikipediaPattern("4n,3n,12n"), 4)
        };

        yield return new IbanCountry("ST")
        {
            NativeName = "São Tomé e Príncipe",
            EnglishName = "São Tomé and Príncipe",
            Iban = new IbanStructure(new IbanWikipediaPattern("ST", "21n")),
            Bban = new BbanStructure(new WikipediaPattern("21n"), 4)
        };

        yield return new IbanCountry("SV")
        {
            NativeName = "El Salvador",
            EnglishName = "El Salvador",
            Iban = new IbanStructure(new IbanWikipediaPattern("SV", "4a,20n")),
            Bban = new BbanStructure(new WikipediaPattern("4a,20n"), 4)
        };

        yield return new IbanCountry("TD")
        {
            NativeName = "تشاد",
            EnglishName = "Chad",
            Iban = new IbanStructure(new IbanWikipediaPattern("TD", "23n")),
            Bban = new BbanStructure(new WikipediaPattern("23n"), 4)
        };

        yield return new IbanCountry("TG")
        {
            NativeName = "Togo nutome",
            EnglishName = "Togo",
            Iban = new IbanStructure(new IbanWikipediaPattern("TG", "2a,22n")),
            Bban = new BbanStructure(new WikipediaPattern("2a,22n"), 4)
        };

        yield return new IbanCountry("TL")
        {
            NativeName = "Timor-Leste",
            EnglishName = "East Timor",
            Iban = new IbanStructure(new IbanWikipediaPattern("TL", "19n")),
            Bban = new BbanStructure(new WikipediaPattern("19n"), 4)
        };

        yield return new IbanCountry("TN")
        {
            NativeName = "تونس",
            EnglishName = "Tunisia",
            Iban = new IbanStructure(new IbanWikipediaPattern("TN", "20n")),
            Bban = new BbanStructure(new WikipediaPattern("20n"), 4)
        };

        yield return new IbanCountry("TR")
        {
            NativeName = "Türkiye",
            EnglishName = "Turkey",
            Iban = new IbanStructure(new IbanWikipediaPattern("TR", "5n,1n,16c")),
            Bban = new BbanStructure(new WikipediaPattern("5n,1n,16c"), 4)
        };

        yield return new IbanCountry("UA")
        {
            NativeName = "Україна",
            EnglishName = "Ukraine",
            Iban = new IbanStructure(new IbanWikipediaPattern("UA", "6n,19c")),
            Bban = new BbanStructure(new WikipediaPattern("6n,19c"), 4)
        };

        yield return new IbanCountry("VA")
        {
            NativeName = "Città del Vaticano",
            EnglishName = "Vatican City",
            Iban = new IbanStructure(new IbanWikipediaPattern("VA", "3n,15n")),
            Bban = new BbanStructure(new WikipediaPattern("3n,15n"), 4)
        };

        yield return new IbanCountry("VG")
        {
            NativeName = "British Virgin Islands",
            EnglishName = "Virgin Islands, British",
            Iban = new IbanStructure(new IbanWikipediaPattern("VG", "4a,16n")),
            Bban = new BbanStructure(new WikipediaPattern("4a,16n"), 4)
        };

        yield return new IbanCountry("XK")
        {
            NativeName = "Kosovë",
            EnglishName = "Kosovo",
            Iban = new IbanStructure(new IbanWikipediaPattern("XK", "4n,10n,2n")),
            Bban = new BbanStructure(new WikipediaPattern("4n,10n,2n"), 4)
        };

        yield return new IbanCountry("YE")
        {
            NativeName = "اليمن",
            EnglishName = "Yemen",
            Iban = new IbanStructure(new IbanWikipediaPattern("YE", "4a,4n,18c")),
            Bban = new BbanStructure(new WikipediaPattern("4a,4n,18c"), 4)
        };

        // ReSharper restore StringLiteralTypo
        // ReSharper restore CommentTypo
    }
}
