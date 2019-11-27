namespace IbanNet.Validation.Results
{
	/// <summary>
	/// The IBAN has an incorrect length.
	/// </summary>
	public class UnknownCountryCodeResult : ErrorResult
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UnknownCountryCodeResult"/> class.
		/// </summary>
		public UnknownCountryCodeResult()
			: base("The country code is unknown/not supported.")
		{
		}
	}
}
