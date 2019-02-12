using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using IbanNet.Registry;
using IbanNet.Validation;
using IbanNet.Validation.Rules;

namespace IbanNet
{
	/// <summary>
	/// Represents the default IBAN validator.
	/// </summary>
	public class IbanValidator : IIbanValidator
	{
		private readonly Lazy<IReadOnlyCollection<CountryInfo>> _registry;
		private Collection<IIbanValidationRule> _rules;
		private readonly object _lockObject = new object();
		private readonly IStructureValidationFactory _structureValidationFactory;

		/// <summary>
		/// Initializes a new instance of the <see cref="IbanValidator"/> class.
		/// </summary>
		public IbanValidator()
			: this(new Lazy<IReadOnlyCollection<CountryInfo>>(() => new IbanRegistry(), LazyThreadSafetyMode.ExecutionAndPublication))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="IbanValidator"/> class with specified registry.
		/// </summary>
		/// <param name="registry">The IBAN registry containing IBAN/BBAN/SEPA information per country.</param>
		// ReSharper disable once MemberCanBePrivate.Global
		public IbanValidator(Lazy<IReadOnlyCollection<CountryInfo>> registry)
		{
			_registry = registry ?? throw new ArgumentNullException(nameof(registry));

			_structureValidationFactory = new CachedStructureValidationFactory(new SwiftStructureValidationFactory());
		}

		private ICollection<IIbanValidationRule> Rules
		{
			get
			{
				if (_rules != null)
				{
					return _rules;
				}

				lock (_lockObject)
				{
					Dictionary<string, CountryInfo> structures = _registry.Value
						.ToDictionary(
							kvp => kvp.TwoLetterISORegionName,
							kvp => kvp
						);
					_rules = _rules ?? new Collection<IIbanValidationRule>
					{
						new NotNullRule(),
						new NoIllegalCharactersRule(),
						new HasCountryCodeRule(),
						new HasIbanChecksumRule(),
						new IsValidCountryCodeRule(structures),
						new IsValidLengthRule(structures),
						new IsMatchingStructureRule(_structureValidationFactory, structures),
						new Mod97Rule()
					};
				}

				return _rules;
			}
		}
		
		/// <summary>
		/// Gets the supported countries.
		/// </summary>
		public IEnumerable<CountryInfo> SupportedCountries => _registry.Value;

		/// <summary>
		/// Validates the specified IBAN for correctness.
		/// </summary>
		/// <param name="iban">The IBAN value.</param>
		/// <returns>a validation result, indicating if the IBAN is valid or not</returns>
		public IbanValidationResult Validate(string iban)
		{
			string normalizedIban = Iban.Normalize(iban);

			IbanValidationResult validationResult = IbanValidationResult.Valid;
			foreach (IIbanValidationRule rule in Rules)
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
