using IbanNet.Registry.Patterns;

namespace IbanNet.Registry.Swift;

public class SwiftPatternTests
{
    [Fact]
    public void When_creating_with_null_pattern_it_should_throw()
    {
        string? pattern = null;

        // Act
        Func<SwiftPattern> act = () => new SwiftPattern(pattern!);

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(pattern));
    }

    [Theory]
    [MemberData(nameof(GetTestCases))]
    public void Given_pattern_when_calling_to_string_should_return_same_pattern(string pattern, IEnumerable<PatternToken> _)
    {
        // Act
        var actual = new SwiftPattern(pattern);

        // Assert
        actual.ToString().Should().Be(pattern);
    }

    [Theory]
    [MemberData(nameof(GetTestCases))]
    public void Given_pattern_tokens_when_calling_to_string_should_return_same_pattern(string expectedPattern, IEnumerable<PatternToken> tokens)
    {
        // Act
        var actual = new SwiftPattern(tokens);

        // Assert
        actual.ToString().Should().Be(expectedPattern);
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
    [InlineData("2!n4!a4!n2!n8!c", true)]
    [InlineData("2!n4!a4!n2n8!c", false)]
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
            "2!n4!a4!n2!n8!c",
            new List<PatternToken>
            {
                new(AsciiCategory.Digit, 2),
                new(AsciiCategory.UppercaseLetter, 4),
                new(AsciiCategory.Digit, 4),
                new(AsciiCategory.Digit, 2),
                new(AsciiCategory.AlphaNumeric, 8)
            }
        };

        yield return new object[]
        {
            "4!n10a1!e2!c",
            new List<PatternToken>
            {
                new(AsciiCategory.Digit, 4),
                new(AsciiCategory.UppercaseLetter, 1, 10),
                new(AsciiCategory.Space, 1),
                new(AsciiCategory.AlphaNumeric, 2)
            }
        };

        yield return new object[]
        {
            "AD2!n4!n4!n12!c",
            new List<PatternToken>
            {
                new("AD"),
                new(AsciiCategory.Digit, 2, 2),
                new(AsciiCategory.Digit, 4, 4),
                new(AsciiCategory.Digit, 4, 4),
                new(AsciiCategory.AlphaNumeric, 12, 12)
            }
        };
    }

    [Theory]
    [InlineData("AD2!n4!n4!n12!c", "^AD\\d{10}[a-zA-Z0-9]{12}$")]
    [InlineData("NL2!n4!a10!n", "^NL\\d{2}[A-Z]{4}\\d{10}$")]
    [InlineData("NO2!n4!n6!n1!n", "^NO\\d{13}$")]
    [InlineData("PT2!n4!n4!n11!n2!n", "^PT\\d{23}$")]
    [InlineData("SM2!n1!a5!n5!n12!c", "^SM\\d{2}[A-Z]\\d{10}[a-zA-Z0-9]{12}$")]
    public void When_getting_regexPattern_it_should_return_expected(string ibanPattern, string regexPattern)
    {
        var sut = new SwiftPattern(ibanPattern);

        sut.ToRegexPattern().Should().Be(regexPattern);
    }
}
