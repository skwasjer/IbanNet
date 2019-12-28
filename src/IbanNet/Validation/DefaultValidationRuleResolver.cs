using System;
using System.Collections.Generic;
using IbanNet.Registry;
using IbanNet.Validation.Methods;
using IbanNet.Validation.Rules;

namespace IbanNet.Validation
{
	/// <summary>
	/// Resolves validation rules by validation method.
	/// </summary>
	public class DefaultValidationRuleResolver : IValidationRuleResolver
	{
		private readonly IStructureValidationFactory _structureValidationFactory;
		private readonly ICollection<IIbanValidationRule>? _customRules;

		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultValidationRuleResolver"/>.
		/// </summary>
		public DefaultValidationRuleResolver(IStructureValidationFactory structureValidationFactory, ICollection<IIbanValidationRule>? customRules)
		{
			_structureValidationFactory = structureValidationFactory ?? throw new ArgumentNullException(nameof(structureValidationFactory));
			_customRules = customRules;
		}

		/// <inheritdoc />
		public IEnumerable<IIbanValidationRule> GetRules(ValidationMethod validationMethod, IDictionary<string, IbanCountry> acceptedCountries)
		{
			yield return new NotEmptyRule();
			yield return new HasCountryCodeRule();
			yield return new NoIllegalCharactersRule();
			yield return new HasIbanChecksumRule();
			yield return new IsValidCountryCodeRule(acceptedCountries);
			yield return new IsValidLengthRule();

			if (validationMethod is StrictValidation)
			{
				yield return new IsMatchingStructureRule(_structureValidationFactory);
			}

			yield return new Mod97Rule();

			if (_customRules == null)
			{
				yield break;
			}

			foreach (IIbanValidationRule rule in _customRules)
			{
				yield return rule;
			}
		}
	}
}
