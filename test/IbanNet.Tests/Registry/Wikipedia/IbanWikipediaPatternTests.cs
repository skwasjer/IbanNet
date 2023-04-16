namespace IbanNet.Registry.Wikipedia;

public class IbanWikipediaPatternTests
{
    [Theory]
    [InlineData("AB", "6a,4n,16c", "6a,4n,16c")]
    [InlineData("XY", "16c4n6a", "16c,4n,6a")]
    public void Given_pattern_when_calling_to_string_should_return_expected_pattern(string countryCode, string pattern, string expectedPattern)
    {
        // Act
        var actual = new IbanWikipediaPattern(countryCode, pattern);

        // Assert
        actual.ToString().Should().Be(expectedPattern);
    }
}
