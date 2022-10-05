namespace IbanNet.Internal;

public class InputNormalizationTests
{
    [Theory]
    [InlineData("no-whitespace", "NO-WHITESPACE")]
    [InlineData(" \ti n-\nst \t r ing\r ", "IN-\nSTRING\r")]
    [InlineData("(&*!S #%t", "(&*!S#%T")]
    [InlineData("", "")]
    [InlineData(null, null)]
    public void Given_string_when_normalizing_it_should_return_expected_value(string input, string expected)
    {
        // Act
        string actual = InputNormalization.NormalizeOrNull(input);

        // Assert
        actual.Should().Be(expected);
    }

#if USE_SPANS
    [Fact]
    public void Given_that_string_exceeds_max_stackalloc_length_when_normalizing_it_should_return_expected_value()
    {
        string spaces = new(' ', 50);
        string input = spaces + " \tin-str ing" + spaces;
        input.Length.Should().BeGreaterThan(Iban.MaxLength * 2);
        const string expected = "IN-STRING";

        // Act
        string actual = InputNormalization.NormalizeOrNull(input);

        // Assert
        actual.Should().Be(expected);
    }
#endif
}
