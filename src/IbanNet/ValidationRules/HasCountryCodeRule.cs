namespace IbanNet.ValidationRules
{
	/// <summary>
	/// Asserts that the IBAN has a country code but does not check the validity of the country code itself.
	/// </summary>
	internal class HasCountryCodeRule : RegexRule
	{
		public HasCountryCodeRule() : base(@"^\D\D")
		{
		}

		/// <summary>
		/// The validation result to use when this rule is not valid.
		/// </summary>
		public override IbanValidationResult InvalidResult { get; } = IbanValidationResult.IllegalCharacters;
	}
}