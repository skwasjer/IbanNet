namespace IbanNet.Registry.Patterns
{
    /// <summary>
    /// Defines the ASCII category of a character.
    /// </summary>
    [Flags]
#pragma warning disable S2342 // Enumeration types should comply with a naming convention - justification: breaking change
    public enum AsciiCategory
#pragma warning restore S2342
    {
        /// <summary>
        /// Other ASCII category.
        /// </summary>
#pragma warning disable CA1008 // Enums should have zero value - renaming to None is a breaking change
#pragma warning disable S2346 // Flags enumerations zero-value members should be named "None"
        Other = 0,
#pragma warning restore S2346 // Flags enumerations zero-value members should be named "None"
#pragma warning restore CA1008 // Enums should have zero value

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
