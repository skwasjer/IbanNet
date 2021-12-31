using IbanNet.Validation.Rules;

namespace IbanNet.Validation
{
    /// <summary>
    /// Resolves all validation rules, built-in first, followed by custom rules.
    /// </summary>
    internal class DefaultValidationRuleResolver : IValidationRuleResolver
    {
        private readonly IbanValidatorOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultValidationRuleResolver" />.
        /// </summary>
        public DefaultValidationRuleResolver(IbanValidatorOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <inheritdoc />
        public IEnumerable<IIbanValidationRule> GetRules()
        {
            yield return new NotEmptyRule();
            yield return new HasCountryCodeRule();
            yield return new NoIllegalCharactersRule();
            yield return new HasIbanChecksumRule();
            yield return new IsValidCountryCodeRule(_options.Registry);
            yield return new IsValidLengthRule();
            yield return new IsMatchingStructureRule();

            yield return new Mod97Rule();

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (_options.Rules is null)
            {
                yield break;
            }

            foreach (IIbanValidationRule rule in _options.Rules)
            {
                yield return rule;
            }
        }
    }
}
