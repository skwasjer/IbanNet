using System;
using System.Collections.Generic;
using FluentAssertions;
using IbanNet.Registry;
using NUnit.Framework;

namespace IbanNet
{
	[TestFixture]
	public class IbanValidatorOptionsTests
	{
		private IbanValidatorOptions _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new IbanValidatorOptions();
		}

		[Test]
		public void Given_registry_factory_returns_countries_when_getting_registry_it_should_return_dictionary()
		{
			var countries = new[]
			{
				new IbanCountry("NL"),
				new IbanCountry("FR")
			};
			_sut.Registry = () => countries;

			// Act
			IDictionary<string, IbanCountry> registry = _sut.GetRegistry();

			// Assert
			registry.Should().HaveCount(2);
			registry.Should()
				.ContainKey("NL")
				.WhichValue.Should()
				.Be(countries[0]);
			registry.Should()
				.ContainKey("FR")
				.WhichValue.Should()
				.Be(countries[1]);
		}

		[TestCase]
		public void Given_registry_factory_is_null_when_getting_registry_it_should_throw()
		{
			_sut.Registry = null;

			// Act
			Action act = () => _sut.GetRegistry();

			// Assert
			act.Should()
				.Throw<InvalidOperationException>()
				.WithMessage("The provided registry cannot be empty.");
		}

		[TestCase]
		public void Given_registry_factory_returns_null_when_getting_registry_it_should_throw()
		{
			_sut.Registry = () => null;

			// Act
			Action act = () => _sut.GetRegistry();

			// Assert
			act.Should()
				.Throw<InvalidOperationException>()
				.WithMessage("The provided registry cannot be empty.");
		}

		[TestCase]
		public void Given_registry_factory_returns_empty_list_when_getting_registry_it_should_throw()
		{
			_sut.Registry = () => new IbanCountry[0];

			// Act
			Action act = () => _sut.GetRegistry();

			// Assert
			act.Should()
				.Throw<InvalidOperationException>()
				.WithMessage("The provided registry cannot be empty.");
		}
	}
}
