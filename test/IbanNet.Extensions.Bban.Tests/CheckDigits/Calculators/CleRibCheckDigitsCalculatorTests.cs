using System;
using FluentAssertions;
using NUnit.Framework;

namespace IbanNet.CheckDigits.Calculators
{
	[TestFixture]
	public class CleRibCheckDigitsCalculatorTests
	{
		private CleRibCheckDigitsCalculator _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new CleRibCheckDigitsCalculator();
		}

		[TestCase("12345123451234567891A", 16)]
		[TestCase("12345123451234567891B", 13)]
		[TestCase("2004 1010 0505 0001 3M02 6", 06)]
		[TestCase("300060000112345678901", 89)]
		public void Given_account_number_when_computing_check_digit_should_match_expected(string accountNumber, int expectedCheckDigits)
		{
			// Act
			int actual = _sut.Compute(accountNumber);

			// Assert
			actual.Should().Be(expectedCheckDigits);
		}

		[TestCase("02345123451234567891A", 16)]
		[TestCase("02345123451234567891B", 13)]
		[TestCase("1004 1010 0505 0001 3M02 6", 06)]
		[TestCase("200060000112345678901", 89)]
		public void Given_invalid_account_number_when_computing_check_digit_should_not_match_expected(string accountNumber, int assumedCheckDigits)
		{
			// Act
			int actual = _sut.Compute(accountNumber);

			// Assert
			actual.Should().NotBe(assumedCheckDigits);
		}

		[Test]
		public void Given_account_number_contains_whitespace_when_computing_should_ignore_whitespace()
		{
			const int expectedCheckDigits = 06;
			const string accountNumberWithWhitespace = "\t2004 1010\t0505 0001 3M02 6 \t";
			const string accountNumberWithoutWhitespace = "20041010050500013M026";

			// Act
			int cd1 = _sut.Compute(accountNumberWithWhitespace);
			int cd2 = _sut.Compute(accountNumberWithoutWhitespace);

			// Assert
			cd1.Should().Be(cd2).And.Be(expectedCheckDigits);
		}

		[TestCase("ShortAnd20CharsLong.")]
		[TestCase("TooShort")]
		public void Given_account_number_contains_insufficient_chars_when_computing_should_throw(string input)
		{
			Action act = () => _sut.Compute(input);

			// Assert
			act.Should()
				.Throw<ArgumentException>()
				.WithMessage($"The input '{input}' can not be validated using clé RIB.*")
				.Which.ParamName.Should()
				.Be(nameof(input));
		}
	}
}
