using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules
{
	/// <summary>
	/// Asserts that the IBAN has a valid and known country code.
	/// </summary>
	internal sealed class IsValidCountryCodeRule : IIbanValidationRule
	{
		/// <inheritdoc />
		public ValidationRuleResult Validate(ValidationRuleContext context)
		{
			return context.Country == null
				? new UnknownCountryCodeResult()
				: ValidationRuleResult.Success;
		}
	}
}
