namespace IbanNet.Validation.Results
{
	/// <summary>
	/// The result returned when the IBAN has an incorrect length.
	/// </summary>
	public class InvalidLengthResult : ErrorResult
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InvalidLengthResult"/> class.
		/// </summary>
		public InvalidLengthResult()
			: base(Resources.InvalidLengthResult)
		{
		}
	}
}
