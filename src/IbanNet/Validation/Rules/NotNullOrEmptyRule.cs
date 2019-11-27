namespace IbanNet.Validation.Rules
{
	/// <summary>
	/// Asserts that the IBAN is not an null/empty value.
	/// </summary>
	internal sealed class NotNullOrEmptyRule : IIbanValidationRule
	{
		/// <inheritdoc />
		public void Validate(ValidationContext context)
		{
			if (string.IsNullOrEmpty(context.Value))
			{
				context.Result = IbanValidationResult.InvalidLength;
			}
		}
	}
}
