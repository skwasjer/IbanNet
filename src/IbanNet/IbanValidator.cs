using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using IbanNet.Extensions;
using IbanNet.Registry;
using IbanNet.Validation.Results;
using IbanNet.Validation.Rules;

namespace IbanNet
{
	/// <summary>
	/// Represents the default IBAN validator.
	/// </summary>
	public class IbanValidator : IIbanValidator, ICountryValidationSupport
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly Func<IReadOnlyCollection<CountryInfo>> _registryBuilder;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly List<IIbanValidationRule> _rules;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly object _lockObject = new object();
		private Dictionary<string, CountryInfo>? _structures;

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
		/// <param name="options">The IBAN registry containing IBAN/BBAN/SEPA information per country.</param>
		// ReSharper disable once MemberCanBePrivate.Global
		public IbanValidator(IbanValidatorOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException(nameof(options));
			}

			if (options.ValidationMethod == null)
			{
				throw new ArgumentException(Resources.ArgumentException_ValidationMethod_is_required, nameof(options));
			}

			_registryBuilder = options.Registry ?? throw new ArgumentException(Resources.ArgumentException_Registry_is_required, nameof(options));

			_rules = options.ValidationMethod.GetRules().ToList();

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
		public ValidationResult Validate(string? iban)
		{
			InitRegistry();

			string? normalizedIban = iban.StripWhitespaceOrNull();
			string valueToValidate = normalizedIban ?? string.Empty;
			var context = new ValidationRuleContext(GetMatchingCountry(valueToValidate));

			ValidationRuleResult ruleResult = ValidationRuleResult.Success;
			foreach (IIbanValidationRule rule in _rules)
			{
				ruleResult = rule.Validate(context, valueToValidate);
				if (!Equals(ruleResult, ValidationRuleResult.Success))
				{
					break;
				}
			}

			return new ValidationResult
			{
				Value = normalizedIban?.ToUpperInvariant(),
				Result = ruleResult,
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
				_structures ??= _registryBuilder()
					.ToDictionary(
						kvp => kvp.TwoLetterISORegionName
					);
			}
		}

		private CountryInfo? GetMatchingCountry(string iban)
		{
			string? countryCode = GetCountryCode(iban);
			if (countryCode == null)
			{
				return null;
			}

			// Note: This function can only be called once registry is initialized.
			_structures!.TryGetValue(countryCode, out CountryInfo matchedCountry);
			return matchedCountry;
		}

		private static string? GetCountryCode(string value)
		{
			return value.Length < 2 ? null : value.Substring(0, 2).ToUpperInvariant();
		}
	}
}
