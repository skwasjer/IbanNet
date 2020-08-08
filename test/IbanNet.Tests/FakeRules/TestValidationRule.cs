using IbanNet.Validation.Results;
using IbanNet.Validation.Rules;

namespace IbanNet.FakeRules
{
    public class TestValidationRule : IIbanValidationRule
    {
        public ValidationRuleResult Validate(ValidationRuleContext context)
        {
            return ValidationRuleResult.Success;
        }
    }
}
