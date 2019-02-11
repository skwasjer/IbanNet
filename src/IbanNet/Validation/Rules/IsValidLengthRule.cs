using System.Collections.Generic;
using IbanNet.Registry;

namespace IbanNet.Validation.Rules
{
	/// <summary>
	/// Asserts that the IBAN has the correct length as defined for its country.
	/// </summary>
	internal class IsValidLengthRule : CountrySpecificRule
	{
		public IsValidLengthRule(IReadOnlyDictionary<string, CountryInfo> definitions) : base(definitions)
		{
		}

		/// <summary>
		/// Validates the IBAN according to the country specific definition.
		/// </summary>
		/// <param name="iban">The IBAN to validate.</param>
		/// <param name="definition">The country specific definition.</param>
		/// <returns>true if the IBAN is valid, or false otherwise</returns>
		protected override IbanValidationResult Validate(string iban, CountryInfo definition)
		{
			return iban.Length == definition.Iban.Length
				? IbanValidationResult.Valid
				: IbanValidationResult.InvalidLength;
		}
	}
}
