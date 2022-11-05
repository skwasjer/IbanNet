namespace IbanNet.Validation.Results;

/// <summary>
/// The result returned when the IBAN check digits are incorrect.
/// </summary>
public class InvalidCheckDigitsResult : ErrorResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidCheckDigitsResult" /> class.
    /// </summary>
    public InvalidCheckDigitsResult()
        : base(Resources.InvalidCheckDigitsResult)
    {
    }
}