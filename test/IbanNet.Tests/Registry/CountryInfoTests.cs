using System;
using FluentAssertions;
using NUnit.Framework;

namespace IbanNet.Registry
{
	public class CountryInfoTests
	{
		[TestCase("")]
		[TestCase("N")]
		[TestCase("NLD")]
		public void When_country_code_is_of_invalid_length_should_throw(string countryCode)
		{
			// Act
			Action act = () => new CountryInfo(countryCode);

			// Assert
			act.Should().Throw<ArgumentOutOfRangeException>()
				.Which.ParamName.Should().Be("name");
		}

		[Test]
		public void When_country_code_is_of_valid_length_should_not_throw()
		{
			// Act
			Action act = () => new CountryInfo("ZA");

			// Assert
			act.Should().NotThrow();
		}

		[Test]
		public void When_country_code_is_provided_in_lowercase_should_make_it_uppercase()
		{
			// Act
			var actual = new CountryInfo("nl");

			// Assert
			actual.TwoLetterISORegionName.Should().Be("NL");
		}
	}
}
