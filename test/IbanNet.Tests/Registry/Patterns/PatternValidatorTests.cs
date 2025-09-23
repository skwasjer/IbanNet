namespace IbanNet.Registry.Patterns;

public sealed class PatternValidatorTests
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Given_that_tokens_are_empty_when_validating_it_should_return_false(bool isFixedLength)
    {
        var sut = new PatternValidator([], isFixedLength);

        // Act
        bool result = sut.TryValidate("ABC", out int? errorPos);

        // Assert
        result.Should().BeFalse();
        errorPos.Should().Be(0);
    }

    public sealed class ValueBasedTests
    {
        [Theory]
        // Single token
        [InlineData("ABC", null, "ABC")]
        [InlineData("BCD", 0, "ABC")]
        [InlineData("AB", 2, "ABC")]
        [InlineData("ABD", 2, "ABC")]
        [InlineData("ABCD", 3, "ABC")]
        // Multiple tokens
        [InlineData("ABC", null, "AB", "C")]
        [InlineData("BCD", 0, "A", "BC")]
        [InlineData("AB", 2, "A", "BC")]
        [InlineData("ABD", 2, "AB", "C")]
        [InlineData("ABCD", 3, "AB", "C")]
        public void Given_that_token_is_value_based_when_validating_it_should_return_expected
        (
            string value,
            int? expectedErrorPos,
            params string[] tokens)
        {
            var tokenList = tokens.Select(t => new PatternToken(t)).ToList();
            var sut = new PatternValidator(tokenList, true);

            // Act
            bool result = sut.TryValidate(value, out int? errorPos);

            // Assert
            result.Should().Be(!expectedErrorPos.HasValue);
            errorPos.Should().Be(expectedErrorPos);
        }
    }

    public sealed class CategoryBasedTests
    {
        [Theory]
        [InlineData("   ", null, AsciiCategory.Space, 1, 3)] // Input contains spaces only, valid length range is 1-3, no error expected.
        [InlineData("A  ", 0, AsciiCategory.Space, 1, 3)] // Input contains a letter followed by spaces, error at position 0.
        [InlineData("    ", 3, AsciiCategory.Space, 1, 3)] // Input exceeds the maximum length of 3, error at position 3.
        [InlineData("", 0, AsciiCategory.Space, 1, 3)] // Input is empty, error at position 0 due to minimum length requirement.
        [InlineData("12345", null, AsciiCategory.Digit, 5, 5)] // Input contains digits only, valid fixed length of 5, no error expected.
        [InlineData("12A45", 2, AsciiCategory.Digit, 5, 5)] // Input contains a non-digit at position 2, error at position 2.
        [InlineData("123456", 5, AsciiCategory.Digit, 5, 5)] // Input exceeds the fixed length of 5, error at position 5.
        [InlineData("123", 3, AsciiCategory.Digit, 5, 5)] // Input is shorter than the fixed length of 5, error at position 3.
        [InlineData("ABC", null, AsciiCategory.UppercaseLetter, 3, 3)] // Input contains uppercase letters only, valid fixed length of 3, no error expected.
        [InlineData("AbC", 1, AsciiCategory.UppercaseLetter, 3, 3)] // Input contains a lowercase letter at position 1, error at position 1.
        [InlineData("ABCD", 3, AsciiCategory.UppercaseLetter, 3, 3)] // Input exceeds the fixed length of 3, error at position 3.
        [InlineData("AB", 2, AsciiCategory.UppercaseLetter, 3, 3)] // Input is shorter than the fixed length of 3, error at position 2.
        [InlineData("abc", null, AsciiCategory.LowercaseLetter, 3, 3)] // Input contains lowercase letters only, valid fixed length of 3, no error expected.
        [InlineData("aBc", 1, AsciiCategory.LowercaseLetter, 3, 3)] // Input contains an uppercase letter at position 1, error at position 1.
        [InlineData("abcd", 3, AsciiCategory.LowercaseLetter, 3, 3)] // Input exceeds the fixed length of 3, error at position 3.
        [InlineData("ab", 2, AsciiCategory.LowercaseLetter, 3, 3)] // Input is shorter than the fixed length of 3, error at position 2.
        [InlineData("abcABC", null, AsciiCategory.Letter, 3, 6)] // Input contains mixed case letters, valid length range is 3-6, no error expected.
        [InlineData("abc123", 3, AsciiCategory.Letter, 3, 6)] // Input contains digits starting at position 3, error at position 3.
        [InlineData("abcABCD", 6, AsciiCategory.Letter, 3, 6)] // Input exceeds the maximum length of 6, error at position 6.
        [InlineData("ab", 2, AsciiCategory.Letter, 3, 6)] // Input is shorter than the minimum length of 3, error at position 2.
        [InlineData("12", null, AsciiCategory.Digit, 2, 5)] // Input contains digits only, valid length range is 2-5, no error expected.
        [InlineData("123", null, AsciiCategory.Digit, 2, 5)] // Input contains digits only, valid length range is 2-5, no error expected.
        [InlineData("12345", null, AsciiCategory.Digit, 2, 5)] // Input contains digits only, valid length range is 2-5, no error expected.
        [InlineData("123456", 5, AsciiCategory.Digit, 2, 5)] // Input exceeds the maximum length of 5, error at position 5.
        [InlineData("1", 1, AsciiCategory.Digit, 2, 5)] // Input is shorter than the minimum length of 2, error at position 1.
        [InlineData("aBc123", null, AsciiCategory.AlphaNumeric, 6, 6)] // Input contains alphanumeric characters, valid fixed length of 6, no error expected.
        [InlineData("aBc123!", 6, AsciiCategory.AlphaNumeric, 6, 6)] // Input contains a non-alphanumeric character at position 6, error at position 6.
        [InlineData("123", null, AsciiCategory.AlphaNumeric, 3, 6)] // Input contains digits only, valid length range is 3-6, no error expected.
        [InlineData("aBc", null, AsciiCategory.AlphaNumeric, 3, 6)] // Input contains letters only, valid length range is 3-6, no error expected.
        [InlineData("aBc123", null, AsciiCategory.AlphaNumeric, 3, 6)] // Input contains alphanumeric characters, valid length range is 3-6, no error expected.
        [InlineData("aBc1234", 6, AsciiCategory.AlphaNumeric, 3, 6)] // Input exceeds the maximum length of 6, error at position 6.
        [InlineData("1", 1, AsciiCategory.AlphaNumeric, 2, 5)] // Input is shorter than the minimum length of 2, error at position 1.
        [InlineData("1B", null, AsciiCategory.AlphaNumeric, 2, 5)] // Input contains digits only, valid length range is 2-5, no error expected.
        [InlineData("aBc45", null, AsciiCategory.AlphaNumeric, 2, 5)] // Input contains digits only, valid length range is 2-5, no error expected.
        [InlineData("aBc456", 5, AsciiCategory.AlphaNumeric, 2, 5)] // Input exceeds the maximum length of 5, error at position 5.
        public void Given_that_single_token_is_category_based_when_validating_it_should_return_expected
        (
            string value,
            int? expectedErrorPos,
            AsciiCategory category,
            int minLength,
            int maxLength)
        {
            var sut = new PatternValidator([new PatternToken(category, minLength, maxLength)], minLength == maxLength);

            // Act
            bool result = sut.TryValidate(value, out int? errorPos);

            // Assert
            result.Should().Be(!expectedErrorPos.HasValue);
            errorPos.Should().Be(expectedErrorPos);
        }
    }
}
