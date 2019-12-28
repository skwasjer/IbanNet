namespace IbanNet.Validation.Methods
{
	/// <summary>
	/// Loose validation consists of all built-in IBAN validation rules, except for checking the entire IBAN structure.
	/// This means that the IBAN could potentially contain certain characters that are officially not allowed, but could
	/// pass all other criteria (even including check digit).
	/// </summary>
	/// <remarks>
	/// Loose validation is around 15%-20% faster than strict validation.
	/// </remarks>
	public class LooseValidation : ValidationMethod
	{
	}
}
