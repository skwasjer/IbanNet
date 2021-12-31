using System.Globalization;
using IbanNet.Registry;

namespace IbanNet.Validation.Results;

/// <inheritdoc />
public class CountryNotAcceptedResult : ErrorResult
{
    /// <summary>
    /// The result returned when the IBAN is not accepted because of restrictions by country.
    /// </summary>
    /// <param name="country">The country that was rejected.</param>
    public CountryNotAcceptedResult(IbanCountry country)
        : base(string.Format(CultureInfo.CurrentCulture, Resources.CountryNotAcceptedResult_Bank_account_numbers_from_country_0_are_not_accepted, country.DisplayName))
    {
    }
}
