using System.Globalization;
using IbanNet.Registry.Patterns;

namespace IbanNet.CodeGen.Swift;

public class SwiftPatternTokenizerTests
{
    private readonly SwiftPatternTokenizer _sut;

    public SwiftPatternTokenizerTests()
    {
        _sut = new SwiftPatternTokenizer();
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
    [InlineData("2!n", "12", true, null)]
    [InlineData("3!n", "1234", false, 3)]
    [InlineData("2!n", "1A", false, 1)]
    [InlineData("2n", "", false, 0)]
    [InlineData("2n", "1", true, null)]
    [InlineData("2n", "12", true, null)]
    [InlineData("2n", "123", false, 2)]
    [InlineData("8n6a", "AB", false, 0)]
    [InlineData("1!a", "A", true, null)]
    [InlineData("1!a2!a1!a1!n2!n", "ABCD123", true, null)]
    [InlineData("1!a1!n", "A1", true, null)]
    [InlineData("3!c", "d1F", true, null)]
    [InlineData("2!n", "@#", false, 0)]
    [InlineData("2!n3!a2!c", "12ABCe1", true, null)]
    [InlineData("2n3a2c", "12ABCe1", true, null)]
    [InlineData("2n3!a2c", "12123e1", false, 2)]
    [InlineData("2n3a2c", "12123e1", false, 2)]
    [InlineData("2n3a3!c", "", false, 0)]
    [InlineData("2n3a", "12ABCD", false, 5)]
    public void Given_valid_pattern_without_countryCode_it_should_decompose_into_tests(string pattern, string value, bool expectedResult, int? expectedErrorPos)
    {
        var fakePattern = new PatternWrapper(pattern, _sut);

        // Act
        var validator = new PatternValidator(fakePattern.Tokens, fakePattern.IsFixedLength);
        bool isValid = validator.TryValidate(value, out int? errorPos);

        // Assert
        isValid.Should().Be(expectedResult);
        errorPos.Should().Be(expectedErrorPos);
    }

    [Theory]
    [InlineData("a", "a", 0)]
    [InlineData("A1", "A", 0)]
    [InlineData("2z", "2z", 0)]
    [InlineData("2!n2z", "2z", 1)]
    public void Given_invalid_pattern_when_tokenizing_it_should_throw(string pattern, string token, int pos)
    {
        // Act
        Action act = () => _sut.Tokenize(pattern);

        // Assert
        act.Should()
            .Throw<PatternException>()
            .WithMessage(string.Format(CultureInfo.CurrentCulture, $"The pattern token '{token}' is invalid at position {pos}.", token, pos) + "*")
            .Which.InnerException.Should()
            .BeNull();
    }
}
