namespace IbanNet
{
	/// <summary>
	/// Represents possible validation results.
	/// </summary>
	public enum IbanValidationResult
	{
		/// <summary>
		/// The IBAN seems correct.
		/// </summary>
		Valid,

		/// <summary>
		/// The IBAN contains illegal characters.
		/// </summary>
		IllegalCharacters,

		/// <summary>
		/// The country code is unknown/not supported.
		/// </summary>
		UnknownCountryCode,

		/// <summary>
		/// The structure of the IBAN is incorrect.
		/// </summary>
		InvalidStructure,

		/// <summary>
		/// The IBAN check digits are incorrect.
		/// </summary>
		InvalidCheckDigits,

		/// <summary>
		/// The IBAN has an incorrect length.
		/// </summary>
		InvalidLength
	}
}