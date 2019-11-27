namespace IbanNet.Validation.Results
{
	/// <summary>
	/// The IBAN has an incorrect length.
	/// </summary>
	public class InvalidCheckDigitsResult : ErrorResult
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InvalidCheckDigitsResult"/> class.
		/// </summary>
		public InvalidCheckDigitsResult()
			: base("The IBAN check digits are incorrect.")
		{
		}
	}
}
