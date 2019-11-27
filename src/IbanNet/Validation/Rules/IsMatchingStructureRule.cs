using System;
using IbanNet.Validation.Results;

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
		public ValidationRuleResult Validate(ValidationRuleContext context, string iban)
		{
			if (context.Country is null)
			{
				return new InvalidStructureResult();
			}

			IStructureValidator validator = _structureValidationFactory.CreateValidator(
				context.Country.TwoLetterISORegionName,
				context.Country.Iban.Structure
			);

			return validator.Validate(iban)
				? ValidationRuleResult.Success
				: new InvalidStructureResult();
		}
	}
}
