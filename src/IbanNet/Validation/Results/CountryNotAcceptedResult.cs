using System.Globalization;
using IbanNet.Registry;
using IbanNet.Validation.Rules;

namespace IbanNet.Validation.Results;

/// <summary>
/// The result returned when the IBAN passed validation, but is explicitly rejected because it failed to pass a <see cref="AcceptCountryRule" /> or <see cref="RejectCountryRule" /> rule which was added to the validation pipeline.
/// </summary>
public record CountryNotAcceptedResult : ErrorResult
{
    /// <summary>
    /// The result returned when the IBAN is not accepted because of restrictions by country.
    /// </summary>
    /// <param name="country">The country that was rejected.</param>
    public CountryNotAcceptedResult(IbanCountry country)
        : base(string.Format(CultureInfo.CurrentCulture, Resources.CountryNotAcceptedResult_Bank_account_numbers_from_country_0_are_not_accepted, country?.DisplayName))
    {
        if (country is null)
        {
            throw new ArgumentNullException(nameof(country));
        }
    }
}
