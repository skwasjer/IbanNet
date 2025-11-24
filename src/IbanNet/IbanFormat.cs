namespace IbanNet;

/// <summary>
/// Formats to represent IBAN's in.
/// </summary>
public enum IbanFormat
{
    /// <summary>
    /// Format in human readable format. (eg.: NL91 ABNA 0417 1643 00)
    /// </summary>
    /// <remarks>
    /// Typically used for displaying IBAN's to end-users in safe environments.
    /// </remarks>
    Print,

    /// <summary>
    /// Format in electronic format. (eg.: NL91ABNA0417164300)
    /// </summary>
    /// <remarks>
    /// Typically used for electronic processing and storage of IBAN's.
    /// </remarks>
    Electronic,

    /// <summary>
    /// Format in obfuscated format. (eg.: XXXXXXXXXXXXXXXX4300)
    /// </summary>
    /// <remarks>
    /// Typically used when displaying IBAN's in user interfaces to prevent exposing sensitive information.
    /// Note that this format still allows for partial identification of the IBAN, both due to the visible last characters and the length.
    /// </remarks>
    Obfuscated,

    /// <summary>
    /// Format is hidden in its entirety. (eg.: ****)
    /// </summary>
    /// <remarks>
    /// Typically used when IBAN's should not be displayed at all, and only an indication that an IBAN exists is needed. (For example in audit logs or activity feeds)
    /// </remarks>
    Hidden
}
