﻿using System.Diagnostics;
using IbanNet.Registry;
using IbanNet.Validation;
using IbanNet.Validation.Results;
using IbanNet.Validation.Rules;

namespace IbanNet
{
    /// <summary>
    /// Represents the default IBAN validator.
    /// </summary>
    public sealed class IbanValidator : IIbanValidator
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly List<IIbanValidationRule> _rules;

        /// <summary>
        /// Initializes a new instance of the <see cref="IbanValidator" /> class.
        /// </summary>
        public IbanValidator()
            : this(
                new IbanValidatorOptions()
            )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IbanValidator" /> class with specified options.
        /// </summary>
        /// <param name="options">The validator options.</param>
        public IbanValidator(IbanValidatorOptions options)
            : this(
                options ?? throw new ArgumentNullException(nameof(options)),
                new DefaultValidationRuleResolver(options)
            )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IbanValidator" /> class with specified options.
        /// </summary>
        /// <param name="options">The validator options.</param>
        /// <param name="validationRuleResolver">The validation rule resolver.</param>
        // ReSharper disable once MemberCanBePrivate.Global
        internal IbanValidator(IbanValidatorOptions options, IValidationRuleResolver validationRuleResolver)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
            if (validationRuleResolver is null)
            {
                throw new ArgumentNullException(nameof(validationRuleResolver));
            }

            if (options.Registry is null)
            {
                throw new ArgumentException(Resources.ArgumentException_Registry_is_required, nameof(options));
            }

            SupportedCountries = options.Registry;
            _rules = validationRuleResolver.GetRules().ToList();
        }

        /// <summary>
        /// Gets the validator options.
        /// </summary>
        /// <remarks>The instance members should not be set/modified after creating the <see cref="IbanValidator" />.</remarks>
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
            var context = new ValidationRuleContext(iban ?? string.Empty);
            ErrorResult? error = null;

            foreach (IIbanValidationRule rule in _rules)
            {
                try
                {
                    error = rule.Validate(context) as ErrorResult;
                }
                catch (Exception ex)
                {
                    error = new ExceptionResult(ex);
                }

                if (error is not null)
                {
                    break;
                }
            }

            return new ValidationResult
            {
                AttemptedValue = iban,
                Country = context.Country,
                Error = error
            };
        }
    }
}
