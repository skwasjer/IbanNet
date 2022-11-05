using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules;

public class NoIllegalCharactersRuleTests
{
    private readonly NoIllegalCharactersRule _sut;

    public NoIllegalCharactersRuleTests()
    {
        _sut = new NoIllegalCharactersRule();
    }

    [Theory]
    [InlineData("AB!C", 2)]
    [InlineData("é", 0)]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!", 36)]
    public void Given_invalid_value_when_validating_it_should_return_error(string value, int expectedErrorPos)
    {
        ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext(value));

        actual.Should()
            .BeOfType<IllegalCharactersResult>()
            .Which.Position.Should()
            .Be(expectedErrorPos);
    }

    [Fact]
    public void Given_valid_value_when_validating_it_should_return_success()
    {
        ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext("0123ABCdef"));

        actual.Should().Be(ValidationRuleResult.Success);
    }
}