namespace IbanNet.ValidationRules
{
	/// <summary>
	/// Asserts that the IBAN checksum digits are not 00, 01 or 99.
	/// </summary>
	internal class HasIbanChecksumRule : RegexRule
	{
		public HasIbanChecksumRule() : base(@"^\D\D00|^\D\D01|^\D\D99")
		{
		}

		/// <summary>
		/// The validation result to use when this rule is not valid.
		/// </summary>
		public override IbanValidationResult InvalidResult { get; } = IbanValidationResult.IllegalCharacters;

		/// <summary>
		/// Validates the IBAN against this rule.
		/// </summary>
		/// <param name="iban">The IBAN to validate.</param>
		/// <returns>true if the IBAN is valid, or false otherwise</returns>
		public override bool Validate(string iban)
		{
			// We have to invert the result of the regex check, since we're testing for the presence of 00, 01 and 99.
			return base.Validate(iban) == false;
		}
	}
}