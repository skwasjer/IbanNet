namespace IbanNet
{
	/// <summary>
	/// Describes a validator for IBAN.
	/// </summary>
	public interface IIbanValidator
	{
		/// <summary>
		/// Validates the specified <paramref name="iban"/> for correctness.
		/// </summary>
		/// <param name="iban">The IBAN value.</param>
		/// <returns>a validation result, indicating if the IBAN is valid or not</returns>
		IbanValidationResult Validate(string iban);
	}
}
