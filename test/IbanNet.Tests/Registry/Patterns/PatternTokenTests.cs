using System.ComponentModel;

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
    [MemberData(nameof(GetTestCases))]
    public void When_creating_instance_it_should_set_properties
    (
        AsciiCategory category,
        int minLength,
        int maxLength,
        bool isFixedLength,
        bool isLower,
        bool isUpper,
        bool isDigit)
    {
        // Act
        var pattern = new PatternToken(category, minLength, maxLength);

        // Assert
        pattern.Category.Should().Be(category);
        pattern.MinLength.Should().Be(minLength);
        pattern.MaxLength.Should().Be(maxLength);
        pattern.IsFixedLength.Should().Be(isFixedLength);
        pattern.IsMatch('a').Should().Be(isLower);
        pattern.IsMatch('A').Should().Be(isUpper);
        pattern.IsMatch('0').Should().Be(isDigit);
    }

    public static IEnumerable<object[]> GetTestCases()
    {
        yield return new object[] { AsciiCategory.None, 1, 1, true, false, false, false };
        yield return new object[] { AsciiCategory.Space, 1, 1, true, false, false, false };
        yield return new object[] { AsciiCategory.Digit, 1, 2, false, false, false, true };
        yield return new object[] { AsciiCategory.LowercaseLetter, 2, 4, false, true, false, false };
        yield return new object[] { AsciiCategory.UppercaseLetter, 3, 4, false, false, true, false };
        yield return new object[] { AsciiCategory.Letter, 6, 6, true, true, true, false };
        yield return new object[] { AsciiCategory.AlphaNumeric, 3, 3, true, true, true, true };
    }
}
