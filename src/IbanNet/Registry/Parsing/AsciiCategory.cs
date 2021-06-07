namespace IbanNet.Registry.Parsing
{
    /// <summary>
    /// Defines the ASCII category of a character.
    /// </summary>
    public enum AsciiCategory
    {
        /// <summary>
        /// Other ASCII category.
        /// </summary>
        Other = 0,

        /// <summary>
        /// The space character.
        /// </summary>
        Space = 0x1,

        /// <summary>
        /// ASCII digit.
        /// </summary>
        Digit = 0x2,

        /// <summary>
        /// ASCII uppercase letter.
        /// </summary>
        UppercaseLetter = 0x4,

        /// <summary>
        /// ASCII lowercase letter.
        /// </summary>
        LowercaseLetter = 0x8,

        /// <summary>
        /// ASCII letter.
        /// </summary>
        Letter = UppercaseLetter | LowercaseLetter,

        /// <summary>
        /// ASCII letter or digit.
        /// </summary>
        AlphaNumeric = Letter | Digit
    }
}
