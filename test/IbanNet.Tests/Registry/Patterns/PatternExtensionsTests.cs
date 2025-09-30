namespace IbanNet.Registry.Patterns;

public abstract class PatternExtensionsTests
{
    public sealed class ToRegexPattern : PatternExtensionsTests
    {

        [Fact]
        public void Given_that_pattern_is_null_when_getting_regexPattern_it_should_throw()
        {
            Pattern? pattern = null;

            // Act
            Func<string> act = () => pattern!.ToRegexPattern();

            // Assert
            act.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithParameterName(nameof(pattern));
        }

        [Theory]
        [MemberData(nameof(GetTestCases))]
        public void When_getting_regexPattern_it_should_return_expected(IEnumerable<PatternToken> pattern, string expectedRegex)
        {
            // Act
            var sut = new TestPattern(pattern);
            string actual = sut.ToRegexPattern();

            // Assert
            actual.Should().Be(expectedRegex);
        }

        public static IEnumerable<object[]> GetTestCases()
        {
            // Specific chars.
            yield return
            [
                new PatternToken[]
                {
                    new("A"),
                    new("B"),
                    new("C")
                },
                "^ABC$"
            ];


            // Digit fixed length.
            yield return
            [
                new PatternToken[]
                {
                    new(AsciiCategory.Digit, 4),
                },
                "^\\d{4}$"
            ];

            // Digit variable length.
            yield return
            [
                new PatternToken[]
                {
                    new(AsciiCategory.Digit, 4, 7),
                },
                "^\\d{4,7}$"
            ];

            // Single digit.
            yield return
            [
                new PatternToken[]
                {
                    new(AsciiCategory.Digit, 1),
                },
                "^\\d$"
            ];


            // Uppercase fixed length.
            yield return
            [
                new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 4),
                },
                "^[A-Z]{4}$"
            ];

            // Uppercase variable length.
            yield return
            [
                new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 4, 7),
                },
                "^[A-Z]{4,7}$"
            ];

            // Single uppercase.
            yield return
            [
                new PatternToken[]
                {
                    new(AsciiCategory.UppercaseLetter, 1),
                },
                "^[A-Z]$"
            ];


            // Lowercase fixed length.
            yield return
            [
                new PatternToken[]
                {
                    new(AsciiCategory.LowercaseLetter, 4),
                },
                "^[a-z]{4}$"
            ];

            // Lowercase variable length.
            yield return
            [
                new PatternToken[]
                {
                    new(AsciiCategory.LowercaseLetter, 4, 7),
                },
                "^[a-z]{4,7}$"
            ];

            // Single lowercase.
            yield return
            [
                new PatternToken[]
                {
                    new(AsciiCategory.LowercaseLetter, 1),
                },
                "^[a-z]$"
            ];


            // Letter fixed length.
            yield return
            [
                new PatternToken[]
                {
                    new(AsciiCategory.Letter, 4),
                },
                "^[a-zA-Z]{4}$"
            ];

            // Letter variable length.
            yield return
            [
                new PatternToken[]
                {
                    new(AsciiCategory.Letter, 4, 7),
                },
                "^[a-zA-Z]{4,7}$"
            ];

            // Letter alphanumeric.
            yield return
            [
                new PatternToken[]
                {
                    new(AsciiCategory.Letter, 1),
                },
                "^[a-zA-Z]$"
            ];


            // Alphanumeric fixed length.
            yield return
            [
                new PatternToken[]
                {
                    new(AsciiCategory.AlphaNumeric, 4),
                },
                "^[a-zA-Z0-9]{4}$"
            ];

            // Alphanumeric variable length.
            yield return
            [
                new PatternToken[]
                {
                    new(AsciiCategory.AlphaNumeric, 4, 7),
                },
                "^[a-zA-Z0-9]{4,7}$"
            ];

            // Single alphanumeric.
            yield return
            [
                new PatternToken[]
                {
                    new(AsciiCategory.AlphaNumeric, 1),
                },
                "^[a-zA-Z0-9]$"
            ];


            // Space fixed length.
            yield return
            [
                new PatternToken[]
                {
                    new(AsciiCategory.Space, 4),
                },
                "^ {4}$"
            ];

            // Space variable length.
            yield return
            [
                new PatternToken[]
                {
                    new(AsciiCategory.Space, 4, 7),
                },
                "^ {4,7}$"
            ];

            // Single space.
            yield return
            [
                new PatternToken[]
                {
                    new(AsciiCategory.Space, 1),
                },
                "^ $"
            ];


            // Compress fixed.
            yield return
            [
                new PatternToken[]
                {
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Space, 2, 2),
                    new(AsciiCategory.Space, 2, 2),
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new(AsciiCategory.LowercaseLetter, 2, 2),
                    new(AsciiCategory.LowercaseLetter, 2, 2),
                    new(AsciiCategory.Letter, 2, 2),
                    new(AsciiCategory.Letter, 2, 2),
                    new(AsciiCategory.AlphaNumeric, 2, 2),
                    new(AsciiCategory.AlphaNumeric, 2, 2)
                },
                "^\\d{4} {4}[A-Z]{4}[a-z]{4}[a-zA-Z]{4}[a-zA-Z0-9]{4}$"
            ];

            // Compress non-fixed.
            yield return
            [
                new PatternToken[]
                {
                    new(AsciiCategory.Digit, 1, 2),
                    new(AsciiCategory.Digit, 1, 2),
                    new(AsciiCategory.Space, 1, 2),
                    new(AsciiCategory.Space, 1, 2),
                    new(AsciiCategory.UppercaseLetter, 1, 2),
                    new(AsciiCategory.UppercaseLetter, 1, 3),
                    new("A"),
                    new("BC"),
                    new(AsciiCategory.LowercaseLetter, 1, 2),
                    new(AsciiCategory.LowercaseLetter, 1, 2),
                    new(AsciiCategory.Letter, 1, 2),
                    new(AsciiCategory.Letter, 1, 2),
                    new(AsciiCategory.AlphaNumeric, 1, 2),
                    new(AsciiCategory.AlphaNumeric, 1, 2)
                },
                "^\\d{2,4} {2,4}[A-Z]{2,5}ABC[a-z]{2,4}[a-zA-Z]{2,4}[a-zA-Z0-9]{2,4}$"
            ];

            // Compress mixed.
            yield return
            [
                new PatternToken[]
                {
                    new(AsciiCategory.Digit, 1, 2),
                    new(AsciiCategory.Digit, 2, 2),
                    new(AsciiCategory.Space, 1, 2),
                    new(AsciiCategory.Space, 2, 2),
                    new(AsciiCategory.UppercaseLetter, 1, 2),
                    new(AsciiCategory.UppercaseLetter, 2, 2),
                    new("A"),
                    new("BC"),
                    new(AsciiCategory.LowercaseLetter, 1, 2),
                    new(AsciiCategory.LowercaseLetter, 2, 2),
                    new(AsciiCategory.Letter, 1, 2),
                    new(AsciiCategory.Letter, 2, 2),
                    new(AsciiCategory.AlphaNumeric, 1, 2),
                    new(AsciiCategory.AlphaNumeric, 2, 2)
                },
                "^\\d{3,4} {3,4}[A-Z]{3,4}ABC[a-z]{3,4}[a-zA-Z]{3,4}[a-zA-Z0-9]{3,4}$"
            ];
        }
    }
}
