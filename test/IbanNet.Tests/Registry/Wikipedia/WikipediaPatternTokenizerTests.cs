using System.Globalization;
using IbanNet.Registry.Patterns;

namespace IbanNet.Registry.Wikipedia;

public class WikipediaPatternTokenizerTests
{
    private readonly WikipediaPatternTokenizer _sut;

    public WikipediaPatternTokenizerTests()
    {
        _sut = new WikipediaPatternTokenizer();
    }

#if !USE_SPANS
    [Fact]
    public void Given_null_input_when_tokenizing_it_should_throw()
    {
        char[]? input = null;

        // Act
        Action act = () => _sut.Tokenize(input!);

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(input));
    }
#endif

    [Theory]
    [InlineData("2n", "12", true, null)]
    [InlineData("3n", "1234", false, 3)]
    [InlineData("2n", "1A", false, 1)]
    [InlineData("2n", "", false, 0)]
    [InlineData("2n", "1", false, 1)]
    [InlineData("2n", "123", false, 2)]
    [InlineData("8n6a", "AB", false, 2)]
    [InlineData("1a", "A", true, null)]
    [InlineData("1a2a1a1n2n", "ABCD123", true, null)]
    [InlineData("1a1n", "A1", true, null)]
    [InlineData("3c", "d1F", true, null)]
    [InlineData("2n", "@#", false, 0)]
    [InlineData("2n3a2c", "12ABCe1", true, null)]
    [InlineData("2n3a2c", "12123e1", false, 2)]
    [InlineData("2n3a3c", "", false, 0)]
    [InlineData("2n3a", "12ABCD", false, 5)]
    public void Given_valid_pattern_without_countryCode_it_should_decompose_into_tests(string pattern, string value, bool expectedResult, int? expectedErrorPos)
    {
        var fakePattern = new FakePattern(_sut.Tokenize(pattern));

        // Act
        var validator = new PatternValidator(fakePattern.Tokens, fakePattern.IsFixedLength);
        bool isValid = validator.TryValidate(value, out int? errorPos);

        // Assert
        isValid.Should().Be(expectedResult);
        errorPos.Should().Be(expectedErrorPos);
    }

    [Theory]
    [InlineData("A", "A", 0)]
    [InlineData("2z", "2z", 0)]
    [InlineData("2a2!n", "2!n", 1)]
    public void Given_invalid_pattern_when_tokenizing_it_should_throw(string pattern, string token, int pos)
    {
        // Act
        Action act = () => _sut.Tokenize(pattern);

        // Assert
        act.Should()
            .Throw<PatternException>()
            .WithMessage(string.Format(CultureInfo.CurrentCulture, Resources.PatternException_Invalid_token_0_at_position_1, token, pos) + "*");
    }
}
