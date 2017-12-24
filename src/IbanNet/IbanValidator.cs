namespace IbanNet
{
	/// <summary>
	/// Represents the default IBAN validator.
	/// </summary>
	public class IbanValidator : IIbanValidator
	{
		public IbanValidationResult Validate(string iban)
		{
			return IbanValidationResult.Valid;
		}
	}
}
