using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace IbanNet.Registry.Patterns;

/// <summary>
/// Extensions for <see cref="Pattern" />.
/// </summary>
public static class PatternExtensions
{
    /// <summary>
    /// Generates a regex pattern from the validation pattern.
    /// </summary>
    /// <param name="pattern">The pattern to generate a regex pattern for.</param>
    /// <returns>A regex pattern that can be used with <see cref="Regex" />.</returns>
    public static string ToRegexPattern(this Pattern pattern)
    {
        if (pattern is null)
        {
            throw new ArgumentNullException(nameof(pattern));
        }

        // We compress to keep the regex as small as possible.
        // The regex for now has no capture groups (eg. for country code, bank/branch, account nr.)
        // because Pattern does not encapsulate this info. It would also use more mem.
        return pattern.Tokens.Compress().ToRegexPattern();
    }

    private static string ToRegexPattern(this IEnumerable<PatternToken> tokens)
    {
        return $"^{string.Concat(tokens.Select(GenerateRegexPattern))}$";
    }

    private static string GenerateRegexPattern(PatternToken token)
    {
        if (token.Value is not null)
        {
            return token.Value;
        }

        string? length =
            token.MinLength > 1
                ? token.IsFixedLength
                    ? string.Format(CultureInfo.InvariantCulture, "{{{0}}}", token.MaxLength)
                    : string.Format(CultureInfo.InvariantCulture, "{{{0},{1}}}", token.MinLength, token.MaxLength)
                : null;

        return token.Category switch
        {
            AsciiCategory.AlphaNumeric => "[a-zA-Z0-9]",
            AsciiCategory.Letter => "[a-zA-Z]",
            AsciiCategory.Digit => "\\d",
            AsciiCategory.LowercaseLetter => "[a-z]",
            AsciiCategory.UppercaseLetter => "[A-Z]",
            AsciiCategory.Space => " ",
            _ => throw new InvalidOperationException("Invalid character token.") // Should never happen but to satisfy intellisense.
        } + length;
    }

    /// <summary>
    /// Compresses a sequence of pattern tokens by combining aligned tokens of same ASCII type into a single token.
    /// </summary>
    /// <param name="tokens">A list of tokens.</param>
    /// <returns>A compressed list of tokens.</returns>
    internal static IReadOnlyList<PatternToken> Compress(this IReadOnlyList<PatternToken> tokens)
    {
        if (tokens.Count == 0)
        {
            throw new ArgumentException("Expected list of tokens.", nameof(tokens));
        }

        var compressedTokens = new List<PatternToken>(tokens.Count);

#if NET6_0_OR_GREATER
        Span<PatternToken> tokenSpan = CollectionsMarshal.AsSpan(tokens as List<PatternToken> ?? tokens.ToList());
        PatternToken current = tokenSpan[0];
        Span<PatternToken> tokensExceptFirst = tokenSpan[1..];
        foreach (ref readonly PatternToken token in tokensExceptFirst)
#else
        PatternToken current = tokens[0];
        IEnumerable<PatternToken> tokensExceptFirst = tokens.Skip(1);
        foreach (PatternToken token in tokensExceptFirst)
#endif
        {
            if (current.Category != AsciiCategory.None && current.Category == token.Category)
            {
                current = new PatternToken(token.Category, current.MinLength + token.MinLength, current.MaxLength + token.MaxLength);
                continue;
            }

            compressedTokens.Add(current);

            current = token;
        }

        compressedTokens.Add(current);
        return compressedTokens;
    }
}
