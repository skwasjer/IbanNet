using System.Collections.Generic;
using FluentAssertions;
using IbanNet.Registry;
using IbanNet.Validation.Results;
using NUnit.Framework;

namespace IbanNet.Validation.Rules
{
	[TestFixture]
	internal class IsValidCountryCodeRuleTests
	{
		private Dictionary<string, IbanCountry> _registryDict;
		private IsValidCountryCodeRule _sut;

		[SetUp]
		public void SetUp()
		{
			_registryDict = new Dictionary<string, IbanCountry>();
			_sut = new IsValidCountryCodeRule(new IbanRegistry(_registryDict));
		}

		[Test]
		public void Given_no_country_code_when_validating_it_should_return_error()
		{
			var context = new ValidationRuleContext(string.Empty);

			// Act
			ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext(string.Empty));

			// Assert
			actual.Should().BeOfType<UnknownCountryCodeResult>();
			context.Country.Should().BeNull();
		}

		[Test]
		public void Given_known_country_code_when_validating_it_should_return_success()
		{
			var context = new ValidationRuleContext("XX");
			var country = new IbanCountry("XX");
			_registryDict.Add(country.TwoLetterISORegionName, country);

			// Act
			ValidationRuleResult actual = _sut.Validate(context);

			// Assert
			actual.Should().Be(ValidationRuleResult.Success);
			context.Country.Should().BeSameAs(country);
		}

		[Test]
		public void Given_unknown_country_code_when_validating_it_should_return_error()
		{
			var context = new ValidationRuleContext("XX");

			// Act
			ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext("XX"));

			// Assert
			actual.Should().BeOfType<UnknownCountryCodeResult>();
			context.Country.Should().BeNull();
		}
	}
}
