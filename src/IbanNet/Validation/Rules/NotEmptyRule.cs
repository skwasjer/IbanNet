using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules
{
	/// <summary>
	/// Asserts that the IBAN is not an empty value.
	/// </summary>
	internal sealed class NotEmptyRule : IIbanValidationRule
	{
		/// <inheritdoc />
		public ValidationRuleResult Validate(ValidationRuleContext context)
		{
			return context.Value.Length == 0
				? new InvalidLengthResult()
				: ValidationRuleResult.Success;
		}
	}
}
