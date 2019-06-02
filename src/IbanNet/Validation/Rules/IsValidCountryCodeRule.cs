using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules
{
	/// <summary>
	/// Asserts that the IBAN has a valid and known country code.
	/// </summary>
	internal class IsValidCountryCodeRule : IIbanValidationRule
	{
		/// <inheritdoc />
		public ValidationRuleResult Validate(ValidationRuleContext context, string iban)
		{
			return context.Country == null
				? new BuiltInErrorResult(IbanValidationResult.UnknownCountryCode)
				: ValidationRuleResult.Success;
		}
	}
}
