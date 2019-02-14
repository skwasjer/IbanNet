namespace IbanNet.Validation.Rules
{
	/// <summary>
	/// Asserts that the IBAN has a valid and known country code.
	/// </summary>
	internal class IsValidCountryCodeRule : IIbanValidationRule
	{
		/// <inheritdoc />
		public void Validate(ValidationContext context)
		{
			if (context.Country == null)
			{
				context.Result = IbanValidationResult.UnknownCountryCode;
			}
		}
	}
}
