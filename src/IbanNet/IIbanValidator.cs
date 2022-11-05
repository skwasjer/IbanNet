namespace IbanNet;

/// <summary>
/// Describes a validator for IBAN.
/// </summary>
public interface IIbanValidator
{
    /// <summary>
    /// Validates the specified IBAN for correctness.
    /// </summary>
    /// <param name="iban">The IBAN value to validate.</param>
    /// <returns>a validation result, indicating if the IBAN is valid or not</returns>
    ValidationResult Validate(string? iban);
}