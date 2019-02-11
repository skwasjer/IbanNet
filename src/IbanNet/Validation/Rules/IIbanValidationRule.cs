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
		/// <param name="iban">The IBAN value to validate.</param>
		/// <returns>a validation result, indicating if the rule passed or not</returns>
		IbanValidationResult Validate(string iban);
	}
}