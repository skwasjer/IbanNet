using IbanNet.Registry;

namespace IbanNet.Validation.Results;

internal sealed class CountryResolvedResult(IbanCountry country) : ValidationRuleResult
{
    public IbanCountry Country { get; } = country;
}
