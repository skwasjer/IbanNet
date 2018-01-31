namespace IbanNet.ValidationRules
{
	/// <summary>
	/// Describes a validation rule for IBAN.
	/// </summary>
	internal interface IIbanValidationRule
	{
		/// <summary>
		/// Validates the IBAN against this rule.
		/// </summary>
		/// <param name="iban">The IBAN to validate.</param>
		/// <returns>true if the IBAN is valid, or false otherwise</returns>
		IbanValidationResult Validate(string iban);
	}
}