using IbanNet.CheckDigits.Calculators;
using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules;

/// <summary>
/// Asserts that the check digits are valid.
/// </summary>
internal sealed class Mod97Rule : IIbanValidationRule
{
    private const int ExpectedCheckDigit = 1;

    private readonly ICheckDigitsCalculator _checkDigitsCalculator;

    public Mod97Rule()
        : this(new Mod97CheckDigitsCalculator())
    {
    }

    internal Mod97Rule(ICheckDigitsCalculator calculator)
    {
        _checkDigitsCalculator = calculator ?? throw new ArgumentNullException(nameof(calculator));
    }

    /// <inheritdoc />
    public ValidationRuleResult Validate(ValidationRuleContext context)
    {
        string iban = context.Value;
        int length = iban.Length;
        char[] buffer = new char[length];
        // Reorder (first 4 chars at end).
        iban.CopyTo(4, buffer, 0, length - 4);
        iban.CopyTo(0, buffer, length - 4, 4);

        return _checkDigitsCalculator.Compute(buffer) == ExpectedCheckDigit
            ? ValidationRuleResult.Success
            : new InvalidCheckDigitsResult();
    }
}