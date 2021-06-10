using System;
using System.Collections.Generic;
using FluentAssertions;
using IbanNet.Registry.Patterns;
using Xunit;

namespace IbanNet.Registry.Swift
{
    public class SwiftPatternTests
    {
        [Fact]
        public void When_creating_with_null_pattern_it_should_throw()
        {
            string pattern = null;

            // Act
            // ReSharper disable once AssignNullToNotNullAttribute
            // ReSharper disable once ObjectCreationAsStatement
            Action act = () => new SwiftPattern(pattern);

            // Assert
            act.Should()
                .ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should()
                .Be(nameof(pattern));
        }

        [Fact]
        public void Given_pattern_when_calling_to_string_should_return_same_pattern()
        {
            const string pattern = "BG2!n4!a4!n2!n8!c";

            // Act
            var actual = new SwiftPattern(pattern);

            // Assert
            actual.ToString().Should().Be(pattern);
        }

        [Theory]
        [MemberData(nameof(GetTestCases))]
        public void Given_pattern_when_getting_tokens_it_should_return_expected(string pattern, IEnumerable<PatternToken> expectedTokens)
        {
            // Act
            var actual = new SwiftPattern(pattern);

            // Assert
            actual.Tokens.Should().BeEquivalentTo(expectedTokens);
        }

        [Theory]
        [InlineData("BG2!n4!a4!n2!n8!c", true)]
        [InlineData("BG2!n4!a4!n2n8!c", false)]
        [InlineData("4!n", true)]
        [InlineData("4n", false)]
        public void Given_pattern_when_getting_isFixedLength_it_should_return_expected(string pattern, bool expectedIsFixedLength)
        {
            // Act
            var actual = new SwiftPattern(pattern);

            // Assert
            actual.IsFixedLength.Should().Be(expectedIsFixedLength);
        }

        public static IEnumerable<object[]> GetTestCases()
        {
            yield return new object[]
            {
                "BG2!n4!a4!n2!n8!c",
                new List<PatternToken>
                {
                    new PatternToken(AsciiCategory.Letter, 2),
                    new PatternToken(AsciiCategory.Digit, 2),
                    new PatternToken(AsciiCategory.UppercaseLetter, 4),
                    new PatternToken(AsciiCategory.Digit, 4),
                    new PatternToken(AsciiCategory.Digit, 2),
                    new PatternToken(AsciiCategory.AlphaNumeric, 8)
                }
            };

            yield return new object[]
            {
                "4!n10a1!e2!c",
                new List<PatternToken>
                {
                    new PatternToken(AsciiCategory.Digit, 4),
                    new PatternToken(AsciiCategory.UppercaseLetter, 1, 10),
                    new PatternToken(AsciiCategory.Space, 1),
                    new PatternToken(AsciiCategory.AlphaNumeric, 2)
                }
            };
        }
    }
}
