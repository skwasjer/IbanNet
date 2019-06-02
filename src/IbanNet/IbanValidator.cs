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
	public class IbanValidator : IIbanValidator, ICountryValidationSupport
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly Lazy<IReadOnlyCollection<CountryInfo>> _registry;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly List<IIbanValidationRule> _rules;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly object _lockObject = new object();
		// ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable - Justification: helpful during debug.
		private readonly IStructureValidationFactory _structureValidationFactory;
		private Dictionary<string, CountryInfo> _structures;

		/// <summary>
		/// Initializes a new instance of the <see cref="IbanValidator"/> class.
		/// </summary>
		public IbanValidator()
			: this(new IbanValidatorOptions())
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="IbanValidator"/> class with specified registry.
		/// </summary>
		/// <param name="registry">The IBAN registry containing IBAN/BBAN/SEPA information per country.</param>
		// ReSharper disable once MemberCanBePrivate.Global
		[Obsolete("Use the overload that accepts " + nameof(IbanValidatorOptions) + ".")]
		public IbanValidator(Lazy<IReadOnlyCollection<CountryInfo>> registry)
			: this(new IbanValidatorOptions
			{
				Registry = registry
			})
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="IbanValidator"/> class with specified registry.
		/// </summary>
		/// <param name="options">The IBAN registry containing IBAN/BBAN/SEPA information per country.</param>
		// ReSharper disable once MemberCanBePrivate.Global
		public IbanValidator(IbanValidatorOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException(nameof(options));
			}

			_registry = options.Registry ?? throw new ArgumentException("Provide the registry.", nameof(options));

			_structureValidationFactory = new CachedStructureValidationFactory(new SwiftStructureValidationFactory());
			_rules = new List<IIbanValidationRule>
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

			if (options.Rules != null)
			{
				_rules.AddRange(options.Rules);
			}
		}

		/// <summary>
		/// Gets the supported countries.
		/// </summary>
		// TODO: v4, change to dictionary for faster lookup.
		public IEnumerable<CountryInfo> SupportedCountries => ((ICountryValidationSupport)this).SupportedCountries.Values;

		/// <summary>
		/// Gets the supported countries.
		/// </summary>
		IReadOnlyDictionary<string, CountryInfo> ICountryValidationSupport.SupportedCountries
		{
			get
			{
				InitRegistry();

				return new ReadOnlyDictionary<string, CountryInfo>(_structures);
			}
		}

		/// <summary>
		/// Validates the specified IBAN for correctness.
		/// </summary>
		/// <param name="iban">The IBAN value.</param>
		/// <returns>a validation result, indicating if the IBAN is valid or not</returns>
		public ValidationResult Validate(string iban)
		{
			InitRegistry();

			string normalizedIban = Iban.Normalize(iban);
			var context = new ValidationRuleContext(normalizedIban, GetMatchingCountry(normalizedIban));

			IIbanValidationRule failingRule = null;
			foreach (IIbanValidationRule rule in _rules)
			{
				rule.Validate(context);
				if (context.Result != IbanValidationResult.Valid)
				{
					failingRule = rule;
					break;
				}
			}

			return new ValidationResult
			{
				Value = normalizedIban?.ToUpperInvariant(),
				Result = context.Result,
				Country = context.Country,
				ErrorMessage = context.ErrorMessage,
				Exception = context.Exception,
				ValidationRuleType = failingRule?.GetType()
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
						kvp => kvp.TwoLetterISORegionName
					);
			}
		}

		private CountryInfo GetMatchingCountry(string iban)
		{
			string countryCode = GetCountryCode(iban);
			if (countryCode == null)
			{
				return null;
			}

			_structures.TryGetValue(countryCode, out CountryInfo matchedCountry);
			return matchedCountry;
		}

		private static string GetCountryCode(string value)
		{
			if (value == null || value.Length < 2)
			{
				return null;
			}

			return value.Substring(0, 2).ToUpperInvariant();
		}
	}
}
