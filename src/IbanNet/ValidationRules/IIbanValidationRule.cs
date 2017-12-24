namespace IbanNet.ValidationRules
{
	/// <summary>
	/// Describes a validation rule for IBAN.
	/// </summary>
	internal interface IIbanValidationRule
	{
		/// <summary>
		/// The validation result to use when this rule is not valid.
		/// </summary>
		IbanValidationResult InvalidResult { get; }

		/// <summary>
		/// Validates the IBAN against this rule.
		/// </summary>
		/// <param name="iban">The IBAN to validate.</param>
		/// <returns>true if the IBAN is valid, or false otherwise</returns>
		bool Validate(string iban);
	}
}