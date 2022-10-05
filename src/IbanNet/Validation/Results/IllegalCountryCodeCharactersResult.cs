namespace IbanNet.Validation.Results
{
    /// <summary>
    /// The result returned when the IBAN contains illegal characters in the country code.
    /// </summary>
    public class IllegalCountryCodeCharactersResult
        : IllegalCharactersResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IllegalCountryCodeCharactersResult" /> class.
        /// </summary>
        /// <param name="position">The position of the illegal character.</param>
        public IllegalCountryCodeCharactersResult(int position)
            : base(Resources.IllegalCountryCodeCharactersResult, position)
        {
        }
    }
}
