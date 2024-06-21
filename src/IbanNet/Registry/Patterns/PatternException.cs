namespace IbanNet.Registry.Patterns;

/// <summary>
/// The exception that is thrown when a pattern is invalid.
/// </summary>
[Serializable]
public class PatternException : FormatException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PatternException" />.
    /// </summary>
    public PatternException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PatternException" /> class using specified message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public PatternException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PatternException" /> class using specified message and inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public PatternException(string message, Exception? innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PatternException" /> with serialized data.
    /// </summary>
    /// <param name="info">The object that holds the serialized data.</param>
    /// <param name="context">The contextual information about the source or destination.</param>
#if NET8_0_OR_GREATER
#pragma warning disable CA1041
    [Obsolete(DiagnosticId = "SYSLIB0051")]
#pragma warning restore CA1041
#endif
    protected PatternException
    (
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {
    }
}
