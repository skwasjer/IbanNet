using System.ComponentModel.DataAnnotations;
using System.Globalization;
using IbanNet.Internal;

namespace IbanNet.DataAnnotations;

/// <summary>
/// When applied to a <see cref="string" /> property or parameter, validates that a valid IBAN is provided.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
public sealed class IbanAttribute : ValidationAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IbanAttribute" /> class.
    /// </summary>
    public IbanAttribute()
        : base(Resources.IbanAttribute_Invalid)
    {
    }

    /// <summary>
    /// Gets or sets whether to perform strict validation. When true, the input must strictly match the IBAN format rules.
    /// When false, whitespace is ignored and strict character casing enforcement is disabled (meaning, the user can input in lower and uppercase). This mode is a bit more forgiving when dealing with user-input. However it does require after successful validation, that you parse the user input with <see cref="IIbanParser" /> to normalize/sanitize the input and to be able to format the IBAN in correct electronic format.
    ///
    /// <para>Default is <see langword="true" />. (this may change in future major release)</para>
    /// </summary>
    public bool Strict { get; init; } = true;

    /// <inheritdoc />
    protected override System.ComponentModel.DataAnnotations.ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return System.ComponentModel.DataAnnotations.ValidationResult.Success;
        }

        if (value is not string strValue)
        {
            return base.IsValid(value, validationContext);
        }

        IIbanValidator ibanValidator = GetValidator(validationContext);
        ValidationResult result = ibanValidator.Validate(
            Strict
                ? strValue
                : InputNormalization.NormalizeOrNull(strValue)
        );
        if (result.IsValid)
        {
            return System.ComponentModel.DataAnnotations.ValidationResult.Success;
        }

        validationContext.Items.Add("Error", result.Error);

        IEnumerable<string>? memberNames = null;
        if (validationContext.MemberName is not null)
        {
            memberNames = new[] { validationContext.MemberName };
        }

        return new System.ComponentModel.DataAnnotations.ValidationResult(FormatErrorMessage(validationContext.DisplayName), memberNames);
    }

    /// <inheritdoc />
    public override bool RequiresValidationContext => true;

    /// <summary>
    /// Gets the validator from IoC container.
    /// </summary>
    private static IIbanValidator GetValidator(ValidationContext? serviceProvider)
    {
        var ibanValidator = (IIbanValidator?)serviceProvider?.GetService(typeof(IIbanValidator));
        if (ibanValidator is null)
        {
            throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.IbanAttribute_ValidatorMissing, nameof(IIbanValidator)));
        }

        return ibanValidator;
    }
}
