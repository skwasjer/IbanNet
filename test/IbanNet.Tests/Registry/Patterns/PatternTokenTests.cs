using System.ComponentModel;
using Newtonsoft.Json.Linq;

namespace IbanNet.Registry.Patterns;

public class PatternTokenTests
{
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void Given_min_length_is_less_than_1_when_creating_instance_it_should_throw(int length)
    {
        // Act
        Func<PatternToken> act = () => new PatternToken(AsciiCategory.Digit, length);

        // Assert
        act.Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage(string.Format(Resources.The_value_cannot_be_less_than_or_equal_to_0, 0) + "*")
            .WithParameterName(nameof(length));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void Given_max_length_is_less_than_1_when_creating_instance_it_should_throw(int maxLength)
    {
        // Act
        Func<PatternToken> act = () => new PatternToken(AsciiCategory.Digit, 10, maxLength);

        // Assert
        act.Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage(string.Format(Resources.The_value_cannot_be_less_than_or_equal_to_0, 0) + "*")
            .WithParameterName(nameof(maxLength));
    }

    [Theory]
    [InlineData(10, 8)]
    [InlineData(4, 3)]
    public void Given_max_length_is_less_than_min_length_when_creating_instance_it_should_throw(int minLength, int maxLength)
    {
        // Act
        Func<PatternToken> act = () => new PatternToken(AsciiCategory.Digit, minLength, maxLength);

        // Assert
        act.Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage(string.Format(Resources.The_value_cannot_be_less_than_or_equal_to_0, minLength) + "*")
            .WithParameterName(nameof(maxLength));
    }

    [Fact]
    public void Given_invalid_category_value_when_creating_instance_it_should_throw()
    {
        const AsciiCategory category = (AsciiCategory)int.MaxValue;

        // Act
        Func<PatternToken> act = () => new PatternToken(category, 10);

        // Assert
        act.Should()
#if NETSTD_LEGACY
            .Throw<ArgumentException>()
            .WithMessage(string.Format(Resources.Enum_value_0_should_be_defined_in_the_1_enum, category, nameof(AsciiCategory)) + "*")
#else
            .Throw<InvalidEnumArgumentException>()
#endif
            .WithParameterName(nameof(category));
    }

    [Theory]
    [MemberData(nameof(GetCtorAsciiTestCases))]
    public void When_creating_instance_it_should_set_properties
    (
        AsciiCategory category,
        int minLength,
        int maxLength,
        bool isFixedLength,
        bool isLower,
        bool isUpper,
        bool isDigit
    )
    {
        // Act
        var pattern = new PatternToken(category, minLength, maxLength);

        // Assert
        pattern.Category.Should().Be(category);
        pattern.Value.Should().BeNull();
        pattern.MinLength.Should().Be(minLength);
        pattern.MaxLength.Should().Be(maxLength);
        pattern.IsFixedLength.Should().Be(isFixedLength);
        pattern.IsMatch('a', 0).Should().Be(isLower);
        pattern.IsMatch('A', 0).Should().Be(isUpper);
        pattern.IsMatch('0', 0).Should().Be(isDigit);
    }

    public static IEnumerable<object[]> GetCtorAsciiTestCases()
    {
        yield return new object[] { AsciiCategory.None, 1, 1, true, false, false, false };
        yield return new object[] { AsciiCategory.Space, 1, 1, true, false, false, false };
        yield return new object[] { AsciiCategory.Digit, 1, 2, false, false, false, true };
        yield return new object[] { AsciiCategory.LowercaseLetter, 2, 4, false, true, false, false };
        yield return new object[] { AsciiCategory.UppercaseLetter, 3, 4, false, false, true, false };
        yield return new object[] { AsciiCategory.Letter, 6, 6, true, true, true, false };
        yield return new object[] { AsciiCategory.AlphaNumeric, 3, 3, true, true, true, true };
    }

    [Fact]
    public void Given_that_pattern_value_is_null_when_creating_instance_it_should_throw()
    {
        string? value = null;

        // Act
        Func<PatternToken> act = () => new PatternToken(value!);

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(value));
    }

    [Theory]
    [MemberData(nameof(GetCtorValueTestCases))]
    public void Given_that_pattern_value_is_valid_when_creating_instance_it_should_set_properties
    (
        string value,
        int minLength,
        int maxLength
    )
    {
        // Act
        var pattern = new PatternToken(value);

        // Assert
        pattern.Category.Should().Be(AsciiCategory.None);
        pattern.Value.Should().Be(value);
        pattern.MinLength.Should().Be(minLength);
        pattern.MaxLength.Should().Be(maxLength);
        pattern.IsFixedLength.Should().BeTrue();
    }

    public static IEnumerable<object[]> GetCtorValueTestCases()
    {
        yield return new object[] { "A", 1, 1 };
        yield return new object[] { "AB", 2, 2 };
        yield return new object[] { "ABCDEF", 6, 6 };
    }

    [Theory]
    [MemberData(nameof(GetCtorValueMatchTestCases))]
    public void Given_that_pattern_value_when_matching_it_should_return_expected
    (
        string value,
        char[] match,
        bool shouldMatch
    )
    {
        // Act
        var pattern = new PatternToken(value);

        // Assert
        pattern.Value.Should().Be(value);
        bool isMatch = true;
        for (int i = 0; i < match.Length; i++)
        {
            isMatch &= pattern.IsMatch(match[i], i);
        }

        isMatch.Should().Be(shouldMatch);
    }

    public static IEnumerable<object[]> GetCtorValueMatchTestCases()
    {
        yield return new object[] { "A", new[] { 'A' }, true };
        yield return new object[] { "AB", new[] { 'A', 'B' }, true };
        yield return new object[] { "AB", new[] { 'a', 'B' }, false };
        yield return new object[] { "ABC", new[] { 'A', 'B', 'c' }, false };
        yield return new object[] { "ABCDEF", new[] { 'A', 'B', 'C', 'D', 'E', 'F' }, true };
    }
}
