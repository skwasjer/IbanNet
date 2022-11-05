using IbanNet.Registry.Patterns;

namespace IbanNet.Registry.Wikipedia;

public class WikipediaPatternTests
{
    [Fact]
    public void When_creating_with_null_pattern_it_should_throw()
    {
        string pattern = null;

        // Act
        // ReSharper disable once AssignNullToNotNullAttribute
        Func<WikipediaPattern> act = () => new WikipediaPattern(pattern);

        // Assert
        act.Should()
            .ThrowExactly<ArgumentNullException>()
            .Which.ParamName.Should()
            .Be(nameof(pattern));
    }

    [Theory]
    [InlineData("6a,4n,16c", "6a,4n,16c")]
    [InlineData("16c4n6a", "16c,4n,6a")]
    public void Given_pattern_when_calling_to_string_should_return_expected_pattern(string pattern, string expectedPattern)
    {
        // Act
        var actual = new WikipediaPattern(pattern);

        // Assert
        actual.ToString().Should().Be(expectedPattern);
    }

    [Theory]
    [MemberData(nameof(GetTestCases))]
    public void Given_pattern_when_getting_tokens_it_should_return_expected(string pattern, IEnumerable<PatternToken> expectedTokens)
    {
        // Act
        var actual = new WikipediaPattern(pattern);

        // Assert
        actual.Tokens.Should().BeEquivalentTo(expectedTokens);
    }

    [Theory]
    [InlineData("6a,4n,16c", true)]
    [InlineData("4n", true)]
    public void Given_pattern_when_getting_isFixedLength_it_should_return_expected(string pattern, bool expectedIsFixedLength)
    {
        // Act
        var actual = new WikipediaPattern(pattern);

        // Assert
        actual.IsFixedLength.Should().Be(expectedIsFixedLength);
    }

    public static IEnumerable<object[]> GetTestCases()
    {
        yield return new object[]
        {
            "6a,4n,16c",
            new List<PatternToken>
            {
                new(AsciiCategory.UppercaseLetter, 6),
                new(AsciiCategory.Digit, 4),
                new(AsciiCategory.AlphaNumeric, 16)
            }
        };

        yield return new object[]
        {
            "1n2a3c",
            new List<PatternToken>
            {
                new(AsciiCategory.Digit, 1),
                new(AsciiCategory.UppercaseLetter, 2),
                new(AsciiCategory.AlphaNumeric, 3)
            }
        };
    }
}