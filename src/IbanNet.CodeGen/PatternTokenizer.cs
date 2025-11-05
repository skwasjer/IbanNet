using IbanNet.Extensions;
using IbanNet.Registry.Patterns;

namespace IbanNet.CodeGen;

internal abstract class PatternTokenizer : ITokenizer<PatternToken>
{
    private readonly Func<char, bool> _partitionOn;

    protected PatternTokenizer(Func<char, bool> partitionOn)
    {
        _partitionOn = partitionOn ?? throw new ArgumentNullException(nameof(partitionOn));
    }

    /// <inheritdoc />
    public virtual IEnumerable<PatternToken> Tokenize(IEnumerable<char> input)
    {
        if (input is null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        return TokenizeIterator(input);
    }

    private List<PatternToken> TokenizeIterator(IEnumerable<char> input)
    {
        var tokenList = new List<PatternToken>(8);
        var tokenCharList = new List<string>(4);
        int pos = 0;

        void AddCharTokens(IEnumerable<string> charTokens)
        {
            tokenList.AddRange(charTokens.Select(t => CreateToken(t, pos++)));
        }

        foreach (string tokenStr in input.PartitionOn(_partitionOn))
        {
            if (tokenStr.Length == 1)
            {
                tokenCharList.Add(tokenStr);
            }
            else
            {
                if (tokenCharList.Count > 0)
                {
                    string cc = string.Join(string.Empty, tokenCharList);
                    if (IsCountryCodeToken(cc))
                    {
                        tokenList.Add(CreateToken(cc, pos++));
                    }
                    else
                    {
                        AddCharTokens(tokenCharList);
                    }

                    tokenCharList.Clear();
                }

                tokenList.Add(CreateToken(tokenStr, pos++));
            }
        }

        AddCharTokens(tokenCharList);

        return tokenList;
    }

    private PatternToken CreateToken(string token, int pos)
    {
        try
        {
            if (IsCountryCodeToken(token))
            {
                return new PatternToken(token);
            }

            AsciiCategory asciiCategory = AsciiCategory.None;
            int occurrences = 0;
            bool isFixedLength = true;
            if (token.Length > 0)
            {
                asciiCategory = GetCategory(token);
                occurrences = GetLength(token, asciiCategory, out isFixedLength);
            }

            if (asciiCategory == AsciiCategory.None || occurrences <= 0)
            {
                throw new PatternException($"The pattern token '{token}' is invalid at position {pos}.");
            }

            return new PatternToken(asciiCategory, isFixedLength ? occurrences : 1, occurrences);
        }
        catch (Exception ex) when (
            ex is ArgumentException
                or InvalidOperationException
                or FormatException and not PatternException
                or IndexOutOfRangeException
            )
        {
            throw new PatternException($"The pattern token '{token}' is invalid at position {pos}.", ex);
        }
    }

    protected abstract AsciiCategory GetCategory(string token);

    /// <summary>
    /// Gets the length of the token and whether or not it is fixed length.
    /// </summary>
    /// <param name="token">The token to get length for.</param>
    /// <param name="category">The ASCII category.</param>
    /// <param name="isFixedLength"><see langword="true" /> if the token is fixed length; otherwise, <see langword="false" /></param>
    /// <returns></returns>
    protected abstract int GetLength(string token, AsciiCategory category, out bool isFixedLength);

    private static bool IsCountryCodeToken(string token)
    {
        return token.Length == 2 && token[0].IsUpperAsciiLetter() && token[1].IsUpperAsciiLetter();
    }
}
