namespace IbanNet.Validation.Results
{
    /// <summary>
    /// The result returned when the IBAN contains illegal characters.
    /// </summary>
    public class IllegalCharactersResult : ErrorResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IllegalCharactersResult" /> class.
        /// </summary>
        public IllegalCharactersResult()
            : this(Resources.IllegalCharactersResult)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IllegalCharactersResult" /> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        protected IllegalCharactersResult(string errorMessage)
            : base(errorMessage)
        {
        }
    }
}
