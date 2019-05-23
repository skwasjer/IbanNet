using System.Collections.Generic;

namespace IbanNet.FluentValidation
{
	/// <summary>
	/// Represents IBAN validation options.
	/// </summary>
	public class IbanValidationOptions
	{
		/// <summary>
		/// Gets or sets the <see cref="IIbanValidator"/> instance to use for validation.
		/// </summary>
		public IIbanValidator Validator { get; set; }

		/// <summary>
		/// Gets or sets the accepted country codes. If null or empty, all supported countries are accepted.
		/// </summary>
		public ICollection<string> CountryCodes { get; set; } = new List<string>();
	}
}