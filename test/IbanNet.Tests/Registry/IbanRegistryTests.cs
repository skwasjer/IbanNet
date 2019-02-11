using System;
using System.Collections;
using FluentAssertions;
using NUnit.Framework;

namespace IbanNet.Registry
{
    [TestFixture]
    internal class IbanRegistryTests
    {
		private IbanRegistry _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new IbanRegistry();
		}

        [Test]
        public void When_definitions_are_loaded_should_contain_exactly_n_items()
		{
			_sut.Count.Should().Be(76);
		}

		private static IEnumerable GetExpectedDefinitions()
		{
			yield return new TestCaseData(
				new CountryInfo("AD")
				{
					DisplayName = "Andorra",
					EnglishName = "Andorra",
					Bban = new BbanStructure
					{
						Length = 20,
						Structure = "4!n4!n12!c",
						Example = "00012030200359100100",
						Bank = new StructureSection
						{
							Position = 0,
							Length = 4,
							Structure = "4!n",
							Example = "0001",
						},
						Branch = new StructureSection
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
					IsSepaCountry = false,
					DomesticAccountNumberExample = "2030200359100100",
					LastUpdatedDate = new DateTimeOffset(2009, 11, 1, 0, 0, 0, TimeSpan.Zero)
				}
			)
				.SetDescription("The first item in the definition list.")
				.SetName("Should_contain_definition_for_AD")
			;

			yield return new TestCaseData(
				new CountryInfo("XK")
				{
					DisplayName = "Kosovo",
					EnglishName = "Kosovo",
					Bban = new BbanStructure
					{
						Length = 16,
						Structure = "4!n10!n2!n",
						Example = "1212012345678906",
						Bank = new StructureSection
						{
							Position = 0,
							Length = 2,
							Structure = "2!n",
							Example = "12",
						},
						Branch = new StructureSection
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
					IsSepaCountry = false,
					DomesticAccountNumberExample = "1212 0123456789 06",
					LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero)
				}
			)
				.SetDescription("The last item in the definition list.")
				.SetName("Should_contain_definition_for_XK")
			;
		}

		[TestCaseSource(nameof(GetExpectedDefinitions))]
		public void When_definitions_are_loaded_should_contain(CountryInfo expectedCountryInfo)
		{
			_sut.Should()
				.Contain(c => c.TwoLetterISORegionName == expectedCountryInfo.TwoLetterISORegionName)
				.Which.Should().BeEquivalentTo(expectedCountryInfo);
		}
	}
}
