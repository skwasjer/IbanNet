using FluentAssertions;
using NUnit.Framework;

namespace IbanNet.Extensions
{
	[TestFixture]
	public class StringExtensions
	{
		[TestCase("no-whitespace", "no-whitespace")]
		[TestCase(" \tin-\nstr ing\r", "in-string")]
		[TestCase(null, null)]
		public void Given_string_when_stripping_whitespace_it_should_return_expected_value(string input, string expected)
		{
			// Act
			string actual = input.StripWhitespaceOrNull();

			// Assert
			actual.Should().Be(expected);
		}
	}
}
