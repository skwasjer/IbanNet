using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace IbanNet.Registry.Wikipedia
{
    /// <summary>
    /// This IBAN registry provider is derived from Wikipedia.
    /// </summary>
    /// <remarks>
    /// <para>Note: this provider does not conform to the official spec, and is provided as an add-on. Use at your own risk.</para>
    /// <para>
    /// Generated from: https://en.wikipedia.org/wiki/International_Bank_Account_Number
    /// Page ID: 15253
    /// Rev ID: 1036918399
    /// </para>
    /// </remarks>
    [GeneratedCode("WikiRegistryProviderT4", "1.15253-1036918399")]
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
            yield return new IbanCountry("AL")
            {
                DisplayName = "Albania",
                EnglishName = "Albania",
                Iban = new IbanStructure(new IbanWikipediaPattern("8n,16c")),
                Bban = new BbanStructure(new WikipediaPattern("8n,16c"), 4)
            };

            yield return new IbanCountry("AD")
            {
                DisplayName = "Andorra",
                EnglishName = "Andorra",
                Iban = new IbanStructure(new IbanWikipediaPattern("8n,12c")),
                Bban = new BbanStructure(new WikipediaPattern("8n,12c"), 4)
            };

            yield return new IbanCountry("AT")
            {
                DisplayName = "Austria",
                EnglishName = "Austria",
                Iban = new IbanStructure(new IbanWikipediaPattern("16n")),
                Bban = new BbanStructure(new WikipediaPattern("16n"), 4)
            };

            yield return new IbanCountry("AZ")
            {
                DisplayName = "Azerbaijan",
                EnglishName = "Azerbaijan",
                Iban = new IbanStructure(new IbanWikipediaPattern("4c,20n")),
                Bban = new BbanStructure(new WikipediaPattern("4c,20n"), 4)
            };

            yield return new IbanCountry("BH")
            {
                DisplayName = "Bahrain",
                EnglishName = "Bahrain",
                Iban = new IbanStructure(new IbanWikipediaPattern("4a,14c")),
                Bban = new BbanStructure(new WikipediaPattern("4a,14c"), 4)
            };

            yield return new IbanCountry("BY")
            {
                DisplayName = "Belarus",
                EnglishName = "Belarus",
                Iban = new IbanStructure(new IbanWikipediaPattern("4c,4n,16c")),
                Bban = new BbanStructure(new WikipediaPattern("4c,4n,16c"), 4)
            };

            yield return new IbanCountry("BE")
            {
                DisplayName = "Belgium",
                EnglishName = "Belgium",
                Iban = new IbanStructure(new IbanWikipediaPattern("12n")),
                Bban = new BbanStructure(new WikipediaPattern("12n"), 4)
            };

            yield return new IbanCountry("BA")
            {
                DisplayName = "Bosnia and Herzegovina",
                EnglishName = "Bosnia and Herzegovina",
                Iban = new IbanStructure(new IbanWikipediaPattern("16n")),
                Bban = new BbanStructure(new WikipediaPattern("16n"), 4)
            };

            yield return new IbanCountry("BR")
            {
                DisplayName = "Brazil",
                EnglishName = "Brazil",
                Iban = new IbanStructure(new IbanWikipediaPattern("23n,1a,1c")),
                Bban = new BbanStructure(new WikipediaPattern("23n,1a,1c"), 4)
            };

            yield return new IbanCountry("BG")
            {
                DisplayName = "Bulgaria",
                EnglishName = "Bulgaria",
                Iban = new IbanStructure(new IbanWikipediaPattern("4a,6n,8c")),
                Bban = new BbanStructure(new WikipediaPattern("4a,6n,8c"), 4)
            };

            yield return new IbanCountry("CR")
            {
                DisplayName = "Costa Rica",
                EnglishName = "Costa Rica",
                Iban = new IbanStructure(new IbanWikipediaPattern("18n")),
                Bban = new BbanStructure(new WikipediaPattern("18n"), 4)
            };

            yield return new IbanCountry("HR")
            {
                DisplayName = "Croatia",
                EnglishName = "Croatia",
                Iban = new IbanStructure(new IbanWikipediaPattern("17n")),
                Bban = new BbanStructure(new WikipediaPattern("17n"), 4)
            };

            yield return new IbanCountry("CY")
            {
                DisplayName = "Cyprus",
                EnglishName = "Cyprus",
                Iban = new IbanStructure(new IbanWikipediaPattern("8n,16c")),
                Bban = new BbanStructure(new WikipediaPattern("8n,16c"), 4)
            };

            yield return new IbanCountry("CZ")
            {
                DisplayName = "Czech Republic",
                EnglishName = "Czech Republic",
                Iban = new IbanStructure(new IbanWikipediaPattern("20n")),
                Bban = new BbanStructure(new WikipediaPattern("20n"), 4)
            };

            yield return new IbanCountry("DK")
            {
                DisplayName = "Denmark",
                EnglishName = "Denmark",
                Iban = new IbanStructure(new IbanWikipediaPattern("14n")),
                Bban = new BbanStructure(new WikipediaPattern("14n"), 4)
            };

            yield return new IbanCountry("DO")
            {
                DisplayName = "Dominican Republic",
                EnglishName = "Dominican Republic",
                Iban = new IbanStructure(new IbanWikipediaPattern("4a,20n")),
                Bban = new BbanStructure(new WikipediaPattern("4a,20n"), 4)
            };

            yield return new IbanCountry("TL")
            {
                DisplayName = "East Timor",
                EnglishName = "East Timor",
                Iban = new IbanStructure(new IbanWikipediaPattern("19n")),
                Bban = new BbanStructure(new WikipediaPattern("19n"), 4)
            };

            yield return new IbanCountry("EG")
            {
                DisplayName = "Egypt",
                EnglishName = "Egypt",
                Iban = new IbanStructure(new IbanWikipediaPattern("25n")),
                Bban = new BbanStructure(new WikipediaPattern("25n"), 4)
            };

            yield return new IbanCountry("SV")
            {
                DisplayName = "El Salvador",
                EnglishName = "El Salvador",
                Iban = new IbanStructure(new IbanWikipediaPattern("4a,20n")),
                Bban = new BbanStructure(new WikipediaPattern("4a,20n"), 4)
            };

            yield return new IbanCountry("EE")
            {
                DisplayName = "Estonia",
                EnglishName = "Estonia",
                Iban = new IbanStructure(new IbanWikipediaPattern("16n")),
                Bban = new BbanStructure(new WikipediaPattern("16n"), 4)
            };

            yield return new IbanCountry("FO")
            {
                DisplayName = "Faroe Islands",
                EnglishName = "Faroe Islands",
                Iban = new IbanStructure(new IbanWikipediaPattern("14n")),
                Bban = new BbanStructure(new WikipediaPattern("14n"), 4)
            };

            yield return new IbanCountry("FI")
            {
                DisplayName = "Finland",
                EnglishName = "Finland",
                Iban = new IbanStructure(new IbanWikipediaPattern("14n")),
                Bban = new BbanStructure(new WikipediaPattern("14n"), 4)
            };

            yield return new IbanCountry("FR")
            {
                DisplayName = "France",
                EnglishName = "France",
                Iban = new IbanStructure(new IbanWikipediaPattern("10n,11c,2n")),
                Bban = new BbanStructure(new WikipediaPattern("10n,11c,2n"), 4)
            };

            yield return new IbanCountry("GE")
            {
                DisplayName = "Georgia",
                EnglishName = "Georgia",
                Iban = new IbanStructure(new IbanWikipediaPattern("2c,16n")),
                Bban = new BbanStructure(new WikipediaPattern("2c,16n"), 4)
            };

            yield return new IbanCountry("DE")
            {
                DisplayName = "Germany",
                EnglishName = "Germany",
                Iban = new IbanStructure(new IbanWikipediaPattern("18n")),
                Bban = new BbanStructure(new WikipediaPattern("18n"), 4)
            };

            yield return new IbanCountry("GI")
            {
                DisplayName = "Gibraltar",
                EnglishName = "Gibraltar",
                Iban = new IbanStructure(new IbanWikipediaPattern("4a,15c")),
                Bban = new BbanStructure(new WikipediaPattern("4a,15c"), 4)
            };

            yield return new IbanCountry("GR")
            {
                DisplayName = "Greece",
                EnglishName = "Greece",
                Iban = new IbanStructure(new IbanWikipediaPattern("7n,16c")),
                Bban = new BbanStructure(new WikipediaPattern("7n,16c"), 4)
            };

            yield return new IbanCountry("GL")
            {
                DisplayName = "Greenland",
                EnglishName = "Greenland",
                Iban = new IbanStructure(new IbanWikipediaPattern("14n")),
                Bban = new BbanStructure(new WikipediaPattern("14n"), 4)
            };

            yield return new IbanCountry("GT")
            {
                DisplayName = "Guatemala",
                EnglishName = "Guatemala",
                Iban = new IbanStructure(new IbanWikipediaPattern("4c,20c")),
                Bban = new BbanStructure(new WikipediaPattern("4c,20c"), 4)
            };

            yield return new IbanCountry("HU")
            {
                DisplayName = "Hungary",
                EnglishName = "Hungary",
                Iban = new IbanStructure(new IbanWikipediaPattern("24n")),
                Bban = new BbanStructure(new WikipediaPattern("24n"), 4)
            };

            yield return new IbanCountry("IS")
            {
                DisplayName = "Iceland",
                EnglishName = "Iceland",
                Iban = new IbanStructure(new IbanWikipediaPattern("22n")),
                Bban = new BbanStructure(new WikipediaPattern("22n"), 4)
            };

            yield return new IbanCountry("IQ")
            {
                DisplayName = "Iraq",
                EnglishName = "Iraq",
                Iban = new IbanStructure(new IbanWikipediaPattern("4a,15n")),
                Bban = new BbanStructure(new WikipediaPattern("4a,15n"), 4)
            };

            yield return new IbanCountry("IE")
            {
                DisplayName = "Ireland",
                EnglishName = "Ireland",
                Iban = new IbanStructure(new IbanWikipediaPattern("4c,14n")),
                Bban = new BbanStructure(new WikipediaPattern("4c,14n"), 4)
            };

            yield return new IbanCountry("IL")
            {
                DisplayName = "Israel",
                EnglishName = "Israel",
                Iban = new IbanStructure(new IbanWikipediaPattern("19n")),
                Bban = new BbanStructure(new WikipediaPattern("19n"), 4)
            };

            yield return new IbanCountry("IT")
            {
                DisplayName = "Italy",
                EnglishName = "Italy",
                Iban = new IbanStructure(new IbanWikipediaPattern("1a,10n,12c")),
                Bban = new BbanStructure(new WikipediaPattern("1a,10n,12c"), 4)
            };

            yield return new IbanCountry("JO")
            {
                DisplayName = "Jordan",
                EnglishName = "Jordan",
                Iban = new IbanStructure(new IbanWikipediaPattern("4a,22n")),
                Bban = new BbanStructure(new WikipediaPattern("4a,22n"), 4)
            };

            yield return new IbanCountry("KZ")
            {
                DisplayName = "Kazakhstan",
                EnglishName = "Kazakhstan",
                Iban = new IbanStructure(new IbanWikipediaPattern("3n,13c")),
                Bban = new BbanStructure(new WikipediaPattern("3n,13c"), 4)
            };

            yield return new IbanCountry("XK")
            {
                DisplayName = "Kosovo",
                EnglishName = "Kosovo",
                Iban = new IbanStructure(new IbanWikipediaPattern("4n,10n,2n")),
                Bban = new BbanStructure(new WikipediaPattern("4n,10n,2n"), 4)
            };

            yield return new IbanCountry("KW")
            {
                DisplayName = "Kuwait",
                EnglishName = "Kuwait",
                Iban = new IbanStructure(new IbanWikipediaPattern("4a,22c")),
                Bban = new BbanStructure(new WikipediaPattern("4a,22c"), 4)
            };

            yield return new IbanCountry("LV")
            {
                DisplayName = "Latvia",
                EnglishName = "Latvia",
                Iban = new IbanStructure(new IbanWikipediaPattern("4a,13c")),
                Bban = new BbanStructure(new WikipediaPattern("4a,13c"), 4)
            };

            yield return new IbanCountry("LB")
            {
                DisplayName = "Lebanon",
                EnglishName = "Lebanon",
                Iban = new IbanStructure(new IbanWikipediaPattern("4n,20c")),
                Bban = new BbanStructure(new WikipediaPattern("4n,20c"), 4)
            };

            yield return new IbanCountry("LY")
            {
                DisplayName = "Libya",
                EnglishName = "Libya",
                Iban = new IbanStructure(new IbanWikipediaPattern("21n")),
                Bban = new BbanStructure(new WikipediaPattern("21n"), 4)
            };

            yield return new IbanCountry("LI")
            {
                DisplayName = "Liechtenstein",
                EnglishName = "Liechtenstein",
                Iban = new IbanStructure(new IbanWikipediaPattern("5n,12c")),
                Bban = new BbanStructure(new WikipediaPattern("5n,12c"), 4)
            };

            yield return new IbanCountry("LT")
            {
                DisplayName = "Lithuania",
                EnglishName = "Lithuania",
                Iban = new IbanStructure(new IbanWikipediaPattern("16n")),
                Bban = new BbanStructure(new WikipediaPattern("16n"), 4)
            };

            yield return new IbanCountry("LU")
            {
                DisplayName = "Luxembourg",
                EnglishName = "Luxembourg",
                Iban = new IbanStructure(new IbanWikipediaPattern("3n,13c")),
                Bban = new BbanStructure(new WikipediaPattern("3n,13c"), 4)
            };

            yield return new IbanCountry("MK")
            {
                DisplayName = "North Macedonia",
                EnglishName = "North Macedonia",
                Iban = new IbanStructure(new IbanWikipediaPattern("3n,10c,2n")),
                Bban = new BbanStructure(new WikipediaPattern("3n,10c,2n"), 4)
            };

            yield return new IbanCountry("MT")
            {
                DisplayName = "Malta",
                EnglishName = "Malta",
                Iban = new IbanStructure(new IbanWikipediaPattern("4a,5n,18c")),
                Bban = new BbanStructure(new WikipediaPattern("4a,5n,18c"), 4)
            };

            yield return new IbanCountry("MR")
            {
                DisplayName = "Mauritania",
                EnglishName = "Mauritania",
                Iban = new IbanStructure(new IbanWikipediaPattern("23n")),
                Bban = new BbanStructure(new WikipediaPattern("23n"), 4)
            };

            yield return new IbanCountry("MU")
            {
                DisplayName = "Mauritius",
                EnglishName = "Mauritius",
                Iban = new IbanStructure(new IbanWikipediaPattern("4a,19n,3a")),
                Bban = new BbanStructure(new WikipediaPattern("4a,19n,3a"), 4)
            };

            yield return new IbanCountry("MC")
            {
                DisplayName = "Monaco",
                EnglishName = "Monaco",
                Iban = new IbanStructure(new IbanWikipediaPattern("10n,11c,2n")),
                Bban = new BbanStructure(new WikipediaPattern("10n,11c,2n"), 4)
            };

            yield return new IbanCountry("MD")
            {
                DisplayName = "Moldova",
                EnglishName = "Moldova",
                Iban = new IbanStructure(new IbanWikipediaPattern("2c,18c")),
                Bban = new BbanStructure(new WikipediaPattern("2c,18c"), 4)
            };

            yield return new IbanCountry("ME")
            {
                DisplayName = "Montenegro",
                EnglishName = "Montenegro",
                Iban = new IbanStructure(new IbanWikipediaPattern("18n")),
                Bban = new BbanStructure(new WikipediaPattern("18n"), 4)
            };

            yield return new IbanCountry("NL")
            {
                DisplayName = "Netherlands",
                EnglishName = "Netherlands",
                Iban = new IbanStructure(new IbanWikipediaPattern("4a,10n")),
                Bban = new BbanStructure(new WikipediaPattern("4a,10n"), 4)
            };

            yield return new IbanCountry("NO")
            {
                DisplayName = "Norway",
                EnglishName = "Norway",
                Iban = new IbanStructure(new IbanWikipediaPattern("11n")),
                Bban = new BbanStructure(new WikipediaPattern("11n"), 4)
            };

            yield return new IbanCountry("PK")
            {
                DisplayName = "Pakistan",
                EnglishName = "Pakistan",
                Iban = new IbanStructure(new IbanWikipediaPattern("4c,16n")),
                Bban = new BbanStructure(new WikipediaPattern("4c,16n"), 4)
            };

            yield return new IbanCountry("PS")
            {
                DisplayName = "Palestinian territories",
                EnglishName = "Palestinian territories",
                Iban = new IbanStructure(new IbanWikipediaPattern("4c,21n")),
                Bban = new BbanStructure(new WikipediaPattern("4c,21n"), 4)
            };

            yield return new IbanCountry("PL")
            {
                DisplayName = "Poland",
                EnglishName = "Poland",
                Iban = new IbanStructure(new IbanWikipediaPattern("24n")),
                Bban = new BbanStructure(new WikipediaPattern("24n"), 4)
            };

            yield return new IbanCountry("PT")
            {
                DisplayName = "Portugal",
                EnglishName = "Portugal",
                Iban = new IbanStructure(new IbanWikipediaPattern("21n")),
                Bban = new BbanStructure(new WikipediaPattern("21n"), 4)
            };

            yield return new IbanCountry("QA")
            {
                DisplayName = "Qatar",
                EnglishName = "Qatar",
                Iban = new IbanStructure(new IbanWikipediaPattern("4a,21c")),
                Bban = new BbanStructure(new WikipediaPattern("4a,21c"), 4)
            };

            yield return new IbanCountry("RO")
            {
                DisplayName = "Romania",
                EnglishName = "Romania",
                Iban = new IbanStructure(new IbanWikipediaPattern("4a,16c")),
                Bban = new BbanStructure(new WikipediaPattern("4a,16c"), 4)
            };

            yield return new IbanCountry("LC")
            {
                DisplayName = "Saint Lucia",
                EnglishName = "Saint Lucia",
                Iban = new IbanStructure(new IbanWikipediaPattern("4a,24c")),
                Bban = new BbanStructure(new WikipediaPattern("4a,24c"), 4)
            };

            yield return new IbanCountry("SM")
            {
                DisplayName = "San Marino",
                EnglishName = "San Marino",
                Iban = new IbanStructure(new IbanWikipediaPattern("1a,10n,12c")),
                Bban = new BbanStructure(new WikipediaPattern("1a,10n,12c"), 4)
            };

            yield return new IbanCountry("ST")
            {
                DisplayName = "Sao Tome and Principe",
                EnglishName = "Sao Tome and Principe",
                Iban = new IbanStructure(new IbanWikipediaPattern("21n")),
                Bban = new BbanStructure(new WikipediaPattern("21n"), 4)
            };

            yield return new IbanCountry("SA")
            {
                DisplayName = "Saudi Arabia",
                EnglishName = "Saudi Arabia",
                Iban = new IbanStructure(new IbanWikipediaPattern("2n,18c")),
                Bban = new BbanStructure(new WikipediaPattern("2n,18c"), 4)
            };

            yield return new IbanCountry("RS")
            {
                DisplayName = "Serbia",
                EnglishName = "Serbia",
                Iban = new IbanStructure(new IbanWikipediaPattern("18n")),
                Bban = new BbanStructure(new WikipediaPattern("18n"), 4)
            };

            yield return new IbanCountry("SC")
            {
                DisplayName = "Seychelles",
                EnglishName = "Seychelles",
                Iban = new IbanStructure(new IbanWikipediaPattern("4a,20n,3a")),
                Bban = new BbanStructure(new WikipediaPattern("4a,20n,3a"), 4)
            };

            yield return new IbanCountry("SK")
            {
                DisplayName = "Slovakia",
                EnglishName = "Slovakia",
                Iban = new IbanStructure(new IbanWikipediaPattern("20n")),
                Bban = new BbanStructure(new WikipediaPattern("20n"), 4)
            };

            yield return new IbanCountry("SI")
            {
                DisplayName = "Slovenia",
                EnglishName = "Slovenia",
                Iban = new IbanStructure(new IbanWikipediaPattern("15n")),
                Bban = new BbanStructure(new WikipediaPattern("15n"), 4)
            };

            yield return new IbanCountry("ES")
            {
                DisplayName = "Spain",
                EnglishName = "Spain",
                Iban = new IbanStructure(new IbanWikipediaPattern("20n")),
                Bban = new BbanStructure(new WikipediaPattern("20n"), 4)
            };

            yield return new IbanCountry("SE")
            {
                DisplayName = "Sweden",
                EnglishName = "Sweden",
                Iban = new IbanStructure(new IbanWikipediaPattern("20n")),
                Bban = new BbanStructure(new WikipediaPattern("20n"), 4)
            };

            yield return new IbanCountry("CH")
            {
                DisplayName = "Switzerland",
                EnglishName = "Switzerland",
                Iban = new IbanStructure(new IbanWikipediaPattern("5n,12c")),
                Bban = new BbanStructure(new WikipediaPattern("5n,12c"), 4)
            };

            yield return new IbanCountry("TN")
            {
                DisplayName = "Tunisia",
                EnglishName = "Tunisia",
                Iban = new IbanStructure(new IbanWikipediaPattern("20n")),
                Bban = new BbanStructure(new WikipediaPattern("20n"), 4)
            };

            yield return new IbanCountry("TR")
            {
                DisplayName = "Turkey",
                EnglishName = "Turkey",
                Iban = new IbanStructure(new IbanWikipediaPattern("5n,17c")),
                Bban = new BbanStructure(new WikipediaPattern("5n,17c"), 4)
            };

            yield return new IbanCountry("UA")
            {
                DisplayName = "Ukraine",
                EnglishName = "Ukraine",
                Iban = new IbanStructure(new IbanWikipediaPattern("6n,19c")),
                Bban = new BbanStructure(new WikipediaPattern("6n,19c"), 4)
            };

            yield return new IbanCountry("AE")
            {
                DisplayName = "United Arab Emirates",
                EnglishName = "United Arab Emirates",
                Iban = new IbanStructure(new IbanWikipediaPattern("3n,16n")),
                Bban = new BbanStructure(new WikipediaPattern("3n,16n"), 4)
            };

            yield return new IbanCountry("GB")
            {
                DisplayName = "United Kingdom",
                EnglishName = "United Kingdom",
                Iban = new IbanStructure(new IbanWikipediaPattern("4a,14n")),
                Bban = new BbanStructure(new WikipediaPattern("4a,14n"), 4)
            };

            yield return new IbanCountry("VA")
            {
                DisplayName = "Vatican City",
                EnglishName = "Vatican City",
                Iban = new IbanStructure(new IbanWikipediaPattern("3n,15n")),
                Bban = new BbanStructure(new WikipediaPattern("3n,15n"), 4)
            };

            yield return new IbanCountry("VG")
            {
                DisplayName = "Virgin Islands, British",
                EnglishName = "Virgin Islands, British",
                Iban = new IbanStructure(new IbanWikipediaPattern("4c,16n")),
                Bban = new BbanStructure(new WikipediaPattern("4c,16n"), 4)
            };

            yield return new IbanCountry("DZ")
            {
                DisplayName = "Algeria",
                EnglishName = "Algeria",
                Iban = new IbanStructure(new IbanWikipediaPattern("22n")),
                Bban = new BbanStructure(new WikipediaPattern("22n"), 4)
            };

            yield return new IbanCountry("AO")
            {
                DisplayName = "Angola",
                EnglishName = "Angola",
                Iban = new IbanStructure(new IbanWikipediaPattern("21n")),
                Bban = new BbanStructure(new WikipediaPattern("21n"), 4)
            };

            yield return new IbanCountry("BJ")
            {
                DisplayName = "Benin",
                EnglishName = "Benin",
                Iban = new IbanStructure(new IbanWikipediaPattern("2c,22n")),
                Bban = new BbanStructure(new WikipediaPattern("2c,22n"), 4)
            };

            yield return new IbanCountry("BF")
            {
                DisplayName = "Burkina Faso",
                EnglishName = "Burkina Faso",
                Iban = new IbanStructure(new IbanWikipediaPattern("2c,22n")),
                Bban = new BbanStructure(new WikipediaPattern("2c,22n"), 4)
            };

            yield return new IbanCountry("BI")
            {
                DisplayName = "Burundi",
                EnglishName = "Burundi",
                Iban = new IbanStructure(new IbanWikipediaPattern("12n")),
                Bban = new BbanStructure(new WikipediaPattern("12n"), 4)
            };

            yield return new IbanCountry("CV")
            {
                DisplayName = "Cabo Verde",
                EnglishName = "Cabo Verde",
                Iban = new IbanStructure(new IbanWikipediaPattern("21n")),
                Bban = new BbanStructure(new WikipediaPattern("21n"), 4)
            };

            yield return new IbanCountry("CM")
            {
                DisplayName = "Cameroon",
                EnglishName = "Cameroon",
                Iban = new IbanStructure(new IbanWikipediaPattern("23n")),
                Bban = new BbanStructure(new WikipediaPattern("23n"), 4)
            };

            yield return new IbanCountry("CF")
            {
                DisplayName = "Central African Republic",
                EnglishName = "Central African Republic",
                Iban = new IbanStructure(new IbanWikipediaPattern("23n")),
                Bban = new BbanStructure(new WikipediaPattern("23n"), 4)
            };

            yield return new IbanCountry("TD")
            {
                DisplayName = "Chad",
                EnglishName = "Chad",
                Iban = new IbanStructure(new IbanWikipediaPattern("23n")),
                Bban = new BbanStructure(new WikipediaPattern("23n"), 4)
            };

            yield return new IbanCountry("KM")
            {
                DisplayName = "Comoros",
                EnglishName = "Comoros",
                Iban = new IbanStructure(new IbanWikipediaPattern("23n")),
                Bban = new BbanStructure(new WikipediaPattern("23n"), 4)
            };

            yield return new IbanCountry("CG")
            {
                DisplayName = "Congo, Republic of the",
                EnglishName = "Congo, Republic of the",
                Iban = new IbanStructure(new IbanWikipediaPattern("23n")),
                Bban = new BbanStructure(new WikipediaPattern("23n"), 4)
            };

            yield return new IbanCountry("CI")
            {
                DisplayName = "Côte d'Ivoire",
                EnglishName = "Côte d'Ivoire",
                Iban = new IbanStructure(new IbanWikipediaPattern("1a,23n")),
                Bban = new BbanStructure(new WikipediaPattern("1a,23n"), 4)
            };

            yield return new IbanCountry("DJ")
            {
                DisplayName = "Djibouti",
                EnglishName = "Djibouti",
                Iban = new IbanStructure(new IbanWikipediaPattern("23n")),
                Bban = new BbanStructure(new WikipediaPattern("23n"), 4)
            };

            yield return new IbanCountry("GQ")
            {
                DisplayName = "Equatorial Guinea",
                EnglishName = "Equatorial Guinea",
                Iban = new IbanStructure(new IbanWikipediaPattern("23n")),
                Bban = new BbanStructure(new WikipediaPattern("23n"), 4)
            };

            yield return new IbanCountry("GA")
            {
                DisplayName = "Gabon",
                EnglishName = "Gabon",
                Iban = new IbanStructure(new IbanWikipediaPattern("23n")),
                Bban = new BbanStructure(new WikipediaPattern("23n"), 4)
            };

            yield return new IbanCountry("GW")
            {
                DisplayName = "Guinea-Bissau",
                EnglishName = "Guinea-Bissau",
                Iban = new IbanStructure(new IbanWikipediaPattern("2c,19n")),
                Bban = new BbanStructure(new WikipediaPattern("2c,19n"), 4)
            };

            yield return new IbanCountry("HN")
            {
                DisplayName = "Honduras",
                EnglishName = "Honduras",
                Iban = new IbanStructure(new IbanWikipediaPattern("4a,20n")),
                Bban = new BbanStructure(new WikipediaPattern("4a,20n"), 4)
            };

            yield return new IbanCountry("IR")
            {
                DisplayName = "Iran",
                EnglishName = "Iran",
                Iban = new IbanStructure(new IbanWikipediaPattern("22n")),
                Bban = new BbanStructure(new WikipediaPattern("22n"), 4)
            };

            yield return new IbanCountry("MG")
            {
                DisplayName = "Madagascar",
                EnglishName = "Madagascar",
                Iban = new IbanStructure(new IbanWikipediaPattern("23n")),
                Bban = new BbanStructure(new WikipediaPattern("23n"), 4)
            };

            yield return new IbanCountry("ML")
            {
                DisplayName = "Mali",
                EnglishName = "Mali",
                Iban = new IbanStructure(new IbanWikipediaPattern("2c,22n")),
                Bban = new BbanStructure(new WikipediaPattern("2c,22n"), 4)
            };

            yield return new IbanCountry("MA")
            {
                DisplayName = "Morocco",
                EnglishName = "Morocco",
                Iban = new IbanStructure(new IbanWikipediaPattern("24n")),
                Bban = new BbanStructure(new WikipediaPattern("24n"), 4)
            };

            yield return new IbanCountry("MZ")
            {
                DisplayName = "Mozambique",
                EnglishName = "Mozambique",
                Iban = new IbanStructure(new IbanWikipediaPattern("21n")),
                Bban = new BbanStructure(new WikipediaPattern("21n"), 4)
            };

            yield return new IbanCountry("NI")
            {
                DisplayName = "Nicaragua",
                EnglishName = "Nicaragua",
                Iban = new IbanStructure(new IbanWikipediaPattern("4a,24n")),
                Bban = new BbanStructure(new WikipediaPattern("4a,24n"), 4)
            };

            yield return new IbanCountry("NE")
            {
                DisplayName = "Niger",
                EnglishName = "Niger",
                Iban = new IbanStructure(new IbanWikipediaPattern("2a,22n")),
                Bban = new BbanStructure(new WikipediaPattern("2a,22n"), 4)
            };

            yield return new IbanCountry("SN")
            {
                DisplayName = "Senegal",
                EnglishName = "Senegal",
                Iban = new IbanStructure(new IbanWikipediaPattern("1a,23n")),
                Bban = new BbanStructure(new WikipediaPattern("1a,23n"), 4)
            };

            yield return new IbanCountry("TG")
            {
                DisplayName = "Togo",
                EnglishName = "Togo",
                Iban = new IbanStructure(new IbanWikipediaPattern("2a,22n")),
                Bban = new BbanStructure(new WikipediaPattern("2a,22n"), 4)
            };

            // ReSharper restore StringLiteralTypo
            // ReSharper restore CommentTypo
        }
    }
}
