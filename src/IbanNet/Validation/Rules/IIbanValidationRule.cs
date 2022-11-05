using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules;

/// <summary>
/// Describes a validation rule for IBAN.
/// </summary>
public interface IIbanValidationRule
{
    /// <summary>
    /// Validates the IBAN against this rule.
    /// </summary>
    /// <param name="context">The validation context.</param>
    ValidationRuleResult Validate(ValidationRuleContext context);
}