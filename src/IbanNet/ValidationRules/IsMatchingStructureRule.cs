using System.Collections.Generic;

namespace IbanNet.ValidationRules
{
	/// <summary>
	/// Asserts that the IBAN is matching the structure defined for a specific country.
	/// </summary>
	internal class IsMatchingStructureRule : CountrySpecificRule
	{
		public IsMatchingStructureRule(IReadOnlyDictionary<string, IbanRegionDefinition> definitions) : base(definitions)
		{
		}

		/// <summary>
		/// Validates the IBAN according to the country specific definition.
		/// </summary>
		/// <param name="iban">The IBAN to validate.</param>
		/// <param name="definition">The country specific definition, or null if no definition was found.</param>
		/// <returns>true if the IBAN is valid, or false otherwise</returns>
		protected override IbanValidationResult Validate(string iban, IbanRegionDefinition definition)
		{
			return definition.StructureTest.IsMatch(iban)
				? IbanValidationResult.Valid
				: IbanValidationResult.InvalidStructure;
		}		
	}
}
