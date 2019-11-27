namespace IbanNet.Validation.Results
{
	/// <summary>
	/// The IBAN has an incorrect length.
	/// </summary>
	public class InvalidLengthResult : ErrorResult
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InvalidLengthResult"/> class.
		/// </summary>
		public InvalidLengthResult()
			: base("The IBAN has an incorrect length.")
		{
		}
	}
}
