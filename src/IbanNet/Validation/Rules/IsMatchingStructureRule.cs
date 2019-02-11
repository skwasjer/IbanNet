using System;
using System.Collections.Generic;
using IbanNet.Registry;

namespace IbanNet.Validation.Rules
{
	/// <summary>
	/// Asserts that the IBAN is matching the structure defined for a specific country.
	/// </summary>
	internal class IsMatchingStructureRule : CountrySpecificRule
	{
		private readonly IStructureValidationFactory _structureValidationFactory;

		public IsMatchingStructureRule(IStructureValidationFactory structureValidationFactory, IReadOnlyDictionary<string, CountryInfo> definitions) : base(definitions)
		{
			_structureValidationFactory = structureValidationFactory ?? throw new ArgumentNullException(nameof(structureValidationFactory));
		}

		/// <summary>
		/// Validates the IBAN according to the country specific definition.
		/// </summary>
		/// <param name="iban">The IBAN to validate.</param>
		/// <param name="definition">The country specific definition, or null if no definition was found.</param>
		/// <returns>true if the IBAN is valid, or false otherwise</returns>
		protected override IbanValidationResult Validate(string iban, CountryInfo definition)
		{
			IStructureValidator validator = _structureValidationFactory.CreateValidator(definition, definition.Iban.Structure);
			return validator.Validate(iban)
				? IbanValidationResult.Valid
				: IbanValidationResult.InvalidStructure;
		}
	}
}