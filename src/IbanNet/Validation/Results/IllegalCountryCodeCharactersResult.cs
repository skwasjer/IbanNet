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
        public IllegalCountryCodeCharactersResult()
            : base(Resources.IllegalCountryCodeCharactersResult)
        {
        }
    }
}
