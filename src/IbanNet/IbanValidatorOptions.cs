using System;
using System.Collections.Generic;
using IbanNet.Registry;

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
	}
}
