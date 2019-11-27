using IbanNet.Registry;

namespace IbanNet.Validation.Rules
{
	internal class ValidationContext
	{
		/// <summary>
		/// <see cref="IbanValidationResult.Valid"/> if validation succeeded. Otherwise, indicates the reason of failure. 
		/// </summary>
		public IbanValidationResult Result { get; set; }

		/// <summary>
		/// Gets whether validation is successful.
		/// </summary>
		public bool IsValid => Result == IbanValidationResult.Valid;

		/// <summary>
		/// Gets or sets the country info that matches the iban, if any.
		/// </summary>
		public CountryInfo? Country { get; set; }
	}
}
