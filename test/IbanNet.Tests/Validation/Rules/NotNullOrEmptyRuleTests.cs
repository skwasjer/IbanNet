using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules
{
    public class NotNullOrEmptyRuleTests
    {
        private readonly NotEmptyRule _sut;

        public NotNullOrEmptyRuleTests()
        {
            _sut = new NotEmptyRule();
        }

        [Fact]
        public void Given_empty_value_when_validating_it_should_return_error()
        {
            ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext(string.Empty));

            actual.Should().BeOfType<InvalidLengthResult>();
        }

        [Fact]
        public void Given_non_empty_value_when_validating_it_should_return_success()
        {
            ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext("not-empty"));

            actual.Should().Be(ValidationRuleResult.Success);
        }
    }
}
