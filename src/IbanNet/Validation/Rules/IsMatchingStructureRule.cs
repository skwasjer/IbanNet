using System;
using IbanNet.Registry;
using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules
{
    /// <summary>
    /// Asserts that the IBAN is matching the structure defined for a specific country.
    /// </summary>
    internal sealed class IsMatchingStructureRule : IIbanValidationRule
    {
        private readonly IStructureValidationFactory _structureValidationFactory;

        public IsMatchingStructureRule(IStructureValidationFactory structureValidationFactory)
        {
            _structureValidationFactory = structureValidationFactory ?? throw new ArgumentNullException(nameof(structureValidationFactory));
        }

        /// <inheritdoc />
        public ValidationRuleResult Validate(ValidationRuleContext context)
        {
            if (context.Country is null)
            {
                return new InvalidStructureResult();
            }

            IStructureValidator validator;
            Pattern? pattern = context.Country.Iban.Pattern;
            if (pattern is null)
            {
                validator = _structureValidationFactory.CreateValidator(
                    context.Country.TwoLetterISORegionName,
                    context.Country.Iban.Structure
                );
            }
            else
            {
                validator = new StructureValidator(pattern);
            }

            return validator.Validate(context.Value)
                ? ValidationRuleResult.Success
                : new InvalidStructureResult();
        }
    }
}
