namespace IbanNet.ValidationRules
{
	/// <summary>
	/// Asserts that the IBAN is matching the structure defined for a specific country.
	/// </summary>
	internal class IsMatchingStructure : CountrySpecificRule
	{
		public IsMatchingStructure(IbanDefinitions definitions) : base(definitions)
		{
		}

		/// <summary>
		/// The validation result to use when this rule is not valid.
		/// </summary>
		public override IbanValidationResult InvalidResult { get; } = IbanValidationResult.InvalidStructure;

		/// <summary>
		/// Validates the IBAN according to the country specific definition.
		/// </summary>
		/// <param name="iban">The IBAN to validate.</param>
		/// <param name="definition">The country specific definition, or null if no definition was found.</param>
		/// <returns>true if the IBAN is valid, or false otherwise</returns>
		protected override bool Validate(string iban, IbanDefinition definition)
		{
			return definition.StructureTest.IsMatch(iban);
		}		
	}
}
