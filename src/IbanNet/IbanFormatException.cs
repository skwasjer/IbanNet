﻿namespace IbanNet;

/// <summary>
/// The exception that is thrown when the format of an IBAN is invalid.
/// </summary>
[Serializable]
public class IbanFormatException : FormatException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IbanFormatException" />.
    /// </summary>
    public IbanFormatException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IbanFormatException" /> class using specified message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public IbanFormatException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IbanFormatException" /> class using specified message and inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public IbanFormatException(string message, Exception? innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IbanFormatException" /> class using specified message and validation result.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="validationResult">The validation result.</param>
    public IbanFormatException(string message, ValidationResult validationResult)
        : this(message)
    {
        Result = validationResult;
    }

    /// <summary>
    /// Gets the validation result.
    /// </summary>
    public ValidationResult? Result { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="IbanFormatException" /> with serialized data.
    /// </summary>
    /// <param name="info">The object that holds the serialized data.</param>
    /// <param name="context">The contextual information about the source or destination.</param>
#if NET8_0_OR_GREATER
#pragma warning disable CA1041
    [Obsolete(DiagnosticId = "SYSLIB0051")]
#pragma warning restore CA1041
#endif
    protected IbanFormatException
    (
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {
        // Note: Result property info is lost since it is not serializable.
    }
}
