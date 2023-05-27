namespace IbanNet;

/// <summary>
/// Formats to represent IBAN's in.
/// </summary>
public enum IbanFormat
{
    /// <summary>
    /// Format in human readable format. (eg.: NL91 ABNA 0417 1643 00)
    /// </summary>
    Print,

    /// <summary>
    /// Format in electronic format. (eg.: NL91ABNA0417164300)
    /// </summary>
    Electronic,

    /// <summary>
    /// Format in obfuscated format. (eg.: XXXXXXXXXXXXXXXX4300)
    /// </summary>
    Obfuscated
}
