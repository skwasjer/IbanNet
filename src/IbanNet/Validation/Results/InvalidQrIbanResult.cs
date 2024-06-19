namespace IbanNet.Validation.Results;

/// <summary>
/// The result returned when the IBAN is not a valid QR IBAN.
/// </summary>
public class InvalidQrIbanResult : ErrorResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidQrIbanResult" /> class.
    /// </summary>
    public InvalidQrIbanResult()
        : base(Resources.InvalidQrIbanResult)
    {
    }
}
