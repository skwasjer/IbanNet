using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules
{
	/// <summary>
	/// Asserts that the IBAN is not an null/empty value.
	/// </summary>
	internal sealed class NotNullOrEmptyRule : IIbanValidationRule
	{
		/// <inheritdoc />
		public ValidationRuleResult Validate(ValidationRuleContext context, string iban)
		{
			return string.IsNullOrEmpty(iban)
				? new BuiltInErrorResult(IbanValidationResult.InvalidLength)
				: ValidationRuleResult.Success;
		}
	}
}
