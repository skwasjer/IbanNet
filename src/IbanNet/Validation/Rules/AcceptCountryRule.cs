using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules;

/// <summary>
/// A validation rule that accepts only specific countries.
/// The rule can be used if your use case needs a limitation on countries to allow.
/// <para>Note: this rule is mutually exclusive with the <see cref="RejectCountryRule" /> rule.</para>
/// </summary>
/// <remarks>Returns <see cref="CountryNotAcceptedResult" /> on validation failures.</remarks>
public class AcceptCountryRule : LimitCountryRule
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AcceptCountryRule" /> using specified <paramref name="acceptedCountryCodes" />.
    /// </summary>
    /// <param name="acceptedCountryCodes">An enumerable of accepted country codes. (2 letter ISO region name)</param>
    public AcceptCountryRule(IEnumerable<string> acceptedCountryCodes)
        : base(acceptedCountryCodes, nameof(acceptedCountryCodes), true)
    {
    }
}
