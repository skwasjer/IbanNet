using IbanNet.CheckDigits;
using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules;

/// <summary>
/// Asserts that the check digits are valid.
/// </summary>
internal sealed class Mod97Rule : IIbanValidationRule
{
    private const int ExpectedCheckDigit = 1;

    /// <inheritdoc />
    public ValidationRuleResult Validate(ValidationRuleContext context)
    {
        return Mod9710.Compute(context.Value) == ExpectedCheckDigit
            ? ValidationRuleResult.Success
            : new InvalidCheckDigitsResult();
    }
}
