using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace IbanNet.Registry.Patterns;

/// <summary>
/// Encapsulates the pattern or subsection of a pattern of an IBAN or BBAN. Patterns consist of one or more tokens, each which describes the expected character and number of occurrences in that pattern.
/// </summary>
public abstract class Pattern
{
    private string? _pattern;
    private readonly ITokenizer<PatternToken>? _tokenizer;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private List<PatternToken>? _tokens;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private bool? _fixedLength;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private PatternValidator? _patternValidator;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int? _maxLength;

    /// <summary>
    /// Initializes a new instance of the <see cref="Pattern" /> class using a <paramref name="pattern" /> and <paramref name="tokenizer" />.
    /// </summary>
    /// <param name="pattern">The pattern.</param>
    /// <param name="tokenizer">The tokenizer to break the pattern in individual tokens down with.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="pattern" /> or <paramref name="tokenizer" /> is null.</exception>
    protected Pattern(string pattern, ITokenizer<PatternToken> tokenizer)
    {
        _pattern = pattern ?? throw new ArgumentNullException(nameof(pattern));
        _tokenizer = tokenizer ?? throw new ArgumentNullException(nameof(tokenizer));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Pattern" /> class using specified <paramref name="tokens" />.
    /// </summary>
    /// <param name="tokens">The individual tokens describing the pattern.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="tokens" /> is null.</exception>
    protected Pattern(IEnumerable<PatternToken> tokens)
    {
        if (tokens is null)
        {
            throw new ArgumentNullException(nameof(tokens));
        }

        EnsureInitialized(tokens);
    }

    /// <summary>
    /// Gets the individual tokens describing the pattern.
    /// </summary>
    /// <exception cref="PatternException">Thrown when the pattern is invalid.</exception>
    public IReadOnlyList<PatternToken> Tokens
    {
        get
        {
            EnsureInitialized();
            return new ReadOnlyCollection<PatternToken>(_tokens!);
        }
    }

    /// <summary>
    /// Gets whether or not the pattern is of fixed length.
    /// </summary>
    public bool IsFixedLength
    {
        get
        {
            EnsureInitialized();
            return _fixedLength!.Value;
        }
    }

    /// <summary>
    /// Gets the maximum length of this pattern.
    /// </summary>
    public int MaxLength
    {
        get
        {
            EnsureInitialized();
            return _maxLength!.Value;
        }
    }

    /// <inheritdoc />
    public override string ToString()
    {
#if NETSTANDARD2_1 || NET6_0_OR_GREATER
        return _pattern ??= string.Join(',', Tokens);
#else
        return _pattern ??= string.Join(",", Tokens);
#endif
    }

#if USE_SPANS
    internal bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
    {
#else
    internal bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (value is null)
        {
            errorPos = -1;
            return false;
        }
#endif
        EnsureInitialized();
        _patternValidator ??= new PatternValidator(_tokens!.Compress(), IsFixedLength);
        return _patternValidator.TryValidate(value, out errorPos);
    }

    private void EnsureInitialized(IEnumerable<PatternToken>? tokens = null)
    {
        if (_tokens is not null)
        {
            return;
        }

        IEnumerable<PatternToken> tokensEnum = tokens ?? _tokenizer!.Tokenize(_pattern!);
        List<PatternToken> tokensList = tokensEnum as List<PatternToken> ?? tokensEnum.ToList();
        InitLength(tokensList);
        _tokens = tokensList;
    }

    private void InitLength(List<PatternToken> tokens)
    {
        if (_fixedLength.HasValue)
        {
            return;
        }

        bool fixedLength = true;
        int maxLength = 0;
#if NET6_0_OR_GREATER
        foreach (ref readonly PatternToken token in CollectionsMarshal.AsSpan(tokens))
#else
        foreach (PatternToken token in tokens)
#endif
        {
            fixedLength &= token.IsFixedLength;
            maxLength += token.MaxLength;
        }

        _fixedLength = fixedLength;
        _maxLength = maxLength;
    }
}
