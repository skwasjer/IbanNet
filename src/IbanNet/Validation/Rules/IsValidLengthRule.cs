namespace IbanNet.Validation.Rules
{
	/// <summary>
	/// Asserts that the IBAN has the correct length as defined for its country.
	/// </summary>
	internal class IsValidLengthRule : IIbanValidationRule
	{
		/// <inheritdoc />
		public void Validate(ValidationContext context)
		{
			if (context.Value.Length != context.Country.Iban.Length)
			{
				context.Result = IbanValidationResult.InvalidLength;
			}
		}
	}
}
