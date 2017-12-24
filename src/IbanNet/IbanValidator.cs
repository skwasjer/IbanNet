using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using IbanNet.ValidationRules;

namespace IbanNet
{
	/// <summary>
	/// Represents the default IBAN validator.
	/// </summary>
	public class IbanValidator : IIbanValidator
	{
		private static readonly Lazy<IbanDefinitions> Definitions = new Lazy<IbanDefinitions>(LazyThreadSafetyMode.ExecutionAndPublication);

		/// <summary>
		/// Validates the specified <paramref name="iban"/> for correctness.
		/// </summary>
		/// <param name="iban">The IBAN value.</param>
		/// <returns>a validation result, indicating if the IBAN is valid or not</returns>
		public IbanValidationResult Validate(string iban)
		{
			var normalizedIban = Iban.Normalize(iban);

			var _rules = new Collection<IIbanValidationRule>
			{
				new NotNullRule(),
				new NoIllegalCharactersRule(),
				new HasCountryCodeRule(),
				new HasIbanChecksumRule(),
				new IsValidCountryCodeRule(Definitions.Value),
				new IsValidLengthRule(Definitions.Value),

				// TODO: validate the country specific format.

				// The last rule will always pass, but serves as a marker.
				new FinalRule()
			};

			var testedRules = _rules.TakeUntil(rule => rule.Validate(normalizedIban) == false);
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
