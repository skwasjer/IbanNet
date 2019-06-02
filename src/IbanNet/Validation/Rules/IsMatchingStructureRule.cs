using System;

namespace IbanNet.Validation.Rules
{
	/// <summary>
	/// Asserts that the IBAN is matching the structure defined for a specific country.
	/// </summary>
	internal class IsMatchingStructureRule : IIbanValidationRule
	{
		private readonly IStructureValidationFactory _structureValidationFactory;

		public IsMatchingStructureRule(IStructureValidationFactory structureValidationFactory)
		{
			_structureValidationFactory = structureValidationFactory ?? throw new ArgumentNullException(nameof(structureValidationFactory));
		}

		/// <inheritdoc />
		public void Validate(ValidationRuleContext context)
		{
			IStructureValidator validator = _structureValidationFactory.CreateValidator(
				context.Country.TwoLetterISORegionName,
				context.Country.Iban.Structure
			);

			if (!validator.Validate(context.Value))
			{
				context.Result = IbanValidationResult.InvalidStructure;
			}
		}
	}
}