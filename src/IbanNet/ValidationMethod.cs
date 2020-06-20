namespace IbanNet
{
    /// <summary>
    /// The validation method to use.
    /// </summary>
    public enum ValidationMethod
    {
        /// <summary>
        /// Strict validation consists of all built-in IBAN validation rules.
        /// <para>This is the recommended validation method for user input, imports, etc.</para>
        /// </summary>
        Strict,

        /// <summary>
        /// Loose validation consists of the same built-in IBAN validation rules of the <see cref="Strict" /> method, except that an IBAN is not checked if it is matching the structure, character by character, as defined by the registry.
        /// This does mean that the IBAN could potentially contain certain characters that are officially not allowed, but could
        /// pass all other criteria (including check digit). As such, this method is not recommended when direct user input is involved.
        /// </summary>
        /// <remarks>
        /// Loose validation is around 15%-20% faster than strict validation.
        /// F.ex. a use case is parsing IBANs from a well known source that previously have been validated using strict validation.
        /// </remarks>
        Loose
    }
}
