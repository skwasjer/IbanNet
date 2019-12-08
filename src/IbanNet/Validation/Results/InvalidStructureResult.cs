namespace IbanNet.Validation.Results
{
	/// <summary>
	/// The result returned when the structure of the IBAN is incorrect.
	/// </summary>
	public class InvalidStructureResult : ErrorResult
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InvalidStructureResult"/> class.
		/// </summary>
		public InvalidStructureResult()
			: base("The structure of the IBAN is incorrect.")
		{
		}
	}
}
