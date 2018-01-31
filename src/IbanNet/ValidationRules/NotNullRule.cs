namespace IbanNet.ValidationRules
{
	/// <summary>
	/// Asserts that the IBAN is not null.
	/// </summary>
	internal sealed class NotNullRule : IIbanValidationRule
	{
		/// <summary>
		/// Validates the IBAN against this rule.
		/// </summary>
		/// <param name="iban">The IBAN to validate.</param>
		/// <returns>true if the IBAN is valid, or false otherwise</returns>
		public IbanValidationResult Validate(string iban)
		{
			return iban != null
				? IbanValidationResult.Valid
				: IbanValidationResult.InvalidLength;
		}
	}
}