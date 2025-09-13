namespace IbanNet.Validation.Results;

/// <summary>
/// The result returned when the IBAN contains illegal characters.
/// </summary>
public record IllegalCharactersResult : ErrorResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IllegalCharactersResult" /> class.
    /// </summary>
    /// <param name="position">The position of the illegal character.</param>
    public IllegalCharactersResult(int position)
        : this(Resources.IllegalCharactersResult, position)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IllegalCharactersResult" /> class.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="position">The position of the illegal character.</param>
    protected IllegalCharactersResult(string errorMessage, int position)
        : base(errorMessage)
    {
        if (position < -1)
        {
            throw new ArgumentOutOfRangeException(nameof(position));
        }

        Position = position;
    }

    /// <summary>
    /// Gets the character position where the first illegal character was encountered.
    /// </summary>
    public int Position { get; }
}
