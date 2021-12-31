namespace IbanNet.CheckDigits.Calculators
{
    public class Mod97CheckDigitsCalculatorTests
    {
        private readonly Mod97CheckDigitsCalculator _sut;

        public Mod97CheckDigitsCalculatorTests()
        {
            _sut = new Mod97CheckDigitsCalculator();
        }

        [Theory]
        [InlineData("BMAG00001299123456BH67", 1)]
        [InlineData("ABC012", 85)]
        [InlineData("", 0)]
        public void Given_value_when_computing_should_return_expected_check_digits(string value, int expectedCheckDigits)
        {
            // Act
            int actual = _sut.Compute(value.ToCharArray());

            // Assert
            actual.Should().Be(expectedCheckDigits);
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
                .Be($"Expected alphanumeric character at position 2, but found '@'.");
        }

        [Fact]
        public void Given_null_value_when_computing_it_should_throw()
        {
            char[] value = null;

            // Act
            // ReSharper disable once AssignNullToNotNullAttribute
            Action act = () => _sut.Compute(value);

            // Assert
            act.Should()
                .Throw<ArgumentNullException>()
                .Which.ParamName.Should()
                .Be(nameof(value));
        }
    }
}
