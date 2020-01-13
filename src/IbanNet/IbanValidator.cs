using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using IbanNet.Extensions;
using IbanNet.Registry;
using IbanNet.Validation;
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
			: this(
				new IbanValidatorOptions()
			)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="IbanValidator"/> class with specified options.
		/// </summary>
		/// <param name="options">The validator options.</param>
		public IbanValidator(IbanValidatorOptions options)
			: this(
				options,
				new DefaultValidationRuleResolverFactory().CreateRuleResolver(options)
			)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="IbanValidator"/> class with specified options.
		/// </summary>
		/// <param name="options">The validator options.</param>
		/// <param name="validationRuleResolver">The validation rule resolver.</param>
		// ReSharper disable once MemberCanBePrivate.Global
		internal IbanValidator(IbanValidatorOptions options, IValidationRuleResolver validationRuleResolver)
		{
			Options = options ?? throw new ArgumentNullException(nameof(options));
			if (options.ValidationMethod is null)
			{
				throw new ArgumentException(Resources.ArgumentException_ValidationMethod_is_required, nameof(options));
			}

			if (options.Registry is null)
			{
				throw new ArgumentException(Resources.ArgumentException_Registry_is_required, nameof(options));
			}

			if (validationRuleResolver is null)
			{
				throw new ArgumentNullException(nameof(validationRuleResolver));
			}

			SupportedCountries = options.Registry;
			_rules = validationRuleResolver.GetRules(options.ValidationMethod, options.Registry).ToList();
		}

		/// <summary>
		/// Gets the validator options.
		/// </summary>
		/// <remarks>The instance members should not be set/modified after creating the <see cref="IbanValidator"/>.</remarks>
		public IbanValidatorOptions Options { get; }

		/// <summary>
		/// Gets the supported countries.
		/// </summary>
		public IIbanRegistry SupportedCountries { get; }

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
				AttemptedValue = normalizedIban?.ToUpperInvariant()
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
