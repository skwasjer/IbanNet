using IbanNet.Registry;
using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules;

public sealed class IsValidQrIbanRuleTests
{
    private readonly QrIbanRule _sut;

    public IsValidQrIbanRuleTests()
    {
        _sut = new QrIbanRule();
    }

    [InlineData("DE89370400440532013000")]
    [InlineData("FR1420041010050500013M02606")]
    [InlineData("CH93007620116238529579")]
    [InlineData("DE893704004053201300")]
    [InlineData("DE19370400440532013000")]
    [InlineData("DE193%0400440532013000")]
    [InlineData("DE193A0400440532013000")]
    [InlineData("LI21088100002324013AA")]
    [InlineData("LI7830X74502999200012")]
    [InlineData("CH44 3199 9123 0008 8901 2")]
    [Theory]
    public void Given_invalid_values_it_should_return_success(string iban)
    {
        IbanCountry country = IbanRegistry.Default[iban.Substring(0, 2)];

        // Act
        ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext(iban, country));

        // Assert
        actual.Should().BeOfType<InvalidQrIbanResult>();
    }

    [InlineData("LI7830174502999200012")]
    [InlineData("CH4431999123000889012")]
    [Theory]
    public void Given_valid_values_it_should_return_success(string iban)
    {
        IbanCountry country = IbanRegistry.Default[iban.Substring(0, 2)];

        // Act
        ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext(iban, country));

        // Assert
        actual.Should().Be(ValidationRuleResult.Success);
    }

    [Fact]
    public void Given_that_context_is_null_when_validating_it_should_throw()
    {
        ValidationRuleContext? context = null;

        // Act
        Action act = () => _sut.Validate(context!);

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(context));
    }
}
