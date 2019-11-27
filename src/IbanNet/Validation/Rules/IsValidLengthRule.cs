namespace IbanNet.Validation.Rules
{
	/// <summary>
	/// Asserts that the IBAN has the correct length as defined for its country.
	/// </summary>
	internal class IsValidLengthRule : IIbanValidationRule
	{
		/// <inheritdoc />
		public void Validate(ValidationContext context, string iban)
		{
			if (iban.Length != context.Country.Iban.Length)
			{
				context.Result = IbanValidationResult.InvalidLength;
			}
		}
	}
}
