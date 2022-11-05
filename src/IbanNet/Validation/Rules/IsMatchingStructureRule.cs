using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules;

/// <summary>
/// Asserts that the IBAN is matching the structure defined for a specific country.
/// </summary>
internal sealed class IsMatchingStructureRule : IIbanValidationRule
{
    /// <inheritdoc />
    public ValidationRuleResult Validate(ValidationRuleContext context)
    {
        if (context.Country is null)
        {
            return new InvalidStructureResult(0);
        }

        return context.Country.Iban.Pattern.IsMatch(context.Value, out int? errorPos)
            ? ValidationRuleResult.Success
            : new InvalidStructureResult(errorPos.Value);
    }
}