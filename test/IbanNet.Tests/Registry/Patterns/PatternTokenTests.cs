using System.ComponentModel;

namespace IbanNet.Registry.Patterns;

public static class PatternTokenTests
{
    public class CategoryBasedTests
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

        [Theory]
        [InlineData((AsciiCategory)int.MaxValue)]
        [InlineData(AsciiCategory.None)]
        public void Given_that_category_is_invalid_when_creating_instance_it_should_throw(AsciiCategory category)
        {
            // Act
            Func<PatternToken> act = () => new PatternToken(category, 10);

            // Assert
            act.Should()
                .Throw<InvalidEnumArgumentException>()
                .WithParameterName(nameof(category));
        }

        [Theory]
        [InlineData((AsciiCategory)int.MaxValue)]
        [InlineData(AsciiCategory.None)]
        public void Given_that_category_is_invalid_when_creating_instance_it_should_throw2(AsciiCategory category)
        {
            // Act
            Func<PatternToken> act = () => new PatternToken(category, 10, 10);

            // Assert
            act.Should()
                .Throw<InvalidEnumArgumentException>()
                .WithParameterName(nameof(category));
        }

        [Theory]
        [MemberData(nameof(GetCtorAsciiTestCases))]
        public void When_creating_instance_it_should_set_properties
        (
            AsciiCategory category,
            int minLength,
            int maxLength,
            bool isFixedLength
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
        }

        [Theory]
        [MemberData(nameof(GetCtorAsciiTestCases))]
        public void When_creating_instance_twice_it_should_equal_each_other
        (
            AsciiCategory category,
            int minLength,
            int maxLength,
            bool isFixedLength
        )
        {
            // Act
            var pattern1 = new PatternToken(category, minLength, maxLength);
            var pattern2 = new PatternToken(category, minLength, maxLength);

            // Assert
            pattern1.Equals(pattern2).Should().BeTrue();
        }

        public static IEnumerable<object[]> GetCtorAsciiTestCases()
        {
            yield return [AsciiCategory.Space, 1, 1, true];
            yield return [AsciiCategory.Digit, 1, 2, false];
            yield return [AsciiCategory.LowercaseLetter, 2, 4, false];
            yield return [AsciiCategory.UppercaseLetter, 3, 4, false];
            yield return [AsciiCategory.Letter, 6, 6, true];
            yield return [AsciiCategory.AlphaNumeric, 3, 3, true];
        }

        [Fact]
        public void When_comparing_two_different_instances_for_equality_it_should_not_equal()
        {
            // Act
            var pattern1 = new PatternToken(AsciiCategory.Digit, 1);
            var pattern2 = new PatternToken(AsciiCategory.Digit, 2);

            // Assert
            pattern1.Equals(pattern2).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(GetFormatTestCases))]
        public void It_should_format_correctly(AsciiCategory category, int minLength, int maxLength, string expected)
        {
            var patternToken = new PatternToken(category, minLength, maxLength);

            // Act
            string result = patternToken.ToString();

            // Assert
            result.Should().Be(expected);
        }

        public static IEnumerable<object[]> GetFormatTestCases()
        {
            yield return [AsciiCategory.Space, 1, 1, "Space[1]"];
            yield return [AsciiCategory.Digit, 1, 2, "Digit[1-2]"];
            yield return [AsciiCategory.LowercaseLetter, 2, 4, "LowercaseLetter[2-4]"];
            yield return [AsciiCategory.UppercaseLetter, 3, 4, "UppercaseLetter[3-4]"];
            yield return [AsciiCategory.Letter, 6, 6, "Letter[6]"];
            yield return [AsciiCategory.AlphaNumeric, 3, 3, "AlphaNumeric[3]"];
        }
    }

    public class ValueBasedTests
    {
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

        [Fact]
        public void Given_that_pattern_value_is_empty_when_creating_instance_it_should_throw()
        {
            string value = string.Empty;

            // Act
            Func<PatternToken> act = () => new PatternToken(value);

            // Assert
            act.Should()
                .Throw<ArgumentOutOfRangeException>()
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
#if NET7_0_OR_GREATER
            pattern.ToString().Should().Be($"None[{maxLength}]");
#else
            pattern.ToString().Should().Be($"Other[{maxLength}]");
#endif
        }

        [Theory]
        [MemberData(nameof(GetCtorValueTestCases))]
        public void When_creating_instance_twice_it_should_equal_each_other
        (
            string value,
            int _,
            int __
        )
        {
            // Act
            var pattern1 = new PatternToken(value);
            var pattern2 = new PatternToken(value);

            // Assert
            pattern1.Equals(pattern2).Should().BeTrue();
        }

        public static IEnumerable<object[]> GetCtorValueTestCases()
        {
            yield return ["A", 1, 1];
            yield return ["AB", 2, 2];
            yield return ["ABCDEF", 6, 6];
        }

        [Fact]
        public void When_comparing_two_different_instances_for_equality_it_should_not_equal()
        {
            // Act
            var pattern1 = new PatternToken("AB");
            var pattern2 = new PatternToken("AC");

            // Assert
            pattern1.Equals(pattern2).Should().BeFalse();
        }
    }
}
