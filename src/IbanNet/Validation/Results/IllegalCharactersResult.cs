namespace IbanNet.Validation.Results
{
	/// <summary>
	/// The IBAN has an incorrect length.
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
