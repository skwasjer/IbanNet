﻿using System.Diagnostics.CodeAnalysis;

namespace IbanNet.Registry.Patterns;

internal class PatternValidator
{
    private readonly IReadOnlyList<PatternToken> _tokens;
    private readonly bool _isFixedLength;

    internal PatternValidator(IReadOnlyList<PatternToken> tokens, bool isFixedLength)
    {
        _tokens = tokens;
        _isFixedLength = isFixedLength;
    }

    public bool TryValidate
    (
#if USE_SPANS
        ReadOnlySpan<char> value
#else
        string value
#endif
        ,
        [NotNullWhen(false)]
        out int? errorPos
    )
    {
        // If no tokens, always fail.
        if (_tokens.Count == 0)
        {
            errorPos = value.Length;
            return false;
        }

        errorPos = null;
        int cursor = 0;
        // Short-circuit, if all tests are fixed length, use faster validation.
        bool isValid = _isFixedLength
            ? ValidateFixedLength(value, ref cursor)
            : ValidateNonFixedLength(value, ref cursor);

        if (!isValid)
        {
            errorPos = cursor;
        }

        return isValid;
    }

#if USE_SPANS
    private bool ValidateFixedLength(ReadOnlySpan<char> value, ref int cursor)
#else
    private unsafe bool ValidateFixedLength(string value, ref int cursor)
#endif
    {
        int length = value.Length;
        int tokenCount = _tokens.Count;
        int segmentIndex = 0;
#if USE_SPANS
        // ReSharper disable once InlineTemporaryVariable
        ReadOnlySpan<char> ptr = value;
#else
        fixed (char* ptr = value)
#endif
        {
            for (; segmentIndex < tokenCount; segmentIndex++)
            {
                PatternToken expectedToken = _tokens[segmentIndex];
                int maxLength = expectedToken.MaxLength;

                if (cursor + maxLength > length)
                {
                    cursor = length;
                    return false;
                }

                Func<char, int, bool> isMatch = expectedToken.IsMatch;
                for (int occurrence = 0; occurrence < maxLength; occurrence++)
                {
                    if (!isMatch(ptr[cursor], occurrence))
                    {
                        return false;
                    }

                    cursor++;
                }
            }
        }

        return length == cursor && segmentIndex == tokenCount;
    }

#if USE_SPANS
    private bool ValidateNonFixedLength(ReadOnlySpan<char> value, ref int cursor)
#else
    private bool ValidateNonFixedLength(string value, ref int cursor)
#endif
    {
        int segmentIndex = 0;
        for (; segmentIndex < _tokens.Count; segmentIndex++)
        {
            PatternToken expectedToken = _tokens[segmentIndex];
            if (expectedToken.IsFixedLength)
            {
                if (!ProcessFixedLengthTest(expectedToken, value, ref cursor))
                {
                    return false;
                }
            }
            else if (!ProcessNonFixedLengthTest(expectedToken, value, ref cursor))
            {
                return false;
            }
        }

        return value.Length == cursor && segmentIndex == _tokens.Count;
    }

    private static bool ProcessFixedLengthTest(
        PatternToken expectedToken,
#if USE_SPANS
        ReadOnlySpan<char> value,
#else
        string value,
#endif
        ref int cursor
    )
    {
        if (cursor + expectedToken.MaxLength > value.Length)
        {
            cursor = value.Length;
            return false;
        }

        for (int occurrence = 0; occurrence < expectedToken.MaxLength; occurrence++)
        {
            char ch = value[cursor];
            if (!expectedToken.IsMatch(ch, occurrence))
            {
                return false;
            }

            cursor++;
        }

        return true;
    }

    private static bool ProcessNonFixedLengthTest(
        PatternToken expectedToken,
#if USE_SPANS
        ReadOnlySpan<char> value,
#else
        string value,
#endif
        ref int cursor
    )
    {
        int startPos = cursor;
        for (int occurrence = 0; occurrence < expectedToken.MaxLength; occurrence++)
        {
            if (cursor >= value.Length)
            {
                return cursor >= startPos + expectedToken.MinLength && cursor <= startPos + expectedToken.MaxLength;
            }

            char ch = value[cursor];
            if (!expectedToken.IsMatch(ch, occurrence))
            {
                return false;
            }

            cursor++;
        }

        return cursor >= startPos + expectedToken.MinLength && cursor <= startPos + expectedToken.MaxLength;
    }
}
