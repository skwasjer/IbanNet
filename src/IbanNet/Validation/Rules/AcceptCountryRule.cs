using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules;

/// <summary>
/// A validation rule that accepts only specific countries.
/// The rule can be used if your use case needs a limitation on countries to allow.
/// </summary>
/// <remarks>Returns <see cref="CountryNotAcceptedResult" /> on validation failures.</remarks>
public class AcceptCountryRule : IIbanValidationRule
{
    private readonly ISet<string> _acceptedCountryCodes;

    /// <summary>
    /// Initializes a new instance of the <see cref="AcceptCountryRule" /> using specified <paramref name="acceptedCountryCodes" />.
    /// </summary>
    /// <param name="acceptedCountryCodes">An enumerable of accepted country codes (2 letter ISO region name)</param>
    public AcceptCountryRule(IEnumerable<string> acceptedCountryCodes)
    {
        if (acceptedCountryCodes is null)
        {
            throw new ArgumentNullException(nameof(acceptedCountryCodes));
        }

        _acceptedCountryCodes = new HashSet<string>(acceptedCountryCodes.Select(cc => cc.ToUpperInvariant()), StringComparer.Ordinal);
        if (_acceptedCountryCodes.Count == 0)
        {
            throw new ArgumentException(Resources.ArgumentException_At_least_one_country_code_must_be_provided, nameof(acceptedCountryCodes));
        }
    }

    /// <inheritdoc />
    public ValidationRuleResult Validate(ValidationRuleContext context)
    {
        return _acceptedCountryCodes.Contains(context.Country!.TwoLetterISORegionName)
            ? ValidationRuleResult.Success
            : new CountryNotAcceptedResult(context.Country);
    }
}
