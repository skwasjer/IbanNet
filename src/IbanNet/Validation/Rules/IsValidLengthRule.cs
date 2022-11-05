using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules;

/// <summary>
/// Asserts that the IBAN has the correct length as defined for its country.
/// </summary>
internal sealed class IsValidLengthRule : IIbanValidationRule
{
    /// <inheritdoc />
    public ValidationRuleResult Validate(ValidationRuleContext context)
    {
        int inputLength = context.Value.Length;
        return context.Country is null
            // Must match defined.
         || inputLength != context.Country.Iban.Length
            // Short circuit, in case of faulty country IBAN structure and excessively long input.
         || inputLength > Iban.MaxLength
                ? new InvalidLengthResult()
                : ValidationRuleResult.Success;
    }
}