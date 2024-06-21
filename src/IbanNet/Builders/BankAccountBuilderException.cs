namespace IbanNet.Builders;

/// <summary>
/// The exception that is thrown when building a bank account number fails.
/// </summary>
[Serializable]
public class BankAccountBuilderException : InvalidOperationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BankAccountBuilderException" />.
    /// </summary>
    public BankAccountBuilderException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BankAccountBuilderException" /> using specified message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public BankAccountBuilderException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BankAccountBuilderException" /> class using specified message and inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public BankAccountBuilderException(string message, Exception? innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BankAccountBuilderException" /> with serialized data.
    /// </summary>
    /// <param name="info">The object that holds the serialized data.</param>
    /// <param name="context">The contextual information about the source or destination.</param>
#if NET8_0_OR_GREATER
#pragma warning disable CA1041
    [Obsolete(DiagnosticId = "SYSLIB0051")]
#pragma warning restore CA1041
#endif
    protected BankAccountBuilderException
    (
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {
    }
}
