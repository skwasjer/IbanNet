namespace IbanNet.Validation.Rules
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
		/// Validates the IBAN against this rule.
		/// </summary>
		/// <param name="iban">The IBAN to validate.</param>
		/// <returns>true if the IBAN is valid, or false otherwise</returns>
		public override IbanValidationResult Validate(string iban)
		{
			// We have to invert the result of the regex check, since we're testing for the presence of non-word characters.
			return base.Validate(iban) == IbanValidationResult.Valid
				? IbanValidationResult.IllegalCharacters
				: IbanValidationResult.Valid;
		}
	}
}