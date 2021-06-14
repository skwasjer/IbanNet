using IbanNet.Registry;
using IbanNet.Validation.Results;

namespace IbanNet
{
    /// <summary>
    /// Represents the validator result.
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// Gets whether validation is successful.
        /// </summary>
        public bool IsValid => Error is null;

        /// <summary>
        /// Gets the IBAN value for which validation was attempted.
        /// </summary>
        public string? AttemptedValue { get; init; }

        /// <summary>
        /// Gets the country info that matches the iban, if any.
        /// </summary>
        public IbanCountry? Country { get; init; }

        /// <summary>
        /// Gets the error that occurred, if any.
        /// </summary>
        public ErrorResult? Error { get; init; }
    }
}
