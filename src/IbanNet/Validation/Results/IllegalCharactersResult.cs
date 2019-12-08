namespace IbanNet.Validation.Results
{
	/// <summary>
	/// The result returned when the IBAN contains illegal characters.
	/// </summary>
	public class IllegalCharactersResult : ErrorResult
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="IllegalCharactersResult"/> class.
		/// </summary>
		public IllegalCharactersResult()
			: base("The IBAN contains illegal characters.")
		{
		}
	}
}
