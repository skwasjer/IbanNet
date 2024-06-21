using System.Globalization;

namespace IbanNet.CheckDigits.Calculators;

/// <summary>
/// Exception that is thrown when an unexpected token/character is encountered while computing check digits.
/// </summary>
[Serializable]
public class InvalidTokenException : InvalidOperationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidTokenException" />.
    /// </summary>
    public InvalidTokenException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidTokenException" /> using specified message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public InvalidTokenException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidTokenException" /> class using specified message and inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public InvalidTokenException(string message, Exception? innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidTokenException" /> using specified <paramref name="position" /> and the character that was not expected.
    /// </summary>
    /// <param name="position">The position in the string/char buffer where the unexpected character is located.</param>
    /// <param name="unexpectedChar">The character that was not expected.</param>
    public InvalidTokenException(int position, char unexpectedChar)
        : this(string.Format(
            CultureInfo.CurrentCulture,
            Resources.InvalidTokenException_Expected_alphanumeric_character_at_position_0_but_found_1,
            position,
            unexpectedChar)
        )
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidTokenException" /> with serialized data.
    /// </summary>
    /// <param name="info">The object that holds the serialized data.</param>
    /// <param name="context">The contextual information about the source or destination.</param>
#if NET8_0_OR_GREATER
#pragma warning disable CA1041
        [Obsolete(DiagnosticId = "SYSLIB0051")]
#pragma warning restore CA1041
#endif
    protected InvalidTokenException
    (
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {
    }
}
