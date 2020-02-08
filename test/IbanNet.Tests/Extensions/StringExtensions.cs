using FluentAssertions;
using Xunit;

namespace IbanNet.Extensions
{
	public class StringExtensions
	{
		[Theory]
		[InlineData("no-whitespace", "no-whitespace")]
		[InlineData(" \tin-\nstr ing\r", "in-string")]
		[InlineData("", "")]
		[InlineData(null, null)]
		public void Given_string_when_stripping_whitespace_it_should_return_expected_value(string input, string expected)
		{
			// Act
			string actual = input.StripWhitespaceOrNull();

			// Assert
			actual.Should().Be(expected);
		}
	}
}
