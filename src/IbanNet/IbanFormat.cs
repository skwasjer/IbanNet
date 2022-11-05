namespace IbanNet;

/// <summary>
/// Formats to represent IBAN's in.
/// </summary>
public enum IbanFormat
{
    /// <summary>
    /// Represent IBAN in human readable format. (ie. NL91 ABNA 0417 1643 00)
    /// </summary>
    Print,

    /// <summary>
    /// Represent IBAN in electronic format. (ie.: NL91ABNA0417164300)
    /// </summary>
    Electronic,

    /// <summary>
    /// Represent IBAN in obfuscated format. (ie.: XXXXXXXXXXXXXXXX4300)
    /// </summary>
    Obfuscated
}
