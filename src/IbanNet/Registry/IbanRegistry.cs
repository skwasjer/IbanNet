using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace IbanNet.Registry
{
	/// <summary>
	/// The IBAN registry contains IBAN/BBAN/SEPA information for all known IBAN countries.
	/// </summary>
	/// <remarks>
	/// Generated from: swift_iban_registry_202005.r87.txt
	/// </remarks>
	[GeneratedCode("IbanRegistryT4", "1.1-r87")]
	public class IbanRegistry : List<CountryInfo>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="IbanRegistry" /> class.
		/// </summary>
		public IbanRegistry() {
			AddRange(
				new []
				{
					// ReSharper disable CommentTypo
					// ReSharper disable StringLiteralTypo

					// Andorra
					new CountryInfo
					{
						TwoLetterISORegionName = "AD",
						DisplayName = "Andorra",
						EnglishName = "Andorra",
						Bban = new BbanStructure
						{
							Length = 20,
							Structure = "4!n4!n12!c",
							Example = "00012030200359100100",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!n",
								Example = "0001",
							},
							Branch = new BranchStructure
							{
								Position = 4,
								Length = 4,
								Structure = "4!n",
								Example = "2030",
							}
						},
						Iban = new IbanStructure
						{
							Length = 24,
							Structure = "AD2!n4!n4!n12!c",
							Example = "AD1200012030200359100100",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "2030200359100100",
						LastUpdatedDate = new DateTimeOffset(2009, 11, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// United Arab Emirates (The)
					new CountryInfo
					{
						TwoLetterISORegionName = "AE",
						DisplayName = "United Arab Emirates (The)",
						EnglishName = "United Arab Emirates (The)",
						Bban = new BbanStructure
						{
							Length = 19,
							Structure = "3!n16!n",
							Example = "0331234567890123456",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 3,
								Structure = "3!n",
								Example = "033",
							},
						},
						Iban = new IbanStructure
						{
							Length = 23,
							Structure = "AE2!n3!n16!n",
							Example = "AE070331234567890123456",
							EffectiveDate = new DateTimeOffset(2011, 10, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "1234567890123456",
						LastUpdatedDate = new DateTimeOffset(2015, 2, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Albania
					new CountryInfo
					{
						TwoLetterISORegionName = "AL",
						DisplayName = "Albania",
						EnglishName = "Albania",
						Bban = new BbanStructure
						{
							Length = 24,
							Structure = "8!n16!c",
							Example = "212110090000000235698741",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 3,
								Structure = "8!n",
								Example = "21211009",
							},
							Branch = new BranchStructure
							{
								Position = 3,
								Length = 5,
								Structure = "5!n",
								Example = "1100",
							}
						},
						Iban = new IbanStructure
						{
							Length = 28,
							Structure = "AL2!n8!n16!c",
							Example = "AL47212110090000000235698741",
							EffectiveDate = new DateTimeOffset(2009, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "0000000235698741",
						LastUpdatedDate = new DateTimeOffset(2011, 4, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Austria
					new CountryInfo
					{
						TwoLetterISORegionName = "AT",
						DisplayName = "Austria",
						EnglishName = "Austria",
						Bban = new BbanStructure
						{
							Length = 16,
							Structure = "5!n11!n",
							Example = "1904300234573201",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 5,
								Structure = "5!n",
								Example = "19043",
							},
						},
						Iban = new IbanStructure
						{
							Length = 20,
							Structure = "AT2!n5!n11!n",
							Example = "AT611904300234573201",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "BLZ 19043 Kto 234573201",
						LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Azerbaijan
					new CountryInfo
					{
						TwoLetterISORegionName = "AZ",
						DisplayName = "Azerbaijan",
						EnglishName = "Azerbaijan",
						Bban = new BbanStructure
						{
							Length = 24,
							Structure = "4!a20!c",
							Example = "NABZ00000000137010001944",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!a",
								Example = "NABZ",
							},
						},
						Iban = new IbanStructure
						{
							Length = 28,
							Structure = "AZ2!n4!a20!c",
							Example = "AZ21NABZ00000000137010001944",
							EffectiveDate = new DateTimeOffset(2013, 1, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "NABZ00000000137010001944",
						LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Bosnia and Herzegovina
					new CountryInfo
					{
						TwoLetterISORegionName = "BA",
						DisplayName = "Bosnia and Herzegovina",
						EnglishName = "Bosnia and Herzegovina",
						Bban = new BbanStructure
						{
							Length = 16,
							Structure = "3!n3!n8!n2!n",
							Example = "1990440001200279",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 3,
								Structure = "3!n",
								Example = "199",
							},
							Branch = new BranchStructure
							{
								Position = 3,
								Length = 3,
								Structure = "3!n",
								Example = "044",
							}
						},
						Iban = new IbanStructure
						{
							Length = 20,
							Structure = "BA2!n3!n3!n8!n2!n",
							Example = "BA391290079401028494",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "199-044-00012002-79",
						LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Belgium
					new CountryInfo
					{
						TwoLetterISORegionName = "BE",
						DisplayName = "Belgium",
						EnglishName = "Belgium",
						Bban = new BbanStructure
						{
							Length = 12,
							Structure = "3!n7!n2!n",
							Example = "539007547034",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 3,
								Structure = "3!n",
								Example = "539",
							},
						},
						Iban = new IbanStructure
						{
							Length = 16,
							Structure = "BE2!n3!n7!n2!n",
							Example = "BE68539007547034",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "BE68 5390 0754 7034",
						LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Bulgaria
					new CountryInfo
					{
						TwoLetterISORegionName = "BG",
						DisplayName = "Bulgaria",
						EnglishName = "Bulgaria",
						Bban = new BbanStructure
						{
							Length = 18,
							Structure = "4!a4!n2!n8!c",
							Example = "BNBG96611020345678",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!a",
								Example = "BNBG",
							},
							Branch = new BranchStructure
							{
								Position = 4,
								Length = 4,
								Structure = "4!n",
								Example = "9661",
							}
						},
						Iban = new IbanStructure
						{
							Length = 22,
							Structure = "BG2!n4!a4!n2!n8!c",
							Example = "BG80BNBG96611020345678",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "",
						LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Bahrain
					new CountryInfo
					{
						TwoLetterISORegionName = "BH",
						DisplayName = "Bahrain",
						EnglishName = "Bahrain",
						Bban = new BbanStructure
						{
							Length = 18,
							Structure = "4!a14!c",
							Example = "BMAG00001299123456",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!n",
								Example = "BMAG",
							},
						},
						Iban = new IbanStructure
						{
							Length = 22,
							Structure = "BH2!n4!a14!c",
							Example = "BH67BMAG00001299123456",
							EffectiveDate = new DateTimeOffset(2012, 1, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "00001299123456",
						LastUpdatedDate = new DateTimeOffset(2012, 1, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Brazil
					new CountryInfo
					{
						TwoLetterISORegionName = "BR",
						DisplayName = "Brazil",
						EnglishName = "Brazil",
						Bban = new BbanStructure
						{
							Length = 25,
							Structure = "8!n5!n10!n1!a1!c",
							Example = "00360305000010009795493P1",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 8,
								Structure = "8!n",
								Example = "00360305",
							},
							Branch = new BranchStructure
							{
								Position = 8,
								Length = 5,
								Structure = "5!n",
								Example = "00001",
							}
						},
						Iban = new IbanStructure
						{
							Length = 29,
							Structure = "BR2!n8!n5!n10!n1!a1!c",
							Example = "BR1800360305000010009795493C1",
							EffectiveDate = new DateTimeOffset(2013, 7, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "0009795493C1",
						LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Republic of Belarus
					new CountryInfo
					{
						TwoLetterISORegionName = "BY",
						DisplayName = "Republic of Belarus",
						EnglishName = "Republic of Belarus",
						Bban = new BbanStructure
						{
							Length = 24,
							Structure = "4!c4!n16!c",
							Example = "NBRB3600900000002Z00AB00",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!c",
								Example = "NBRB",
							},
						},
						Iban = new IbanStructure
						{
							Length = 28,
							Structure = "BY2!n4!c4!n16!c",
							Example = "BY13NBRB3600900000002Z00AB00",
							EffectiveDate = new DateTimeOffset(2017, 7, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "3600 0000 0000 0Z00 AB00",
						LastUpdatedDate = new DateTimeOffset(2017, 3, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Switzerland
					new CountryInfo
					{
						TwoLetterISORegionName = "CH",
						DisplayName = "Switzerland",
						EnglishName = "Switzerland",
						Bban = new BbanStructure
						{
							Length = 17,
							Structure = "5!n12!c",
							Example = "00762011623852957",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 5,
								Structure = "5!n",
								Example = "00762",
							},
						},
						Iban = new IbanStructure
						{
							Length = 21,
							Structure = "CH2!n5!n12!c",
							Example = "CH9300762011623852957",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "762 1162-3852.957",
						LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Costa Rica
					new CountryInfo
					{
						TwoLetterISORegionName = "CR",
						DisplayName = "Costa Rica",
						EnglishName = "Costa Rica",
						Bban = new BbanStructure
						{
							Length = 18,
							Structure = "4!n14!n",
							Example = "15202001026284066",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!n",
								Example = "0152",
							},
						},
						Iban = new IbanStructure
						{
							Length = 22,
							Structure = "CR2!n4!n14!n",
							Example = "CR05015202001026284066",
							EffectiveDate = new DateTimeOffset(2011, 6, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "02001026284066",
						LastUpdatedDate = new DateTimeOffset(2019, 1, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Cyprus
					new CountryInfo
					{
						TwoLetterISORegionName = "CY",
						DisplayName = "Cyprus",
						EnglishName = "Cyprus",
						Bban = new BbanStructure
						{
							Length = 24,
							Structure = "3!n5!n16!c",
							Example = "002001280000001200527600",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 3,
								Structure = "3!n",
								Example = "002",
							},
							Branch = new BranchStructure
							{
								Position = 3,
								Length = 5,
								Structure = "5!n",
								Example = "00128",
							}
						},
						Iban = new IbanStructure
						{
							Length = 28,
							Structure = "CY2!n3!n5!n16!c",
							Example = "CY17002001280000001200527600",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "0000001200527600",
						LastUpdatedDate = new DateTimeOffset(2009, 8, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Czechia
					new CountryInfo
					{
						TwoLetterISORegionName = "CZ",
						DisplayName = "Czechia",
						EnglishName = "Czechia",
						Bban = new BbanStructure
						{
							Length = 20,
							Structure = "4!n6!n10!n",
							Example = "08000000192000145399",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!n",
								Example = "0800",
							},
						},
						Iban = new IbanStructure
						{
							Length = 24,
							Structure = "CZ2!n4!n6!n10!n",
							Example = "CZ6508000000192000145399",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "19-2000145399/0800",
						LastUpdatedDate = new DateTimeOffset(2016, 12, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Germany
					new CountryInfo
					{
						TwoLetterISORegionName = "DE",
						DisplayName = "Germany",
						EnglishName = "Germany",
						Bban = new BbanStructure
						{
							Length = 18,
							Structure = "8!n10!n",
							Example = "370400440532013000",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 8,
								Structure = "8!n",
								Example = "37040044",
							},
						},
						Iban = new IbanStructure
						{
							Length = 22,
							Structure = "DE2!n8!n10!n",
							Example = "DE89370400440532013000",
							EffectiveDate = new DateTimeOffset(2007, 7, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "532013000",
						LastUpdatedDate = new DateTimeOffset(2011, 1, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Denmark
					new CountryInfo
					{
						TwoLetterISORegionName = "DK",
						DisplayName = "Denmark",
						EnglishName = "Denmark",
						Bban = new BbanStructure
						{
							Length = 14,
							Structure = "4!n9!n1!n",
							Example = "00400440116243",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!n",
								Example = "0040",
							},
						},
						Iban = new IbanStructure
						{
							Length = 18,
							Structure = "DK2!n4!n9!n1!n",
							Example = "DK5000400440116243",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "0040 0440116243",
						LastUpdatedDate = new DateTimeOffset(2018, 11, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Dominican Republic
					new CountryInfo
					{
						TwoLetterISORegionName = "DO",
						DisplayName = "Dominican Republic",
						EnglishName = "Dominican Republic",
						Bban = new BbanStructure
						{
							Length = 24,
							Structure = "4!c20!n",
							Example = "BAGR00000001212453611324",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!c",
								Example = "BAGR",
							},
						},
						Iban = new IbanStructure
						{
							Length = 28,
							Structure = "DO2!n4!c20!n",
							Example = "DO28BAGR00000001212453611324",
							EffectiveDate = new DateTimeOffset(2010, 12, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "00000001212453611324",
						LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Estonia
					new CountryInfo
					{
						TwoLetterISORegionName = "EE",
						DisplayName = "Estonia",
						EnglishName = "Estonia",
						Bban = new BbanStructure
						{
							Length = 16,
							Structure = "2!n2!n11!n1!n",
							Example = "2200221020145685",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 2,
								Structure = "2!n",
								Example = "22",
							},
						},
						Iban = new IbanStructure
						{
							Length = 20,
							Structure = "EE2!n2!n2!n11!n1!n",
							Example = "EE382200221020145685",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "221020145685",
						LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Egypt
					new CountryInfo
					{
						TwoLetterISORegionName = "EG",
						DisplayName = "Egypt",
						EnglishName = "Egypt",
						Bban = new BbanStructure
						{
							Length = 25,
							Structure = "4!n4!n17!n",
							Example = "0019000500000000263180002",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!n",
								Example = "0019",
							},
							Branch = new BranchStructure
							{
								Position = 4,
								Length = 4,
								Structure = "4!n",
								Example = "0005",
							}
						},
						Iban = new IbanStructure
						{
							Length = 29,
							Structure = "EG2!n4!n4!n17!n",
							Example = "EG380019000500000000263180002",
							EffectiveDate = new DateTimeOffset(2021, 1, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "000263180002",
						LastUpdatedDate = new DateTimeOffset(2020, 1, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Spain
					new CountryInfo
					{
						TwoLetterISORegionName = "ES",
						DisplayName = "Spain",
						EnglishName = "Spain",
						Bban = new BbanStructure
						{
							Length = 20,
							Structure = "4!n4!n1!n1!n10!n",
							Example = "21000418450200051332",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!n",
								Example = "2100",
							},
							Branch = new BranchStructure
							{
								Position = 4,
								Length = 4,
								Structure = "4!n",
								Example = "0418",
							}
						},
						Iban = new IbanStructure
						{
							Length = 24,
							Structure = "ES2!n4!n4!n1!n1!n10!n",
							Example = "ES9121000418450200051332",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "2100 0418 45 0200051332",
						LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Finland
					new CountryInfo
					{
						TwoLetterISORegionName = "FI",
						DisplayName = "Finland",
						EnglishName = "Finland",
						IncludedCountries = new[]
						{
							"fi-AX"
						},
						Bban = new BbanStructure
						{
							Length = 14,
							Structure = "3!n11!n",
							Example = "",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 3,
								Structure = "",
								Example = "123",
							},
						},
						Iban = new IbanStructure
						{
							Length = 18,
							Structure = "FI2!n3!n11!n",
							Example = "FI2112345600000785",
							EffectiveDate = new DateTimeOffset(2011, 12, 1, 0, 0, 0, TimeSpan.Zero)
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
					},

					// Faroe Islands
					new CountryInfo
					{
						TwoLetterISORegionName = "FO",
						DisplayName = "Faroe Islands",
						EnglishName = "Faroe Islands",
						Bban = new BbanStructure
						{
							Length = 14,
							Structure = "4!n9!n1!n",
							Example = "64600001631634",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!n",
								Example = "6460",
							},
						},
						Iban = new IbanStructure
						{
							Length = 18,
							Structure = "FO2!n4!n9!n1!n",
							Example = "FO6264600001631634",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "6460 0001631634",
						LastUpdatedDate = new DateTimeOffset(2017, 2, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// France
					new CountryInfo
					{
						TwoLetterISORegionName = "FR",
						DisplayName = "France",
						EnglishName = "France",
						IncludedCountries = new[]
						{
							"fr-GF", "fr-GP", "fr-MQ", "fr-RE", "fr-PF", "fr-TF", "fr-YT", "fr-NC", "fr-BL", "fr-MF", "fr-PM", "fr-WF"
						},
						Bban = new BbanStructure
						{
							Length = 23,
							Structure = "5!n5!n11!c2!n",
							Example = "20041010050500013M02606",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 5,
								Structure = "5!n",
								Example = "20041",
							},
						},
						Iban = new IbanStructure
						{
							Length = 27,
							Structure = "FR2!n5!n5!n11!c2!n",
							Example = "FR1420041010050500013M02606",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
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
					},

					// United Kingdom
					new CountryInfo
					{
						TwoLetterISORegionName = "GB",
						DisplayName = "United Kingdom",
						EnglishName = "United Kingdom",
						IncludedCountries = new[]
						{
							"gb-IM", "gb-JE", "gb-GG"
						},
						Bban = new BbanStructure
						{
							Length = 18,
							Structure = "4!a6!n8!n",
							Example = "NWBK60161331926819",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!a",
								Example = "NWBK",
							},
							Branch = new BranchStructure
							{
								Position = 4,
								Length = 6,
								Structure = "6!n",
								Example = "601613",
							}
						},
						Iban = new IbanStructure
						{
							Length = 22,
							Structure = "GB2!n4!a6!n8!n",
							Example = "GB29NWBK60161331926819",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "60-16-13 31926819",
						LastUpdatedDate = new DateTimeOffset(2017, 5, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Georgia
					new CountryInfo
					{
						TwoLetterISORegionName = "GE",
						DisplayName = "Georgia",
						EnglishName = "Georgia",
						Bban = new BbanStructure
						{
							Length = 18,
							Structure = "2!a16!n",
							Example = "NB0000000101904917",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 2,
								Structure = "2!a",
								Example = "NB",
							},
						},
						Iban = new IbanStructure
						{
							Length = 22,
							Structure = "GE2!n2!a16!n",
							Example = "GE29NB0000000101904917",
							EffectiveDate = new DateTimeOffset(2010, 5, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "0000000101904917",
						LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Gibraltar
					new CountryInfo
					{
						TwoLetterISORegionName = "GI",
						DisplayName = "Gibraltar",
						EnglishName = "Gibraltar",
						Bban = new BbanStructure
						{
							Length = 19,
							Structure = "4!a15!c",
							Example = "NWBK000000007099453",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!a",
								Example = "NWBK",
							},
						},
						Iban = new IbanStructure
						{
							Length = 23,
							Structure = "GI2!n4!a15!c",
							Example = "GI75NWBK000000007099453",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "0000 00007099 453",
						LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Greenland
					new CountryInfo
					{
						TwoLetterISORegionName = "GL",
						DisplayName = "Greenland",
						EnglishName = "Greenland",
						Bban = new BbanStructure
						{
							Length = 14,
							Structure = "4!n9!n1!n",
							Example = "64710001000206",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!n",
								Example = "6471",
							},
						},
						Iban = new IbanStructure
						{
							Length = 18,
							Structure = "GL2!n4!n9!n1!n",
							Example = "GL8964710001000206",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "6471 0001000206",
						LastUpdatedDate = new DateTimeOffset(2018, 11, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Greece
					new CountryInfo
					{
						TwoLetterISORegionName = "GR",
						DisplayName = "Greece",
						EnglishName = "Greece",
						Bban = new BbanStructure
						{
							Length = 23,
							Structure = "3!n4!n16!c",
							Example = "01101250000000012300695",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 3,
								Structure = "3!n",
								Example = "011",
							},
							Branch = new BranchStructure
							{
								Position = 3,
								Length = 4,
								Structure = "4!n",
								Example = "0125",
							}
						},
						Iban = new IbanStructure
						{
							Length = 27,
							Structure = "GR2!n3!n4!n16!c",
							Example = "GR1601101250000000012300695",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "01250000000012300695",
						LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Guatemala
					new CountryInfo
					{
						TwoLetterISORegionName = "GT",
						DisplayName = "Guatemala",
						EnglishName = "Guatemala",
						Bban = new BbanStructure
						{
							Length = 24,
							Structure = "4!c20!c",
							Example = "TRAJ01020000001210029690",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!c",
								Example = "TRAJ",
							},
						},
						Iban = new IbanStructure
						{
							Length = 28,
							Structure = "GT2!n4!c20!c",
							Example = "GT82TRAJ01020000001210029690",
							EffectiveDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "01020000001210029690",
						LastUpdatedDate = new DateTimeOffset(2016, 10, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Croatia
					new CountryInfo
					{
						TwoLetterISORegionName = "HR",
						DisplayName = "Croatia",
						EnglishName = "Croatia",
						Bban = new BbanStructure
						{
							Length = 17,
							Structure = "7!n10!n",
							Example = "10010051863000160",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 7,
								Structure = "7!n",
								Example = "1001005",
							},
						},
						Iban = new IbanStructure
						{
							Length = 21,
							Structure = "HR2!n7!n10!n",
							Example = "HR1210010051863000160",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "1001005-1863000160",
						LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Hungary
					new CountryInfo
					{
						TwoLetterISORegionName = "HU",
						DisplayName = "Hungary",
						EnglishName = "Hungary",
						Bban = new BbanStructure
						{
							Length = 24,
							Structure = "3!n4!n1!n15!n1!n",
							Example = "117730161111101800000000",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 3,
								Structure = "3!n",
								Example = "117",
							},
							Branch = new BranchStructure
							{
								Position = 3,
								Length = 4,
								Structure = "4!n",
								Example = "7301",
							}
						},
						Iban = new IbanStructure
						{
							Length = 28,
							Structure = "HU2!n3!n4!n1!n15!n1!n",
							Example = "HU42117730161111101800000000",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "11773016-11111018-00000000",
						LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Ireland
					new CountryInfo
					{
						TwoLetterISORegionName = "IE",
						DisplayName = "Ireland",
						EnglishName = "Ireland",
						Bban = new BbanStructure
						{
							Length = 18,
							Structure = "4!a6!n8!n",
							Example = "AIBK93115212345678",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!a",
								Example = "AIBK",
							},
							Branch = new BranchStructure
							{
								Position = 4,
								Length = 6,
								Structure = "6!n",
								Example = "931152",
							}
						},
						Iban = new IbanStructure
						{
							Length = 22,
							Structure = "IE2!n4!a6!n8!n",
							Example = "IE29AIBK93115212345678",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "93-11-52 12345678",
						LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Israel
					new CountryInfo
					{
						TwoLetterISORegionName = "IL",
						DisplayName = "Israel",
						EnglishName = "Israel",
						Bban = new BbanStructure
						{
							Length = 19,
							Structure = "3!n3!n13!n",
							Example = "010800000099999999",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 3,
								Structure = "3!n",
								Example = "010",
							},
							Branch = new BranchStructure
							{
								Position = 3,
								Length = 3,
								Structure = "3!n",
								Example = "800",
							}
						},
						Iban = new IbanStructure
						{
							Length = 23,
							Structure = "IL2!n3!n3!n13!n",
							Example = "IL620108000000099999999",
							EffectiveDate = new DateTimeOffset(2007, 7, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "10-800-99999999",
						LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Iraq
					new CountryInfo
					{
						TwoLetterISORegionName = "IQ",
						DisplayName = "Iraq",
						EnglishName = "Iraq",
						Bban = new BbanStructure
						{
							Length = 19,
							Structure = "4!a3!n12!n",
							Example = "NBIQ850123456789012",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4",
								Example = "NBIQ",
							},
							Branch = new BranchStructure
							{
								Position = 4,
								Length = 3,
								Structure = "3",
								Example = "850",
							}
						},
						Iban = new IbanStructure
						{
							Length = 23,
							Structure = "IQ2!n4!a3!n12!n",
							Example = "IQ98NBIQ850123456789012",
							EffectiveDate = new DateTimeOffset(2017, 1, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "123456789012",
						LastUpdatedDate = new DateTimeOffset(2016, 11, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Iceland
					new CountryInfo
					{
						TwoLetterISORegionName = "IS",
						DisplayName = "Iceland",
						EnglishName = "Iceland",
						Bban = new BbanStructure
						{
							Length = 22,
							Structure = "4!n2!n6!n10!n",
							Example = "0159260076545510730339",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 2,
								Structure = "2!n",
								Example = "01",
							},
							Branch = new BranchStructure
							{
								Position = 2,
								Length = 2,
								Structure = "2!n",
								Example = "59",
							}
						},
						Iban = new IbanStructure
						{
							Length = 26,
							Structure = "IS2!n4!n2!n6!n10!n",
							Example = "IS140159260076545510730339",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "0159-26-007654-551073-0339",
						LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Italy
					new CountryInfo
					{
						TwoLetterISORegionName = "IT",
						DisplayName = "Italy",
						EnglishName = "Italy",
						Bban = new BbanStructure
						{
							Length = 23,
							Structure = "1!a5!n5!n12!c",
							Example = "X0542811101000000123456",
							Bank = new BankStructure
							{
								Position = 1,
								Length = 5,
								Structure = "5!n",
								Example = "05428",
							},
							Branch = new BranchStructure
							{
								Position = 6,
								Length = 5,
								Structure = "5!n",
								Example = "11101",
							}
						},
						Iban = new IbanStructure
						{
							Length = 27,
							Structure = "IT2!n1!a5!n5!n12!c",
							Example = "IT60X0542811101000000123456",
							EffectiveDate = new DateTimeOffset(2007, 7, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "X 05428 11101 000000123456",
						LastUpdatedDate = new DateTimeOffset(2013, 3, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Jordan
					new CountryInfo
					{
						TwoLetterISORegionName = "JO",
						DisplayName = "Jordan",
						EnglishName = "Jordan",
						Bban = new BbanStructure
						{
							Length = 26,
							Structure = "4!a4!n18!c",
							Example = "CBJO0010000000000131000302",
							Bank = new BankStructure
							{
								Position = 4,
								Length = 4,
								Structure = "4!n",
								Example = "CBJO",
							},
							Branch = new BranchStructure
							{
								Position = 4,
								Length = 4,
								Structure = "4!n",
								Example = "",
							}
						},
						Iban = new IbanStructure
						{
							Length = 30,
							Structure = "JO2!n4!a4!n18!c",
							Example = "JO94CBJO0010000000000131000302",
							EffectiveDate = new DateTimeOffset(2014, 2, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "0001310000302",
						LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Kuwait
					new CountryInfo
					{
						TwoLetterISORegionName = "KW",
						DisplayName = "Kuwait",
						EnglishName = "Kuwait",
						Bban = new BbanStructure
						{
							Length = 26,
							Structure = "4!a22!c",
							Example = "CBKU0000000000001234560101",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!a",
								Example = "CBKU",
							},
						},
						Iban = new IbanStructure
						{
							Length = 30,
							Structure = "KW2!n4!a22!c",
							Example = "KW81CBKU0000000000001234560101",
							EffectiveDate = new DateTimeOffset(2011, 1, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "1234560101",
						LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Kazakhstan
					new CountryInfo
					{
						TwoLetterISORegionName = "KZ",
						DisplayName = "Kazakhstan",
						EnglishName = "Kazakhstan",
						Bban = new BbanStructure
						{
							Length = 16,
							Structure = "3!n13!c",
							Example = "125KZT5004100100",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 3,
								Structure = "3!n",
								Example = "125",
							},
						},
						Iban = new IbanStructure
						{
							Length = 20,
							Structure = "KZ2!n3!n13!c",
							Example = "KZ86125KZT5004100100",
							EffectiveDate = new DateTimeOffset(2010, 9, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "KZ86 125K ZT50 0410 0100",
						LastUpdatedDate = new DateTimeOffset(2016, 3, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Lebanon
					new CountryInfo
					{
						TwoLetterISORegionName = "LB",
						DisplayName = "Lebanon",
						EnglishName = "Lebanon",
						Bban = new BbanStructure
						{
							Length = 24,
							Structure = "4!n20!c",
							Example = "099900000001001901229114",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!a",
								Example = "0999",
							},
						},
						Iban = new IbanStructure
						{
							Length = 28,
							Structure = "LB2!n4!n20!c",
							Example = "LB62099900000001001901229114",
							EffectiveDate = new DateTimeOffset(2010, 1, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "01 001 901229114",
						LastUpdatedDate = new DateTimeOffset(2010, 1, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Saint Lucia
					new CountryInfo
					{
						TwoLetterISORegionName = "LC",
						DisplayName = "Saint Lucia",
						EnglishName = "Saint Lucia",
						Bban = new BbanStructure
						{
							Length = 28,
							Structure = "4!a24!c",
							Example = "HEMM000100010012001200023015",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!a",
								Example = "HEMM",
							},
						},
						Iban = new IbanStructure
						{
							Length = 32,
							Structure = "LC2!n4!a24!c",
							Example = "LC55HEMM000100010012001200023015",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "0001 0001 0012 0012 0002 3015",
						LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Liechtenstein
					new CountryInfo
					{
						TwoLetterISORegionName = "LI",
						DisplayName = "Liechtenstein",
						EnglishName = "Liechtenstein",
						Bban = new BbanStructure
						{
							Length = 17,
							Structure = "5!n12!c",
							Example = "088100002324013AA",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 5,
								Structure = "5!n",
								Example = "08810",
							},
						},
						Iban = new IbanStructure
						{
							Length = 21,
							Structure = "LI2!n5!n12!c",
							Example = "LI21088100002324013AA",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "8810 2324013AA",
						LastUpdatedDate = new DateTimeOffset(2012, 4, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Lithuania
					new CountryInfo
					{
						TwoLetterISORegionName = "LT",
						DisplayName = "Lithuania",
						EnglishName = "Lithuania",
						Bban = new BbanStructure
						{
							Length = 16,
							Structure = "5!n11!n",
							Example = "1000011101001000",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 5,
								Structure = "5!n",
								Example = "10000",
							},
						},
						Iban = new IbanStructure
						{
							Length = 20,
							Structure = "LT2!n5!n11!n",
							Example = "LT121000011101001000",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "",
						LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Luxembourg
					new CountryInfo
					{
						TwoLetterISORegionName = "LU",
						DisplayName = "Luxembourg",
						EnglishName = "Luxembourg",
						Bban = new BbanStructure
						{
							Length = 16,
							Structure = "3!n13!c",
							Example = "0019400644750000",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 3,
								Structure = "3!n",
								Example = "001",
							},
						},
						Iban = new IbanStructure
						{
							Length = 20,
							Structure = "LU2!n3!n13!c",
							Example = "LU280019400644750000",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "",
						LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Latvia
					new CountryInfo
					{
						TwoLetterISORegionName = "LV",
						DisplayName = "Latvia",
						EnglishName = "Latvia",
						Bban = new BbanStructure
						{
							Length = 17,
							Structure = "4!a13!c",
							Example = "BANK0000435195001",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!a",
								Example = "BANK",
							},
						},
						Iban = new IbanStructure
						{
							Length = 21,
							Structure = "LV2!n4!a13!c",
							Example = "LV80BANK0000435195001",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "LV80 BANK 0000 4351 9500 1",
						LastUpdatedDate = new DateTimeOffset(2009, 1, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Monaco
					new CountryInfo
					{
						TwoLetterISORegionName = "MC",
						DisplayName = "Monaco",
						EnglishName = "Monaco",
						Bban = new BbanStructure
						{
							Length = 23,
							Structure = "5!n5!n11!c2!n",
							Example = "11222000010123456789030",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 5,
								Structure = "5!n",
								Example = "11222",
							},
							Branch = new BranchStructure
							{
								Position = 5,
								Length = 5,
								Structure = "5!n",
								Example = "00001",
							}
						},
						Iban = new IbanStructure
						{
							Length = 27,
							Structure = "MC2!n5!n5!n11!c2!n",
							Example = "MC5811222000010123456789030",
							EffectiveDate = new DateTimeOffset(2008, 1, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "0011111000h",
						LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Moldova
					new CountryInfo
					{
						TwoLetterISORegionName = "MD",
						DisplayName = "Moldova",
						EnglishName = "Moldova",
						Bban = new BbanStructure
						{
							Length = 20,
							Structure = "2!c18!c",
							Example = "AG000225100013104168",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 2,
								Structure = "2!c",
								Example = "AG",
							},
						},
						Iban = new IbanStructure
						{
							Length = 24,
							Structure = "MD2!n2!c18!c",
							Example = "MD24AG000225100013104168",
							EffectiveDate = new DateTimeOffset(2016, 1, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "000225100013104168",
						LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Montenegro
					new CountryInfo
					{
						TwoLetterISORegionName = "ME",
						DisplayName = "Montenegro",
						EnglishName = "Montenegro",
						Bban = new BbanStructure
						{
							Length = 18,
							Structure = "3!n13!n2!n",
							Example = "505000012345678951",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 3,
								Structure = "3!n",
								Example = "505",
							},
						},
						Iban = new IbanStructure
						{
							Length = 22,
							Structure = "ME2!n3!n13!n2!n",
							Example = "ME25505000012345678951",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "505 0000123456789 51",
						LastUpdatedDate = new DateTimeOffset(2010, 5, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Macedonia
					new CountryInfo
					{
						TwoLetterISORegionName = "MK",
						DisplayName = "Macedonia",
						EnglishName = "Macedonia",
						Bban = new BbanStructure
						{
							Length = 15,
							Structure = "3!n10!c2!n",
							Example = "250120000058984",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 3,
								Structure = "3!n",
								Example = "300",
							},
						},
						Iban = new IbanStructure
						{
							Length = 19,
							Structure = "MK2!n3!n10!c2!n",
							Example = "MK07250120000058984",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "MK07 300 0000000424 25",
						LastUpdatedDate = new DateTimeOffset(2011, 1, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Mauritania
					new CountryInfo
					{
						TwoLetterISORegionName = "MR",
						DisplayName = "Mauritania",
						EnglishName = "Mauritania",
						Bban = new BbanStructure
						{
							Length = 23,
							Structure = "5!n5!n11!n2!n",
							Example = "00020001010000123456753",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 5,
								Structure = "5!n",
								Example = "00020",
							},
							Branch = new BranchStructure
							{
								Position = 5,
								Length = 5,
								Structure = "5!n",
								Example = "00101",
							}
						},
						Iban = new IbanStructure
						{
							Length = 27,
							Structure = "MR2!n5!n5!n11!n2!n",
							Example = "MR1300020001010000123456753",
							EffectiveDate = new DateTimeOffset(2012, 1, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "00020 00101 00001234567 53",
						LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Malta
					new CountryInfo
					{
						TwoLetterISORegionName = "MT",
						DisplayName = "Malta",
						EnglishName = "Malta",
						Bban = new BbanStructure
						{
							Length = 27,
							Structure = "4!a5!n18!c",
							Example = "MALT011000012345MTLCAST001S",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!a",
								Example = "MALT",
							},
							Branch = new BranchStructure
							{
								Position = 4,
								Length = 5,
								Structure = "5!n",
								Example = "01100",
							}
						},
						Iban = new IbanStructure
						{
							Length = 31,
							Structure = "MT2!n4!a5!n18!c",
							Example = "MT84MALT011000012345MTLCAST001S",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "12345MTLCAST001S",
						LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Mauritius
					new CountryInfo
					{
						TwoLetterISORegionName = "MU",
						DisplayName = "Mauritius",
						EnglishName = "Mauritius",
						Bban = new BbanStructure
						{
							Length = 26,
							Structure = "4!a2!n2!n12!n3!n3!a",
							Example = "BOMM0101101030300200000MUR",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 6,
								Structure = "6!c",
								Example = "BOMM01",
							},
							Branch = new BranchStructure
							{
								Position = 6,
								Length = 2,
								Structure = "2!n",
								Example = "01",
							}
						},
						Iban = new IbanStructure
						{
							Length = 30,
							Structure = "MU2!n4!a2!n2!n12!n3!n3!a",
							Example = "MU17BOMM0101101030300200000MUR",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "MU17 BOMM 0101 1010 3030 0200 000M UR",
						LastUpdatedDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Netherlands (The)
					new CountryInfo
					{
						TwoLetterISORegionName = "NL",
						DisplayName = "Netherlands (The)",
						EnglishName = "Netherlands (The)",
						Bban = new BbanStructure
						{
							Length = 14,
							Structure = "4!a10!n",
							Example = "ABNA0417164300",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!a",
								Example = "ABNA",
							},
						},
						Iban = new IbanStructure
						{
							Length = 18,
							Structure = "NL2!n4!a10!n",
							Example = "NL91ABNA0417164300",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "041 71 64 300",
						LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Norway
					new CountryInfo
					{
						TwoLetterISORegionName = "NO",
						DisplayName = "Norway",
						EnglishName = "Norway",
						Bban = new BbanStructure
						{
							Length = 11,
							Structure = "4!n6!n1!n",
							Example = "86011117947",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!n",
								Example = "8601",
							},
						},
						Iban = new IbanStructure
						{
							Length = 15,
							Structure = "NO2!n4!n6!n1!n",
							Example = "NO9386011117947",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "8601 11 17947",
						LastUpdatedDate = new DateTimeOffset(2009, 8, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Pakistan
					new CountryInfo
					{
						TwoLetterISORegionName = "PK",
						DisplayName = "Pakistan",
						EnglishName = "Pakistan",
						Bban = new BbanStructure
						{
							Length = 20,
							Structure = "4!a16!c",
							Example = "SCBL0000001123456702",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!a",
								Example = "SCBL",
							},
						},
						Iban = new IbanStructure
						{
							Length = 24,
							Structure = "PK2!n4!a16!c",
							Example = "PK36SCBL0000001123456702",
							EffectiveDate = new DateTimeOffset(2012, 12, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "00260101036360",
						LastUpdatedDate = new DateTimeOffset(2012, 12, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Poland
					new CountryInfo
					{
						TwoLetterISORegionName = "PL",
						DisplayName = "Poland",
						EnglishName = "Poland",
						Bban = new BbanStructure
						{
							Length = 24,
							Structure = "8!n16!n",
							Example = "109010140000071219812874",
							Branch = new BranchStructure
							{
								Position = 0,
								Length = 8,
								Structure = "8!n",
								Example = "10901014",
							}
						},
						Iban = new IbanStructure
						{
							Length = 28,
							Structure = "PL2!n8!n16!n",
							Example = "PL61109010140000071219812874",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "61 1090 1014 0000 0712 1981 2874",
						LastUpdatedDate = new DateTimeOffset(2016, 10, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Palestine, State of
					new CountryInfo
					{
						TwoLetterISORegionName = "PS",
						DisplayName = "Palestine, State of",
						EnglishName = "Palestine, State of",
						Bban = new BbanStructure
						{
							Length = 25,
							Structure = "4!a21!c",
							Example = "PALS000000000400123456702",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!a",
								Example = "PALS",
							},
						},
						Iban = new IbanStructure
						{
							Length = 29,
							Structure = "PS2!n4!a21!c",
							Example = "PS92PALS000000000400123456702",
							EffectiveDate = new DateTimeOffset(2012, 7, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "400123456702",
						LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Portugal
					new CountryInfo
					{
						TwoLetterISORegionName = "PT",
						DisplayName = "Portugal",
						EnglishName = "Portugal",
						Bban = new BbanStructure
						{
							Length = 21,
							Structure = "4!n4!n11!n2!n",
							Example = "000201231234567890154",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!n",
								Example = "0002",
							},
						},
						Iban = new IbanStructure
						{
							Length = 25,
							Structure = "PT2!n4!n4!n11!n2!n",
							Example = "PT50000201231234567890154",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
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
					},

					// Qatar
					new CountryInfo
					{
						TwoLetterISORegionName = "QA",
						DisplayName = "Qatar",
						EnglishName = "Qatar",
						Bban = new BbanStructure
						{
							Length = 25,
							Structure = "4!a21!c",
							Example = "DOHB00001234567890ABCDEFG",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!a",
								Example = "DOHB",
							},
						},
						Iban = new IbanStructure
						{
							Length = 29,
							Structure = "QA2!n4!a21!c",
							Example = "QA58DOHB00001234567890ABCDEFG",
							EffectiveDate = new DateTimeOffset(2014, 1, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "00001234567890ABCDEFG",
						LastUpdatedDate = new DateTimeOffset(2014, 1, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Romania
					new CountryInfo
					{
						TwoLetterISORegionName = "RO",
						DisplayName = "Romania",
						EnglishName = "Romania",
						Bban = new BbanStructure
						{
							Length = 20,
							Structure = "4!a16!c",
							Example = "AAAA1B31007593840000",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!a",
								Example = "AAAA",
							},
						},
						Iban = new IbanStructure
						{
							Length = 24,
							Structure = "RO2!n4!a16!c",
							Example = "RO49AAAA1B31007593840000",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "RO49 AAAA 1B31 0075 9384 0000",
						LastUpdatedDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Serbia
					new CountryInfo
					{
						TwoLetterISORegionName = "RS",
						DisplayName = "Serbia",
						EnglishName = "Serbia",
						Bban = new BbanStructure
						{
							Length = 18,
							Structure = "3!n13!n2!n",
							Example = "260005601001611379",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 3,
								Structure = "3!n",
								Example = "260",
							},
						},
						Iban = new IbanStructure
						{
							Length = 22,
							Structure = "RS2!n3!n13!n2!n",
							Example = "RS35260005601001611379",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "260-0056010016113-79",
						LastUpdatedDate = new DateTimeOffset(2017, 3, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Saudi Arabia
					new CountryInfo
					{
						TwoLetterISORegionName = "SA",
						DisplayName = "Saudi Arabia",
						EnglishName = "Saudi Arabia",
						Bban = new BbanStructure
						{
							Length = 20,
							Structure = "2!n18!c",
							Example = "80000000608010167519",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 2,
								Structure = "2!n",
								Example = "80",
							},
						},
						Iban = new IbanStructure
						{
							Length = 24,
							Structure = "SA2!n2!n18!c",
							Example = "SA0380000000608010167519",
							EffectiveDate = new DateTimeOffset(2016, 7, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "608010167519",
						LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Seychelles
					new CountryInfo
					{
						TwoLetterISORegionName = "SC",
						DisplayName = "Seychelles",
						EnglishName = "Seychelles",
						Bban = new BbanStructure
						{
							Length = 27,
							Structure = "4!a2!n2!n16!n3!a",
							Example = "SSCB11010000000000001497USD",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 6,
								Structure = "4!a2!n",
								Example = "SSCB11",
							},
							Branch = new BranchStructure
							{
								Position = 6,
								Length = 2,
								Structure = "2!n",
								Example = "01",
							}
						},
						Iban = new IbanStructure
						{
							Length = 31,
							Structure = "SC2!n4!a2!n2!n16!n3!a",
							Example = "SC18SSCB11010000000000001497USD",
							EffectiveDate = new DateTimeOffset(2016, 10, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "0000000000001497",
						LastUpdatedDate = new DateTimeOffset(2019, 10, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Sweden
					new CountryInfo
					{
						TwoLetterISORegionName = "SE",
						DisplayName = "Sweden",
						EnglishName = "Sweden",
						Bban = new BbanStructure
						{
							Length = 20,
							Structure = "3!n16!n1!n",
							Example = "50000000058398257466",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 3,
								Structure = "3!n",
								Example = "123",
							},
						},
						Iban = new IbanStructure
						{
							Length = 24,
							Structure = "SE2!n3!n16!n1!n",
							Example = "SE4550000000058398257466",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "1234 12 3456 1",
						LastUpdatedDate = new DateTimeOffset(2009, 8, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Slovenia
					new CountryInfo
					{
						TwoLetterISORegionName = "SI",
						DisplayName = "Slovenia",
						EnglishName = "Slovenia",
						Bban = new BbanStructure
						{
							Length = 15,
							Structure = "5!n8!n2!n",
							Example = "263300012039086",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 5,
								Structure = "5!n",
								Example = "26330",
							},
						},
						Iban = new IbanStructure
						{
							Length = 19,
							Structure = "SI2!n5!n8!n2!n",
							Example = "SI56263300012039086",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "2633 0001 2039 086",
						LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Slovakia
					new CountryInfo
					{
						TwoLetterISORegionName = "SK",
						DisplayName = "Slovakia",
						EnglishName = "Slovakia",
						Bban = new BbanStructure
						{
							Length = 20,
							Structure = "4!n6!n10!n",
							Example = "12000000198742637541",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!n",
								Example = "1200",
							},
						},
						Iban = new IbanStructure
						{
							Length = 24,
							Structure = "SK2!n4!n6!n10!n",
							Example = "SK3112000000198742637541",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "19-8742637541/1200",
						LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// San Marino
					new CountryInfo
					{
						TwoLetterISORegionName = "SM",
						DisplayName = "San Marino",
						EnglishName = "San Marino",
						Bban = new BbanStructure
						{
							Length = 23,
							Structure = "1!a5!n5!n12!c",
							Example = "U0322509800000000270100",
							Bank = new BankStructure
							{
								Position = 1,
								Length = 5,
								Structure = "5!n",
								Example = "03225",
							},
							Branch = new BranchStructure
							{
								Position = 6,
								Length = 5,
								Structure = "5!n",
								Example = "09800",
							}
						},
						Iban = new IbanStructure
						{
							Length = 27,
							Structure = "SM2!n1!a5!n5!n12!c",
							Example = "SM86U0322509800000000270100",
							EffectiveDate = new DateTimeOffset(2007, 8, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "",
						LastUpdatedDate = new DateTimeOffset(2016, 8, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Sao Tome and Principe
					new CountryInfo
					{
						TwoLetterISORegionName = "ST",
						DisplayName = "Sao Tome and Principe",
						EnglishName = "Sao Tome and Principe",
						Bban = new BbanStructure
						{
							Length = 21,
							Structure = "8!n11!n2!n",
							Example = "000100010051845310146",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!n",
								Example = "0001",
							},
							Branch = new BranchStructure
							{
								Position = 4,
								Length = 4,
								Structure = "4!n",
								Example = "0001",
							}
						},
						Iban = new IbanStructure
						{
							Length = 25,
							Structure = "ST2!n8!n11!n2!n",
							Example = "ST23000100010051845310146",
							EffectiveDate = new DateTimeOffset(2020, 3, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "0051845310146",
						LastUpdatedDate = new DateTimeOffset(2020, 5, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// El Salvador
					new CountryInfo
					{
						TwoLetterISORegionName = "SV",
						DisplayName = "El Salvador",
						EnglishName = "El Salvador",
						Bban = new BbanStructure
						{
							Length = 24,
							Structure = "4!a20!n",
							Example = "CENR00000000000000700025",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!a",
								Example = "CENR",
							},
						},
						Iban = new IbanStructure
						{
							Length = 28,
							Structure = "SV2!n4!a20!n",
							Example = "SV62CENR00000000000000700025",
							EffectiveDate = new DateTimeOffset(2016, 12, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "00000000000000700025",
						LastUpdatedDate = new DateTimeOffset(2019, 4, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Timor-Leste
					new CountryInfo
					{
						TwoLetterISORegionName = "TL",
						DisplayName = "Timor-Leste",
						EnglishName = "Timor-Leste",
						Bban = new BbanStructure
						{
							Length = 19,
							Structure = "3!n14!n2!n",
							Example = "0080012345678910157",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 3,
								Structure = "3!n",
								Example = "008",
							},
						},
						Iban = new IbanStructure
						{
							Length = 23,
							Structure = "TL2!n3!n14!n2!n",
							Example = "TL380080012345678910157",
							EffectiveDate = new DateTimeOffset(2014, 9, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "008 00123456789101 57",
						LastUpdatedDate = new DateTimeOffset(2014, 11, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Tunisia
					new CountryInfo
					{
						TwoLetterISORegionName = "TN",
						DisplayName = "Tunisia",
						EnglishName = "Tunisia",
						Bban = new BbanStructure
						{
							Length = 20,
							Structure = "2!n3!n13!n2!n",
							Example = "10006035183598478831",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 2,
								Structure = "2!n",
								Example = "10",
							},
							Branch = new BranchStructure
							{
								Position = 2,
								Length = 3,
								Structure = "3!n",
								Example = "006",
							}
						},
						Iban = new IbanStructure
						{
							Length = 24,
							Structure = "TN2!n2!n3!n13!n2!n",
							Example = "TN5910006035183598478831",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "10 006 0351835984788 31",
						LastUpdatedDate = new DateTimeOffset(2016, 5, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Turkey
					new CountryInfo
					{
						TwoLetterISORegionName = "TR",
						DisplayName = "Turkey",
						EnglishName = "Turkey",
						Bban = new BbanStructure
						{
							Length = 22,
							Structure = "5!n1!n16!c",
							Example = "0006100519786457841326",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 5,
								Structure = "5!n",
								Example = "00061",
							},
						},
						Iban = new IbanStructure
						{
							Length = 26,
							Structure = "TR2!n5!n1!n16!c",
							Example = "TR330006100519786457841326",
							EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "",
						LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Ukraine
					new CountryInfo
					{
						TwoLetterISORegionName = "UA",
						DisplayName = "Ukraine",
						EnglishName = "Ukraine",
						Bban = new BbanStructure
						{
							Length = 25,
							Structure = "6!n19!c",
							Example = "3223130000026007233566001",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 6,
								Structure = "6!n",
								Example = "322313",
							},
						},
						Iban = new IbanStructure
						{
							Length = 29,
							Structure = "UA2!n6!n19!c",
							Example = "UA213223130000026007233566001",
							EffectiveDate = new DateTimeOffset(2016, 2, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "26007233566001",
						LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Vatican City State
					new CountryInfo
					{
						TwoLetterISORegionName = "VA",
						DisplayName = "Vatican City State",
						EnglishName = "Vatican City State",
						Bban = new BbanStructure
						{
							Length = 18,
							Structure = "3!n15!n",
							Example = "001123000012345678",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 3,
								Structure = "3!n",
								Example = "001",
							},
						},
						Iban = new IbanStructure
						{
							Length = 22,
							Structure = "VA2!n3!n15!n",
							Example = "VA59001123000012345678",
							EffectiveDate = new DateTimeOffset(2019, 3, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = true,
						},
						DomesticAccountNumberExample = "123000012345678",
						LastUpdatedDate = new DateTimeOffset(2018, 12, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Virgin Islands
					new CountryInfo
					{
						TwoLetterISORegionName = "VG",
						DisplayName = "Virgin Islands",
						EnglishName = "Virgin Islands",
						Bban = new BbanStructure
						{
							Length = 20,
							Structure = "4!a16!n",
							Example = "VPVG0000012345678901",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 4,
								Structure = "4!a",
								Example = "VPVG",
							},
						},
						Iban = new IbanStructure
						{
							Length = 24,
							Structure = "VG2!n4!a16!n",
							Example = "VG96VPVG0000012345678901",
							EffectiveDate = new DateTimeOffset(2012, 4, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "00000 12 345 678 901",
						LastUpdatedDate = new DateTimeOffset(2014, 6, 1, 0, 0, 0, TimeSpan.Zero)
					},

					// Kosovo
					new CountryInfo
					{
						TwoLetterISORegionName = "XK",
						DisplayName = "Kosovo",
						EnglishName = "Kosovo",
						Bban = new BbanStructure
						{
							Length = 16,
							Structure = "4!n10!n2!n",
							Example = "1212012345678906",
							Bank = new BankStructure
							{
								Position = 0,
								Length = 2,
								Structure = "2!n",
								Example = "12",
							},
							Branch = new BranchStructure
							{
								Position = 2,
								Length = 2,
								Structure = "2!n",
								Example = "12",
							}
						},
						Iban = new IbanStructure
						{
							Length = 20,
							Structure = "XK2!n4!n10!n2!n",
							Example = "XK051212012345678906",
							EffectiveDate = new DateTimeOffset(2014, 9, 1, 0, 0, 0, TimeSpan.Zero)
						},
						Sepa = new SepaInfo
						{
							IsMember = false,
						},
						DomesticAccountNumberExample = "1212 0123456789 06",
						LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
					},

				// ReSharper restore StringLiteralTypo
				// ReSharper restore CommentTypo
			});
		}
	}
}


