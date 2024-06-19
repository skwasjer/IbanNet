using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules;

/// <summary>
/// Asserts that the IBAN is a valid QR IBAN.
/// </summary>
internal sealed class QrIbanRule : IIbanValidationRule
{
    private static readonly string[] ValidCountries = ["CH", "LI"];

    /// <inheritdoc />
    public ValidationRuleResult Validate(ValidationRuleContext context)
    {
        string qrIban = context.Value;

        if (qrIban.Length < 9)
        {
            return new InvalidQrIbanResult();
        }

        string countryIdentifier = qrIban.Substring(0, 2);
        string qrIdentifier = qrIban.Substring(4, 5);

        if (int.TryParse(qrIdentifier, out int id))
        {
            if (id >= 30000 && id <= 31999 && ValidCountries.Contains(countryIdentifier.ToUpperInvariant()))
            {
                return ValidationRuleResult.Success;
            }
        }

        return new InvalidQrIbanResult();
    }
}
