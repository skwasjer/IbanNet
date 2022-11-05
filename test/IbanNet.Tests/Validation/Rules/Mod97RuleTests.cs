using IbanNet.CheckDigits.Calculators;
using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules;

public class Mod97RuleTests
{
    private readonly Mod97Rule _sut;
    private readonly Mock<ICheckDigitsCalculator> _calculatorMock;

    public Mod97RuleTests()
    {
        _calculatorMock = new Mock<ICheckDigitsCalculator>();
        _sut = new Mod97Rule(_calculatorMock.Object);
    }

    [Fact]
    public void Given_invalid_value_when_validating_it_should_return_error()
    {
        const string value = "ABCD123456";
        const int invalidCheckDigit = 123;
        _calculatorMock
            .Setup(m => m.Compute(It.Is<char[]>(buf => buf.SequenceEqual("123456ABCD"))))
            .Returns(invalidCheckDigit)
            .Verifiable();

        // Act
        ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext(value));

        // Assert
        actual.Should().BeOfType<InvalidCheckDigitsResult>();
        _calculatorMock.Verify();
    }

    [Fact]
    public void Given_valid_value_when_validating_it_should_return_success()
    {
        const string value = "ABCD123456";
        const int expectedCheckDigit = 1;
        _calculatorMock
            .Setup(m => m.Compute(It.Is<char[]>(buf => buf.SequenceEqual("123456ABCD"))))
            .Returns(expectedCheckDigit)
            .Verifiable();

        // Act
        ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext(value));

        // Assert
        actual.Should().Be(ValidationRuleResult.Success);
        _calculatorMock.Verify();
    }
}