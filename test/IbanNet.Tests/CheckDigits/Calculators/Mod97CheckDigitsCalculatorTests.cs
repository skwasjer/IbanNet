using System;
using FluentAssertions;
using NUnit.Framework;

namespace IbanNet.CheckDigits.Calculators
{
	[TestFixture]
	public class Mod97CheckDigitsCalculatorTests
	{
		private readonly Mod97CheckDigitsCalculator _sut;

		public Mod97CheckDigitsCalculatorTests()
		{
			_sut = new Mod97CheckDigitsCalculator();
		}

		[TestCase("BMAG00001299123456BH67", 1)]
		[TestCase("ABC012", 85)]
		[TestCase("", 0)]
		public void Given_value_when_computing_should_return_expected_check_digits(string value, int expectedCheckDigits)
		{
			// Act
			int actual = _sut.Compute(value.ToCharArray());

			// Assert
			actual.Should().Be(expectedCheckDigits);
		}

		[Test]
		public void Given_value_contains_invalid_character_when_computing_it_should_throw()
		{
			// Act
			Action act = () => _sut.Compute("A0@1".ToCharArray());

			// Assert
			act.Should()
				.Throw<InvalidTokenException>()
				.Which.Message.Should()
				.Be($"Expected alphanumeric character at position 2, but found '@'.");
		}

		[Test]
		public void Given_null_value_when_computing_it_should_throw()
		{
			char[] value = null;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			Action act = () => _sut.Compute(value);

			// Assert
			act.Should()
				.Throw<ArgumentNullException>()
				.Which.ParamName.Should()
				.Be(nameof(value));
		}
	}
}
