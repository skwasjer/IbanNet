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
	public class IbanValidator : IIbanValidator
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly List<IIbanValidationRule> _rules;

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
			Options = options ?? throw new ArgumentNullException(nameof(options));

			if (options.ValidationMethod == null)
			{
				throw new ArgumentException(Resources.ArgumentException_ValidationMethod_is_required, nameof(options));
			}

			if (options.Registry == null)
			{
				throw new ArgumentException(Resources.ArgumentException_Registry_is_required, nameof(options));
			}

			SupportedCountries = new ReadOnlyDictionary<string, CountryInfo>(options.GetRegistry());
			_rules = options.ValidationMethod.GetRules(SupportedCountries).ToList();

			if (options.Rules != null)
			{
				_rules.AddRange(options.Rules);
			}
		}

		/// <summary>
		/// Gets the validator options.
		/// </summary>
		/// <remarks>The instance members should not be set/modified after creating the <see cref="IbanValidator"/>.</remarks>
		public IbanValidatorOptions Options { get; }

		/// <summary>
		/// Gets the supported countries.
		/// </summary>
		public IReadOnlyDictionary<string, CountryInfo> SupportedCountries { get; }

		/// <summary>
		/// Validates the specified IBAN for correctness.
		/// </summary>
		/// <param name="iban">The IBAN value.</param>
		/// <returns>a validation result, indicating if the IBAN is valid or not</returns>
		public ValidationResult Validate(string? iban)
		{
			string? normalizedIban = iban.StripWhitespaceOrNull();
			string valueToValidate = normalizedIban ?? string.Empty;

			var context = new ValidationRuleContext(valueToValidate);
			var validationResult = new ValidationResult
			{
				Value = normalizedIban?.ToUpperInvariant()
			};

			foreach (IIbanValidationRule rule in _rules)
			{
				validationResult.Error = rule.Validate(context) as ErrorResult;
				if (!validationResult.IsValid)
				{
					break;
				}
			}

			validationResult.Country = context.Country;
			return validationResult;
		}
	}
}
