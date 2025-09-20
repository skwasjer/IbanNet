using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules;

/// <summary>
/// A validation rule that accepts only specific countries.
/// The rule can be used if your use case needs a limitation on countries to allow.
/// </summary>
/// <remarks>Returns <see cref="CountryNotAcceptedResult" /> on validation failures.</remarks>
public abstract class LimitCountryRule : IIbanValidationRule
{
    private readonly bool _isAccepted;
    private readonly HashSet<string> _countryCodes;

    /// <summary>
    /// Initializes a new instance of the <see cref="LimitCountryRule" /> using specified <paramref name="countryCodes" />.
    /// </summary>
    /// <param name="countryCodes">An enumerable of accepted country codes (2 letter ISO region name)</param>
    /// <param name="paramName">The parameter name of <paramref name="countryCodes" />of derived type.</param>
    /// <param name="isAccepted"></param>
    protected internal LimitCountryRule(
        IEnumerable<string> countryCodes,
        string paramName,
        bool isAccepted
    )
    {
        if (countryCodes is null)
        {
            throw new ArgumentNullException(paramName);
        }

        _countryCodes = new HashSet<string>(countryCodes.Select(cc => cc.ToUpperInvariant()), StringComparer.Ordinal);
        if (_countryCodes.Count == 0)
        {
            throw new ArgumentException(Resources.ArgumentException_At_least_one_country_code_must_be_provided, paramName);
        }

        _isAccepted = isAccepted;
    }

    /// <inheritdoc />
    public ValidationRuleResult Validate(ValidationRuleContext context)
    {
        bool matchesCountryCode = _countryCodes.Contains(context.Country!.TwoLetterISORegionName);
        return matchesCountryCode == _isAccepted
            ? ValidationRuleResult.Success
            : new CountryNotAcceptedResult(context.Country);
    }
}
