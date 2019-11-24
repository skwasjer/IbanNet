using System;
using System.Collections.Generic;
using IbanNet.Registry;
using IbanNet.Validation.Methods;

namespace IbanNet
{
	/// <summary>
	/// Options for <see cref="IbanValidator"/>.
	/// </summary>
	public class IbanValidatorOptions
	{
		/// <summary>
		/// Gets or sets the IBAN country registry builder.
		/// </summary>
		public Func<IReadOnlyCollection<CountryInfo>> Registry { get; set; } = () => new IbanRegistry();

		/// <summary>
		/// Gets or sets the validation method. Defaults to <see cref="StrictValidation"/>.
		/// </summary>
		public ValidationMethod ValidationMethod { get; set; } = new StrictValidation();
	}
}
