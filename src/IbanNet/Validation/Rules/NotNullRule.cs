namespace IbanNet.Validation.Rules
{
	/// <summary>
	/// Asserts that the IBAN is not null.
	/// </summary>
	internal sealed class NotNullRule : IIbanValidationRule
	{
		/// <inheritdoc />
		public void Validate(ValidationContext context)
		{
			if (context.Value == null)
			{
				context.Result = IbanValidationResult.InvalidLength;
			}
		}
	}
}