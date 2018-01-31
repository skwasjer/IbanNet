using System.Collections.Generic;
using System.Collections.ObjectModel;
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
			new Mod97Rule()
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

			var validationResult = IbanValidationResult.Valid;
			foreach (var rule in Rules)
			{
				validationResult = rule.Validate(normalizedIban);
				if (validationResult != IbanValidationResult.Valid)
				{
					break;
				}
			}
			return validationResult;
		}
	}
}
