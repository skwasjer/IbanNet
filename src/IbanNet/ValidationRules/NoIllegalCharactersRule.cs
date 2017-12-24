namespace IbanNet.ValidationRules
{
	/// <summary>
	/// Asserts that the IBAN does not contain any illegal characters.
	/// </summary>
	internal class NoIllegalCharactersRule : RegexRule
	{
		public NoIllegalCharactersRule() : base(@"\W")
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
			// We have to invert the result of the regex check, since we're testing for the presence of non-word characters.
			return base.Validate(iban) == false;
		}
	}
}