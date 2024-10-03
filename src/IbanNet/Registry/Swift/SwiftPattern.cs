using IbanNet.Registry.Patterns;

namespace IbanNet.Registry.Swift;

/// <inheritdoc />
internal class SwiftPattern : Pattern
{
    private static readonly SwiftPatternTokenizer Tokenizer = new();
    private string? _pattern;
    private readonly int? _maxLength;
    private readonly bool? _isFixedLength;

    /// <inheritdoc />
    public SwiftPattern(string pattern) : base(pattern, Tokenizer)
    {
        _pattern = pattern;
    }

    internal SwiftPattern(IEnumerable<PatternToken> tokens) : base(tokens)
    {
    }

    /// <inheritdoc />
    internal SwiftPattern(string pattern, int maxLength, bool isFixedLength)
        : this(pattern)
    {
        _maxLength = maxLength;
        _isFixedLength = isFixedLength;
    }

    /// <inheritdoc />
    public override bool IsFixedLength
    {
        get
        {
            return _isFixedLength ?? base.IsFixedLength;
        }
    }

    /// <inheritdoc />
    public override int MaxLength
    {
        get
        {
            return _maxLength ?? base.MaxLength;
        }
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return _pattern ??= string.Join("", Tokens.Select(t =>
        {
            if (t.Value is not null)
            {
                return t.Value;
            }

            string fixedLen = t.IsFixedLength ? "!" : string.Empty;
            return $"{t.MaxLength}{fixedLen}{GetToken(t.Category)}";
        }));
    }

    private static char GetToken(AsciiCategory category)
    {
        return category switch
        {
            AsciiCategory.Digit => 'n',
            AsciiCategory.UppercaseLetter => 'a',
            AsciiCategory.AlphaNumeric => 'c',
            AsciiCategory.Space => 'e',
            _ => throw new InvalidOperationException()
        };
    }
}
