namespace IbanNet.Registry.Patterns;

/// <summary>
/// Defines the ASCII category of a character.
/// </summary>
[Flags]
#pragma warning disable S2342 // Enumeration types should comply with a naming convention - justification: breaking change
#pragma warning disable CA1008
public enum AsciiCategory
#pragma warning restore CA1008
#pragma warning restore S2342
{
    /// <summary>
    /// No ASCII category.
    /// </summary>
    None = 0,

    /// <summary>
    /// No ASCII category.
    /// </summary>
    [Obsolete("Use None instead.", true)]
#pragma warning disable CA1069
    // ReSharper disable once UnusedMember.Global
    Other = 0,
#pragma warning restore CA1069

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
