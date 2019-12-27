using System.Collections.Generic;
using IbanNet.Registry;
using IbanNet.Validation.Rules;

namespace IbanNet.Validation.Methods
{
	/// <summary>
	/// Strict validation consists of all built-in IBAN validation rules.
	/// </summary>
	public class StrictValidation : LooseValidation
	{
		internal override IEnumerable<IIbanValidationRule> GetRules(IReadOnlyDictionary<string, CountryInfo> ibanRegistry)
		{
			foreach (IIbanValidationRule rule in base.GetRules(ibanRegistry))
			{
				// Inject structure rule before mod 97.
				if (rule is Mod97Rule)
				{
					var structureValidationFactory = new CachedStructureValidationFactory(new SwiftStructureValidationFactory());
					yield return new IsMatchingStructureRule(structureValidationFactory);
				}

				yield return rule;
			}
		}
	}
}
