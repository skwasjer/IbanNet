using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using IbanNet.Registry.Patterns;

namespace IbanNet.Registry;

/// <summary>
/// Describes a pattern.
/// </summary>
[DebuggerStepThrough]
public sealed record PatternDescriptor
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly string? _example;

    /// <summary>
    /// Initializes a new instance of the <see cref="PatternDescriptor" /> class using specified parameters.
    /// </summary>
    /// <param name="pattern">The pattern.</param>
    /// <param name="position">The position where this pattern starts to define the format of the input.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="pattern" /> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="position" /> is less than 0.</exception>
    public PatternDescriptor(Pattern pattern, int position = 0)
    {
        Pattern = pattern ?? throw new ArgumentNullException(nameof(pattern));
        if (position < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(position), string.Format(CultureInfo.CurrentCulture, Resources.The_value_cannot_be_less_than_0, 0));
        }

        Position = position;
    }

    /// <summary>
    /// Gets the position where this pattern starts to define the format of the input.
    /// </summary>
    public int Position { get; }

    /// <summary>
    /// Gets the allowed length of the input.
    /// </summary>
    public int Length
    {
        get => Pattern.MaxLength;
    }

    /// <summary>
    /// Gets an example that matches the <see cref="Pattern" />.
    /// </summary>
    [AllowNull]
    public string Example
    {
        get => _example ?? string.Empty;
        init => _example = value;
    }

    /// <summary>
    /// Gets the pattern.
    /// </summary>
    public Pattern Pattern { get; }
}
