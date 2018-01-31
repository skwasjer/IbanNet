using System;
using System.Collections.Generic;

namespace IbanNet.ValidationRules
{
	/// <summary>
	/// A base class for country specific IBAN validation rules.
	/// </summary>
	internal abstract class CountrySpecificRule : IIbanValidationRule
	{
		private readonly IReadOnlyDictionary<string, IbanRegionDefinition> _definitions;

		protected CountrySpecificRule(IReadOnlyDictionary<string, IbanRegionDefinition> definitions)
		{
			if (definitions == null)
			{
				throw new ArgumentNullException(nameof(definitions));
			}
			_definitions = definitions;
		}

		/// <summary>
		/// Validates the IBAN against this rule.
		/// </summary>
		/// <param name="iban">The IBAN to validate.</param>
		/// <returns>true if the IBAN is valid, or false otherwise</returns>
		public IbanValidationResult Validate(string iban)
		{
			var countryCode = iban.Substring(0, 2);
			IbanRegionDefinition definition;
			_definitions.TryGetValue(countryCode, out definition);

			return Validate(iban, definition);
		}

		/// <summary>
		/// Validates the IBAN according to the country specific definition.
		/// </summary>
		/// <param name="iban">The IBAN to validate.</param>
		/// <param name="definition">The country specific definition, or null if no definition was found.</param>
		/// <returns>true if the IBAN is valid, or false otherwise</returns>
		protected abstract IbanValidationResult Validate(string iban, IbanRegionDefinition definition);
	}
}
