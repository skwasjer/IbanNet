using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules;

public class Mod97RuleTests
{
    private readonly Mod97Rule _sut;

    public Mod97RuleTests()
    {
        _sut = new Mod97Rule();
    }

    [Theory]
    [InlineData("NL92ABNA0417164300")]
    [InlineData("MT44MALT011000012345MTLCAST001S")]
    [InlineData("BH01BMAG00001299123456")]
    public void Given_invalid_value_when_validating_it_should_return_error(string value)
    {
        // Act
        ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext(value));

        // Assert
        actual.Should().BeOfType<InvalidCheckDigitsResult>();
    }

    [Theory]
    [InlineData("NL91ABNA0417164300")]
    [InlineData("MT84MALT011000012345MTLCAST001S")]
    [InlineData("BH67BMAG00001299123456")]
    public void Given_valid_value_when_validating_it_should_return_success(string value)
    {
        // Act
        ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext(value));

        // Assert
        actual.Should().BeSameAs(ValidationRuleResult.Success);
    }
}
