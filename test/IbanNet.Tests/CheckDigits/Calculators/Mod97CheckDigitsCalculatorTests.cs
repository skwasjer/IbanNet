namespace IbanNet.CheckDigits.Calculators;

public class Mod97CheckDigitsCalculatorTests
{
    private readonly Mod97CheckDigitsCalculator _sut;

    public Mod97CheckDigitsCalculatorTests()
    {
        _sut = new Mod97CheckDigitsCalculator();
    }

    [Theory]
    [MemberData(nameof(TestCases))]
    public void Given_value_when_computing_should_return_expected_check_digits(string value, int expectedCheckDigits)
    {
        // Act
        int actual = _sut.Compute(value.ToCharArray());

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
            { "ABCDEFG", 27 },
            { "1234567890", 2 },
            { "209876541320987654130", 51 },
            { "1234567890ZYX", 19 },
            { "65242074034895615069018425766175325148134413755480937177823183347306537990349539826366434011349528759099450602366561132678465015", 16 },
            { "ABNA0417164300NL91", 1 },
            { "MALT011000012345MTLCAST001SMT84", 1 },
            { "BMAG00001299123456BH67", 1 },
            { "ABC012", 85 }
        };
    }

    [Fact]
    public void Given_value_contains_invalid_character_when_computing_it_should_throw()
    {
        // Act
        Action act = () => _sut.Compute("A0@1".ToCharArray());

        // Assert
        act.Should()
            .Throw<InvalidTokenException>()
            .Which.Message.Should()
            .Be("Expected alphanumeric character at position 2, but found '@'.");
    }

    [Fact]
    public void Given_null_value_when_computing_it_should_throw()
    {
        char[]? value = null;

        // Act
        Action act = () => _sut.Compute(value!);

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(value));
    }
}
