using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using IbanNet.Registry;
using NUnit.Framework;

namespace IbanNet
{
	[TestFixture]
	internal class IbanValidatorTests
	{
		public class Given_invalid_options : IbanValidatorTests
		{
			[TestCaseSource(nameof(CtorWithOptionsTestCases))]
			public void When_creating_instance_it_should_throw(IbanValidatorOptions options, Type expectedExceptionType)
			{
				// Act
				// ReSharper disable once ObjectCreationAsStatement
				Action act = () => new IbanValidator(options);

				// Assert
				act.Should()
					.Throw<ArgumentException>()
					.Which.Should()
					.BeOfType(expectedExceptionType);
			}


			public static IEnumerable CtorWithOptionsTestCases()
			{
				yield return new TestCaseData(null, typeof(ArgumentNullException));
				yield return new TestCaseData(new IbanValidatorOptions { Registry = null }, typeof(ArgumentException));
				yield return new TestCaseData(new IbanValidatorOptions { ValidationMethod = null }, typeof(ArgumentException));
			}
		}

		public class Given_default_supported_countries : IbanValidatorTests
		{
			[Test]
			public void When_getting_it_should_match_default_registry()
			{
				// Act
				IEnumerable<CountryInfo> actual = new IbanValidator().SupportedCountries;

				// Assert
				actual.Should().BeEquivalentTo(new IbanRegistry());
			}

			[Test]
			public void When_casting_readonly_countries_to_dictionary_it_should_not_be_able_to_add()
			{
				var sut = new IbanValidator();
				var countries = (IDictionary<string, CountryInfo>)((ICountryValidationSupport)sut).SupportedCountries;

				// Act
				Action act = () => countries.Add("key", new CountryInfo());

				// Assert
				act.Should()
					.Throw<NotSupportedException>()
					.WithMessage("Collection is read-only.");
			}
		}
	}
}
