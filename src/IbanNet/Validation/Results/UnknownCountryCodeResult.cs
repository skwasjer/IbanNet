namespace IbanNet.Validation.Results;

/// <summary>
/// The result returned when the country code is unknown/not supported.
/// </summary>
public class UnknownCountryCodeResult : ErrorResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnknownCountryCodeResult" /> class.
    /// </summary>
    public UnknownCountryCodeResult()
        : base(Resources.UnknownCountryCodeResult)
    {
    }
}