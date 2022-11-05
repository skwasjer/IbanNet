using IbanNet.Validation.Rules;

namespace IbanNet.Validation;

/// <summary>
/// Resolves validation rules.
/// </summary>
internal interface IValidationRuleResolver
{
    /// <summary>
    /// Gets the validation rules.
    /// </summary>
    /// <returns>An enumerable with the validation rules to be used for validation.</returns>
    IEnumerable<IIbanValidationRule> GetRules();
}