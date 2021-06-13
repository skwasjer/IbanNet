using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules
{
    /// <summary>
    /// Asserts that the IBAN has the correct length as defined for its country.
    /// </summary>
    internal sealed class IsValidLengthRule : IIbanValidationRule
    {
        /// <inheritdoc />
        public ValidationRuleResult Validate(ValidationRuleContext context)
        {
            return context.Country is not null && context.Value.Length == context.Country.Iban.Length
                ? ValidationRuleResult.Success
                : new InvalidLengthResult();
        }
    }
}
