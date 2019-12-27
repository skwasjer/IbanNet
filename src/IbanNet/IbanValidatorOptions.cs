using System;
using System.Collections.Generic;
using System.Linq;
using IbanNet.Registry;
using IbanNet.Validation.Methods;
using IbanNet.Validation.Rules;

namespace IbanNet
{
	/// <summary>
	/// Options for <see cref="IbanValidator"/>.
	/// </summary>
	public class IbanValidatorOptions
	{
		/// <summary>
		/// Gets or sets the IBAN country registry factory. Defaults to a new <see cref="IbanRegistry"/>.
		/// </summary>
		public Func<IEnumerable<IbanCountry>> Registry { get; set; } = () => new IbanRegistry();

		/// <summary>
		/// Gets or sets the validation method. Defaults to <see cref="StrictValidation"/>.
		/// </summary>
		public ValidationMethod ValidationMethod { get; set; } = new StrictValidation();

		/// <summary>
		/// Gets or sets custom rules to apply after built-in IBAN validation has taken place.
		/// </summary>
		public ICollection<IIbanValidationRule> Rules { get; set; } = new List<IIbanValidationRule>();

		/// <summary>
		/// Gets the registry as dictionary.
		/// </summary>
		/// <exception cref="InvalidOperationException">Thrown when <see cref="Registry"/> is empty.</exception>
		internal IDictionary<string, IbanCountry> GetRegistry()
		{
			Dictionary<string, IbanCountry>? registry = Registry
				?.Invoke()
				?.ToDictionary(kvp => kvp.TwoLetterISORegionName);

			if (registry == null || registry.Count == 0)
			{
				throw new InvalidOperationException("The provided registry cannot be empty.");
			}

			return registry;
		}
	}
}
