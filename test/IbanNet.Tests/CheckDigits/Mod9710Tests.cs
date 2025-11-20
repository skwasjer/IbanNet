namespace IbanNet.CheckDigits;

public sealed class Mod9710Tests
{
    [Theory]
    [MemberData(nameof(TestCases))]
    public void When_computing_it_should_return_expected_check_digits(string value, int expectedCheckDigits)
    {
        // Act
        int actual = Mod9710.Compute(value.ToCharArray());

        // Assert
        actual.Should().Be(expectedCheckDigits);
    }

    public static TheoryData<string, int> TestCases
    {
        get => new()
        {
            { "", 0 },
            { "0", 0 },
            { "A", 10 },
            { "Z", 35 },
            { "DEFGABC", 27 },
            { "7890123456", 2 },
            { "413020987654132098765", 51 },
            { "0ZYX123456789", 19 },
            { "50156524207403489561506901842576617532514813441375548093717782318334730653799034953982636643401134952875909945060236656113267846", 16 },
            { "NL91ABNA0417164300", 1 },
            { "MT84MALT011000012345MTLCAST001S", 1 },
            { "BH67BMAG00001299123456", 1 },
            { "C012AB", 85 },
        };
    }

    [Theory]
    [InlineData("A0@1", 2)]
    [InlineData("ABC012@3", 6)]
    public void Given_that_value_contains_invalid_character_when_computing_it_should_throw(string value, int errorPos)
    {
        this.Invoking(_ => Mod9710.Compute(value.ToCharArray()))
            .Should()
            .Throw<InvalidTokenException>()
            .WithMessage($"Expected alphanumeric character at position {errorPos}, but found '@'.");
    }

#if !USE_SPANS
    [Fact]
    public void Given_that_value_is_null_when_computing_it_should_throw()
    {
        char[]? value = null;

        this.Invoking(_ => Mod9710.Compute(value!))
            .Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(value));
    }
#endif
}
