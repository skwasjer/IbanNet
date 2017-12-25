using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using IbanNet.ValidationRules;

namespace IbanNet
{
	/// <summary>
	/// Represents the default IBAN validator.
	/// </summary>
	public class IbanValidator : IIbanValidator
	{
		private Collection<IIbanValidationRule> _rules;
		private IbanDefinitions _definitions;

		private IEnumerable<IIbanValidationRule> Rules => _rules ?? (_rules = new Collection<IIbanValidationRule>
		{
			new NotNullRule(),
			new NoIllegalCharactersRule(),
			new HasCountryCodeRule(),
			new HasIbanChecksumRule(),
			new IsValidCountryCodeRule(Definitions),
			new IsValidLengthRule(Definitions),
			new IsMatchingStructureRule(Definitions),
			new Mod97Rule(),

			// The last rule will always pass, but serves as a marker.
			new FinalRule()
		});

		/// <summary>
		/// Gets all the definitions the <see cref="IbanValidator"/> supports.
		/// </summary>
		private IReadOnlyDictionary<string, IbanRegionDefinition> Definitions => _definitions ?? (_definitions = new IbanDefinitions());

		/// <summary>
		/// Gets the supported regions.
		/// </summary>
		public IEnumerable<IbanRegionDefinition> SupportedRegions => Definitions.Values;

		/// <summary>
		/// Validates the specified IBAN for correctness.
		/// </summary>
		/// <param name="iban">The IBAN value.</param>
		/// <returns>a validation result, indicating if the IBAN is valid or not</returns>
		public IbanValidationResult Validate(string iban)
		{
			var normalizedIban = Iban.Normalize(iban);

			var testedRules = Rules.TakeUntil(rule => rule.Validate(normalizedIban) == false);
			var lastTestedRule = testedRules.Last();

			// If we hit the final rule all rules passed.
			return lastTestedRule is FinalRule
				? IbanValidationResult.Valid
				: lastTestedRule.InvalidResult;
		}

		/// <summary>
		/// A marker rule indicating all previous rule tests have passed.
		/// </summary>
		private class FinalRule : IIbanValidationRule
		{
			public IbanValidationResult InvalidResult
			{
				get { throw new NotSupportedException(); }
			}

			public bool Validate(string iban)
			{
				return true;
			}
		}
	}
}
