using System;
using System.Collections.Generic;
using IbanNet.Validation.Rules;

namespace IbanNet.Validation
{
	/// <summary>
	/// Resolves validation rules by validation method.
	/// </summary>
	internal class DefaultValidationRuleResolver : IValidationRuleResolver
	{
		private readonly IbanValidatorOptions _options;
		private readonly IStructureValidationFactory _structureValidationFactory;

		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultValidationRuleResolver"/>.
		/// </summary>
		public DefaultValidationRuleResolver(IbanValidatorOptions options)
		{
			_options = options ?? throw new ArgumentNullException(nameof(options));

			_structureValidationFactory = new CachedStructureValidationFactory(
				new CompositeStructureValidationFactory(
					(options.Registry ?? throw new ArgumentException(Resources.ArgumentException_Registry_is_required, nameof(options))).Providers
				)
			);
		}

		/// <inheritdoc />
		public IEnumerable<IIbanValidationRule> GetRules()
		{
			yield return new NotEmptyRule();
			yield return new HasCountryCodeRule();
			yield return new NoIllegalCharactersRule();
			yield return new HasIbanChecksumRule();
			yield return new IsValidCountryCodeRule(_options.Registry);
			yield return new IsValidLengthRule();

			if (_options.Method == ValidationMethod.Strict)
			{
				yield return new IsMatchingStructureRule(_structureValidationFactory);
			}

			yield return new Mod97Rule();

			if (_options.Rules == null)
			{
				yield break;
			}

			foreach (IIbanValidationRule rule in _options.Rules)
			{
				yield return rule;
			}
		}
	}
}
