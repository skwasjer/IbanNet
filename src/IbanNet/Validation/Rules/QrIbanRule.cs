using System.Globalization;
using IbanNet.Registry;
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
        IbanCountry? country = context.Country;
        if (country != null && IsValid(country, context.Value.Substring(country.Bank.Position, country.Bank.Length)))
        {
            return ValidationRuleResult.Success;
        }

        return new InvalidQrIbanResult();
    }

    internal static bool IsValid(IbanCountry country, string iidStr)
    {
        return country.TwoLetterISORegionName is "CH" or "LI"
           && int.TryParse(iidStr, NumberStyles.None, NumberFormatInfo.InvariantInfo, out int iid)
           && iid is >= 30000 and <= 31999;
    }
}
