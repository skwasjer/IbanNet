using IbanNet.Extensions;
using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules;

/// <summary>
/// Asserts that the IBAN is a valid QR IBAN.
/// </summary>
internal sealed class QrIbanRule : IIbanValidationRule
{
    /// <inheritdoc />
    public ValidationRuleResult Validate(ValidationRuleContext context)
    {
        if (Iban.TryParse(context.Value, out Iban? iban) && SwissIbanExtensions.IsQrIban(iban))
        {
            return ValidationRuleResult.Success;
        }

        return new InvalidQrIbanResult();
    }
}
