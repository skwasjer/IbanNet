using IbanNet.Validation.Results;
using IbanNet.Validation.Rules;

namespace TestHelpers.FakeRules
{
	public class AnotherTestValidationRule : IIbanValidationRule
	{
		public ValidationRuleResult Validate(ValidationRuleContext context)
		{
			return ValidationRuleResult.Success;
		}
	}
}
