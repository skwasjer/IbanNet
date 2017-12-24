using System.Collections;
using FluentAssertions;
using NUnit.Framework;

namespace IbanNet.Tests
{
    [TestFixture]
    internal class IbanDefinitionsTests
    {
		private IbanDefinitions _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new IbanDefinitions();
		}

        [Test]
        public void When_definitions_are_loaded_should_contain_exactly_n_items()
		{
			_sut.Count.Should().Be(72);
		}

		private static IEnumerable GetExpectedDefinitions()
		{
			yield return new TestCaseData(
				new IbanDefinition
				{
					CountryCode = "AD",
					Length = 24,
					Structure = "F04F04A12",
					Example = "AD1200012030200359100100"
				}
			)
				.SetDescription("The first item in the definition list.")
				.SetName("Should_contain_definition_for_AD")
			;

			yield return new TestCaseData(
				new IbanDefinition
				{
					CountryCode = "XK",
					Length = 20,
					Structure = "F04F10F02",
					Example = "XK051212012345678906"
				}
			)
				.SetDescription("The last item in the definition list.")
				.SetName("Should_contain_definition_for_XK")
			;
		}

		[TestCaseSource(nameof(GetExpectedDefinitions))]
		public void When_definitions_are_loaded_should_contain(IbanDefinition expectedDefinition)
		{
			_sut.Should().ContainValue(expectedDefinition);
		}
	}
}
