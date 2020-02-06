using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace IbanNet.Registry
{
	public class IbanRegistryListProviderTests
	{
		[Test]
		public void Given_list_of_countries_when_creating_it_should_wrap_as_provider()
		{
			var sut = new IbanRegistryListProvider(new List<IbanCountry>
			{
				new IbanCountry("AA"),
				new IbanCountry("BB")
			});

			// ReSharper disable once UseCollectionCountProperty
			sut.Count.Should().Be(sut.Count());
			sut.Should()
				.NotBeEmpty()
				.And.Subject.Select(c => c.TwoLetterISORegionName)
				.Should()
				.BeEquivalentTo("AA", "BB");
		}
	}
}
