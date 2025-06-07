using IbanNet.Registry;
using IbanNet.Validation.Rules;

namespace IbanNet.Validation;

/// <summary>
/// Resolves all validation rules, built-in first, followed by custom rules.
/// </summary>
internal class DefaultValidationRuleResolver : IValidationRuleResolver
{
    private readonly IIbanRegistry _registry;
    private readonly IEnumerable<IIbanValidationRule> _customRules;

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultValidationRuleResolver" />.
    /// </summary>
    public DefaultValidationRuleResolver
    (
        IIbanRegistry registry,
        IEnumerable<IIbanValidationRule>? customRules = null
    )
    {
        _registry = registry ?? throw new ArgumentNullException(nameof(registry));
        _customRules = customRules ?? [];
    }

    /// <inheritdoc />
    public IEnumerable<IIbanValidationRule> GetRules()
    {
        yield return new NotEmptyRule();
        yield return new HasCountryCodeRule();
        yield return new NoIllegalCharactersRule();
        yield return new HasIbanChecksumRule();
        yield return new IsValidCountryCodeRule(_registry);
        yield return new IsValidLengthRule();
        yield return new IsMatchingStructureRule();

        yield return new Mod97Rule();

        foreach (IIbanValidationRule rule in _customRules)
        {
            yield return rule;
        }
    }
}
