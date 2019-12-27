using System;
using System.Collections.Generic;
using IbanNet.Registry;
using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules
{
	/// <summary>
	/// Asserts that the IBAN has a valid and known country code.
	/// </summary>
	internal sealed class IsValidCountryCodeRule : IIbanValidationRule
	{
		private readonly IReadOnlyDictionary<string, IbanCountry> _ibanRegistry;

		public IsValidCountryCodeRule(IReadOnlyDictionary<string, IbanCountry> ibanRegistry)
		{
			_ibanRegistry = ibanRegistry ?? throw new ArgumentNullException(nameof(ibanRegistry));
		}

		/// <inheritdoc />
		public ValidationRuleResult Validate(ValidationRuleContext context)
		{
			IbanCountry? country = GetMatchingCountry(context.Value);
			if (country is null)
			{
				return new UnknownCountryCodeResult();
			}

			context.Country = country;
			return ValidationRuleResult.Success;
		}

		private IbanCountry? GetMatchingCountry(string iban)
		{
			string? countryCode = GetCountryCode(iban);
			if (countryCode == null)
			{
				return null;
			}

			return _ibanRegistry.TryGetValue(countryCode, out IbanCountry country) ? country : null;
		}

		private static string? GetCountryCode(string value)
		{
			return value.Length < 2
				? null
				: new string(new[]
				{
					char.ToUpperInvariant(value[0]),
					char.ToUpperInvariant(value[1])
				});
		}
	}
}
