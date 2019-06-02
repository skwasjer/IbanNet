using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using IbanNet.Registry;
using IbanNet.Validation.Rules;

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
		public Lazy<IReadOnlyCollection<CountryInfo>> Registry { get; set; } = new Lazy<IReadOnlyCollection<CountryInfo>>(() => new IbanRegistry(), LazyThreadSafetyMode.ExecutionAndPublication);

		/// <summary>
		/// Gets or sets custom rules to apply after built-in IBAN validation has taken place.
		/// </summary>
		public ICollection<IIbanValidationRule> Rules { get; set; } = new List<IIbanValidationRule>();
	}
}
