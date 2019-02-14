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

		/// <inheritdoc />
		public override void Validate(ValidationContext context)
		{
			base.Validate(context);

			// We have to invert the result of the regex check, since we're testing for the presence of 00, 01 and 99.
			context.Result = context.IsValid
				? IbanValidationResult.IllegalCharacters
				: IbanValidationResult.Valid;
		}
	}
}