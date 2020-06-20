namespace IbanNet.Validation
{
    /// <summary>
    /// Describes a validator that validates a specific IBAN.
    /// </summary>
    public interface IStructureValidator
    {
        /// <summary>
        /// Validates the specified <paramref name="iban" />.
        /// </summary>
        /// <param name="iban">The IBAN to validate.</param>
        /// <returns><see langword="true" /> if the IBAN is valid; <see langword="false" /> otherwise.</returns>
        bool Validate(string iban);
    }
}
