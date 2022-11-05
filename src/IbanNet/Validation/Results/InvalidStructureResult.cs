namespace IbanNet.Validation.Results;

/// <summary>
/// The result returned when the structure of the IBAN is incorrect.
/// </summary>
public class InvalidStructureResult : ErrorResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidStructureResult" /> class.
    /// </summary>
    /// <param name="position">The position of the illegal character.</param>
    public InvalidStructureResult(int position)
        : base(Resources.InvalidStructureResult)
    {
        Position = position;
    }

    /// <summary>
    /// Gets the character position where the first illegal character was encountered.
    /// </summary>
    public int Position { get; }
}