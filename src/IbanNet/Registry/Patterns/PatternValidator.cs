namespace IbanNet.Registry.Patterns
{
    internal class PatternValidator
    {
        private readonly IReadOnlyList<PatternToken> _tokens;
        private readonly bool _isFixedLength;

        public PatternValidator(Pattern pattern)
        {
            _tokens = pattern.Tokens;
            _isFixedLength = pattern.IsFixedLength;
        }

        public bool Validate
        (
#if USE_SPANS
            ReadOnlySpan<char> value
#else
            string value
#endif
        )
        {
            // If no tokens, always fail.
            if (_tokens.Count == 0)
            {
                return false;
            }

            // Short-circuit, if all tests are fixed length, use faster validation.
            return _isFixedLength
                ? ValidateFixedLength(value)
                : ValidateNonFixedLength(value);
        }

#if USE_SPANS
        private bool ValidateFixedLength(ReadOnlySpan<char> value)
#else
        private unsafe bool ValidateFixedLength(string value)
#endif
        {
            int length = value.Length;
            int tokenCount = _tokens.Count;
            int pos = 0;
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

                    if (pos + maxLength > length)
                    {
                        return false;
                    }

                    Func<char, bool> isMatch = expectedToken.IsMatch;
                    for (int occurrence = 0; occurrence < maxLength; occurrence++)
                    {
                        if (!isMatch(ptr[pos++]))
                        {
                            return false;
                        }
                    }
                }
            }

            return length == pos && segmentIndex == tokenCount;
        }

#if USE_SPANS
        private bool ValidateNonFixedLength(ReadOnlySpan<char> value)
#else
        private bool ValidateNonFixedLength(string value)
#endif
        {
            int pos = 0;
            int segmentIndex = 0;
            for (; segmentIndex < _tokens.Count; segmentIndex++)
            {
                PatternToken? expectedToken = _tokens[segmentIndex];
                if (expectedToken.IsFixedLength)
                {
                    if (!ProcessFixedLengthTest(expectedToken, value, ref pos))
                    {
                        return false;
                    }
                }
                else if (!ProcessNonFixedLengthTest(expectedToken, value, ref pos))
                {
                    return false;
                }
            }

            return value.Length == pos && segmentIndex == _tokens.Count;
        }

        private static bool ProcessFixedLengthTest(
            PatternToken expectedToken,
#if USE_SPANS
            ReadOnlySpan<char> value,
#else
            string value,
#endif
            ref int pos
        )
        {
            if (pos + expectedToken.MaxLength > value.Length)
            {
                return false;
            }

            for (int occurrence = 0; occurrence < expectedToken.MaxLength; occurrence++)
            {
                char c = value[pos];
                if (!expectedToken.IsMatch(c))
                {
                    return false;
                }

                pos++;
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
            ref int pos
        )
        {
            int startPos = pos;
            for (int occurrence = 0; occurrence < expectedToken.MaxLength; occurrence++)
            {
                if (pos >= value.Length)
                {
                    return pos >= startPos + expectedToken.MinLength && pos <= startPos + expectedToken.MaxLength;
                }

                char c = value[pos];
                if (!expectedToken.IsMatch(c))
                {
                    return false;
                }

                pos++;
            }

            return pos >= startPos + expectedToken.MinLength && pos <= startPos + expectedToken.MaxLength;
        }
    }
}
