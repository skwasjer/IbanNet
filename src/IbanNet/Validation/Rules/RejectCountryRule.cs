using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules;

/// <summary>
/// A validation rule that rejects specific countries.
/// The rule can be used if your use case needs a limitation on countries to allow.
/// <para>Note: this rule is mutually exclusive with the <see cref="AcceptCountryRule" /> rule.</para>
/// </summary>
/// <remarks>Returns <see cref="CountryNotAcceptedResult" /> on validation failures.</remarks>
public class RejectCountryRule : LimitCountryRule
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RejectCountryRule" /> using specified <paramref name="rejectedCountryCodes" />.
    /// </summary>
    /// <param name="rejectedCountryCodes">An enumerable of rejected country codes. (2 letter ISO region name)</param>
    public RejectCountryRule(IEnumerable<string> rejectedCountryCodes)
        : base(rejectedCountryCodes, nameof(rejectedCountryCodes), false)
    {
    }
}
