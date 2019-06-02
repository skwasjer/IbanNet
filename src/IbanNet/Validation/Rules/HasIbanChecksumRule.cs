namespace IbanNet.Validation.Rules
{
	/// <summary>
	/// Asserts that the IBAN checksum digits are not 00, 01 or 99.
	/// </summary>
	internal class HasIbanChecksumRule : RegexRule
	{
		public HasIbanChecksumRule() : base(@"^\D\D00|^\D\D01|^\D\D99")
		{
		}

		/// <inheritdoc />
		public override void Validate(ValidationRuleContext context)
		{
			base.Validate(context);

			// We have to invert the result of the regex check, since we're testing for the presence of 00, 01 and 99.
			context.Result = context.IsValid
				? IbanValidationResult.IllegalCharacters
				: IbanValidationResult.Valid;
		}
	}
}