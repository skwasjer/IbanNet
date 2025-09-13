using IbanNet.Registry;

namespace IbanNet.Validation.Results;

internal record CountryResolvedResult(IbanCountry Country) : ValidationRuleResult;
