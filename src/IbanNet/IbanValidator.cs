using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly Lazy<IReadOnlyCollection<CountryInfo>> _registry;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Collection<IIbanValidationRule> _rules;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly object _lockObject = new object();
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly IStructureValidationFactory _structureValidationFactory;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Dictionary<string, CountryInfo> _structures;

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
			_rules = new Collection<IIbanValidationRule>
			{
				new NotNullRule(),
				new NoIllegalCharactersRule(),
				new HasCountryCodeRule(),
				new HasIbanChecksumRule(),
				new IsValidCountryCodeRule(),
				new IsValidLengthRule(),
				new IsMatchingStructureRule(_structureValidationFactory),
				new Mod97Rule()
			};
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
		public ValidationResult Validate(string iban)
		{
			InitRegistry();

			string normalizedIban = Iban.Normalize(iban);
			var context = new ValidationContext
			{
				Value = normalizedIban,
				Result = IbanValidationResult.Valid,
				Country = GetMatchingCountry(normalizedIban)
			};

			foreach (IIbanValidationRule rule in _rules)
			{
				rule.Validate(context);
				if (context.Result != IbanValidationResult.Valid)
				{
					break;
				}
			}
			
			return new ValidationResult
			{
				Value = normalizedIban?.ToUpperInvariant(),
				Result = context.Result,
				Country = context.Country
			};
		}

		private void InitRegistry()
		{
			if (_structures != null)
			{
				return;
			}

			lock (_lockObject)
			{
				_structures = _structures ?? _registry.Value
					.ToDictionary(
						kvp => kvp.TwoLetterISORegionName,
						kvp => kvp
					);
			}
		}

		private CountryInfo GetMatchingCountry(string iban)
		{
			if (iban == null || iban.Length < 2)
			{
				return null;
			}

			string countryCode = iban.Substring(0, 2).ToUpperInvariant();
			_structures.TryGetValue(countryCode, out CountryInfo matchedCountry);
			return matchedCountry;
		}
	}
}
