namespace IbanNet.Validation.Results;

/// <summary>
/// Describes the error that occurred for a validation rule.
/// </summary>
public class ExceptionResult : ErrorResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionResult" /> class using specified <paramref name="exception" />.
    /// </summary>
    /// <param name="exception">The exception.</param>
    public ExceptionResult(Exception exception)
        : base(exception?.Message ?? string.Empty)
    {
        Exception = exception ?? throw new ArgumentNullException(nameof(exception));
    }

    /// <summary>
    /// Gets the exception.
    /// </summary>
    public Exception Exception { get; }
}