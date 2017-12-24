namespace IbanNet.ValidationRules
{
	/// <summary>
	/// Asserts that the IBAN is not null.
	/// </summary>
	internal sealed class NotNullRule : IIbanValidationRule
	{
		/// <summary>
		/// The validation result to use when this rule is not valid.
		/// </summary>
		public IbanValidationResult InvalidResult { get; } = IbanValidationResult.IncorrectLength;

		/// <summary>
		/// Validates the IBAN against this rule.
		/// </summary>
		/// <param name="iban">The IBAN to validate.</param>
		/// <returns>true if the IBAN is valid, or false otherwise</returns>
		public bool Validate(string iban)
		{
			return iban != null;
		}
	}
}