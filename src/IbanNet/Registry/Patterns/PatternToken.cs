using System.ComponentModel;
using System.Globalization;

namespace IbanNet.Registry.Patterns;

/// <summary>
/// Defines a token that spans one or more characters of the same <see cref="AsciiCategory" />.
/// </summary>
public sealed record PatternToken
{
    /// <summary>
    /// Initializes a new instance of the pattern token that matches a specific string explicitly.
    /// </summary>
    /// <param name="value">The token string value.</param>
    public PatternToken(string value)
        // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
        : this(AsciiCategory.None, value?.Length ?? throw new ArgumentNullException(nameof(value)), value.Length, nameof(value))
    {
        Value = value;
    }

    /// <summary>
    /// Initializes a new instance of the pattern token.
    /// </summary>
    /// <param name="category">The ASCII category for the token.</param>
    /// <param name="length">The length of the token.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="length" /> is less than 1.</exception>
    /// <exception cref="InvalidEnumArgumentException">Thrown when <paramref name="category" /> is an invalid value.</exception>
    public PatternToken(AsciiCategory category, int length)
        : this(category, length, length, nameof(length))
    {
        if (category == AsciiCategory.None)
        {
            throw new InvalidEnumArgumentException(nameof(category), (int)category, typeof(AsciiCategory));
        }
    }

    /// <summary>
    /// Initializes a new instance of the pattern token.
    /// </summary>
    /// <param name="category">The ASCII category for the token.</param>
    /// <param name="minLength">The minimum length of the token.</param>
    /// <param name="maxLength">The maximum length of the token.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="minLength" /> or <paramref name="maxLength" /> is less than 1, or <paramref name="maxLength" /> is less than <paramref name="minLength" />.</exception>
    /// <exception cref="InvalidEnumArgumentException">Thrown when <paramref name="category" /> is an invalid value.</exception>
    public PatternToken(AsciiCategory category, int minLength, int maxLength)
        : this(category, minLength, maxLength, nameof(minLength))
    {
        if (category == AsciiCategory.None)
        {
            throw new InvalidEnumArgumentException(nameof(category), (int)category, typeof(AsciiCategory));
        }
    }

    private PatternToken(AsciiCategory category, int minLength, int maxLength, string minLengthPropertyName)
    {
#if NET6_0_OR_GREATER
        if (!Enum.IsDefined(category))
#else
        if (!Enum.IsDefined(typeof(AsciiCategory), category))
#endif
        {
            throw new InvalidEnumArgumentException(nameof(category), (int)category, typeof(AsciiCategory));
        }

        if (minLength <= 0)
        {
            throw new ArgumentOutOfRangeException(minLengthPropertyName, string.Format(CultureInfo.CurrentCulture, Resources.The_value_cannot_be_less_than_or_equal_to_0, 0));
        }

        if (maxLength <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxLength), string.Format(CultureInfo.CurrentCulture, Resources.The_value_cannot_be_less_than_or_equal_to_0, 0));
        }

        if (maxLength < minLength)
        {
            throw new ArgumentOutOfRangeException(nameof(maxLength), string.Format(CultureInfo.CurrentCulture, Resources.The_value_cannot_be_less_than_or_equal_to_0, minLength));
        }

        Category = category;
        MinLength = minLength;
        MaxLength = maxLength;
        IsFixedLength = minLength == MaxLength;
    }

    /// <summary>
    /// Gets the ASCII category for this token.
    /// </summary>
    public AsciiCategory Category { get; }

    /// <summary>
    /// Gets the minimum length of this token.
    /// </summary>
    public int MinLength { get; }

    /// <summary>
    /// Gets the maximum length of this token.
    /// </summary>
    public int MaxLength { get; }

    /// <summary>
    /// Gets whether or not this token is of fixed length.
    /// </summary>
    public bool IsFixedLength { get; }

    /// <summary>
    /// Gets the token value, if not representing an ASCII category (<see cref="Category" /> = <see cref="AsciiCategory.None" />).
    /// </summary>
    public string? Value { get; }

    /// <inheritdoc />
    public override string ToString()
    {
#if NET6_0_OR_GREATER
        string? category = Enum.GetName(Category);
#else
        string? category = Enum.GetName(typeof(AsciiCategory), Category);
#endif
        return IsFixedLength
            ? $"{category}[{MaxLength}]"
            : $"{category}[{MinLength}-{MaxLength}]";
    }
}
