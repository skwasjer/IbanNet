using System.Diagnostics;
using System.Runtime.InteropServices;
using IbanNet.Registry;
using IbanNet.Validation;
using IbanNet.Validation.Results;
using IbanNet.Validation.Rules;

namespace IbanNet;

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
        : this(IbanRegistry.Default)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IbanValidator" /> class using specified <paramref name="registry" />.
    /// </summary>
    /// <param name="registry">The IBAN registry to use.</param>
    public IbanValidator(IIbanRegistry registry)
        : this(new IbanValidatorOptions { Registry = registry ?? throw new ArgumentNullException(nameof(registry)) })
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IbanValidator" /> class using specified <paramref name="registry" /> and <paramref name="rules" />.
    /// </summary>
    /// <param name="registry">The IBAN registry to use.</param>
    /// <param name="rules">A list of additional (custom) rules to run after the built-in validation.</param>
    public IbanValidator(IIbanRegistry registry, IEnumerable<IIbanValidationRule> rules)
        : this(new IbanValidatorOptions
        {
            Registry = registry ?? throw new ArgumentNullException(nameof(registry)),
            // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
            Rules = rules?.ToList() ?? throw new ArgumentNullException(nameof(rules))
        })
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IbanValidator" /> class with specified options.
    /// </summary>
    /// <param name="options">The validator options.</param>
    public IbanValidator(IbanValidatorOptions options)
        : this(
            options ?? throw new ArgumentNullException(nameof(options)),
            new DefaultValidationRuleResolver(options.Registry, options.Rules)
        )
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IbanValidator" /> class with specified options.
    /// </summary>
    /// <param name="options">The validator options.</param>
    /// <param name="validationRuleResolver">The validation rule resolver.</param>
    internal IbanValidator(IbanValidatorOptions options, IValidationRuleResolver validationRuleResolver)
    {
        Options = options ?? throw new ArgumentNullException(nameof(options));
        if (validationRuleResolver is null)
        {
            throw new ArgumentNullException(nameof(validationRuleResolver));
        }

        SupportedCountries = options.Registry ?? throw new ArgumentException(Resources.ArgumentException_Registry_is_required, nameof(options));
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

#if NET6_0_OR_GREATER
        foreach (ref readonly IIbanValidationRule rule in CollectionsMarshal.AsSpan(_rules))
#else
        foreach (IIbanValidationRule rule in _rules)
#endif
        {
            try
            {
                ValidationRuleResult result = rule.Validate(context);
                error = result as ErrorResult;
                if (result is CountryResolvedResult cr)
                {
                    context = context with { Country = cr.Country };
                }
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
