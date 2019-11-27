using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules
{
	/// <summary>
	/// Asserts that the IBAN has the correct length as defined for its country.
	/// </summary>
	internal class IsValidLengthRule : IIbanValidationRule
	{
		/// <inheritdoc />
		public ValidationRuleResult Validate(ValidationRuleContext context)
		{
			return context.Country != null && context.Value.Length == context.Country.Iban.Length
				? ValidationRuleResult.Success
				: new InvalidLengthResult();
		}
	}
}
