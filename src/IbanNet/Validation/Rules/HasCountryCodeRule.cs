using IbanNet.Extensions;
using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules;

/// <summary>
/// Asserts that the IBAN has a country code but does not check the validity of the country code itself.
/// </summary>
internal sealed class HasCountryCodeRule : IIbanValidationRule
{
    /// <inheritdoc />
    public ValidationRuleResult Validate(ValidationRuleContext context)
    {
        string iban = context.Value;
        // First 2 chars must be a-z or A-Z.
        int pos = -1;
        if (iban.Length < 2 || !iban[++pos].IsAsciiLetter() || !iban[++pos].IsAsciiLetter())
        {
            return new IllegalCountryCodeCharactersResult(iban.Length == 1 ? 1 : pos);
        }

        return ValidationRuleResult.Success;
    }
}