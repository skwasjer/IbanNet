namespace IbanNet.Validation.Rules
{
	/// <summary>
	/// Describes a validation rule for IBAN.
	/// </summary>
	internal interface IIbanValidationRule
	{
		/// <summary>
		/// Validates the IBAN against this rule.
		/// </summary>
		/// <param name="context">The validation context.</param>
		/// <param name="iban">The IBAN to validate.</param>
		void Validate(ValidationContext context, string iban);
	}
}
