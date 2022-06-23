namespace IbanNet.Registry.Patterns
{
    internal class PatternValidator
    {
        private readonly IReadOnlyList<PatternToken> _tokens;
#if USE_SPANS
        private readonly bool _isFixedLength;
#else
        private readonly Func<string, bool> _validationFunc;
#endif

        public PatternValidator(Pattern pattern)
        {
            _tokens = pattern.Tokens;
#if USE_SPANS
            _isFixedLength = pattern.IsFixedLength;
#else
            _validationFunc = GetValidationMethod(pattern);
#endif
        }

#if USE_SPANS
        public bool Validate(ReadOnlySpan<char> value)
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

        private bool ValidateFixedLength(ReadOnlySpan<char> value)
        {
            int length = value.Length;
            int tokenCount = _tokens.Count;
            int pos = 0;
            int segmentIndex = 0;
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
                    if (!isMatch(value[pos++]))
                    {
                        return false;
                    }
                }
            }

            return length == pos && segmentIndex == tokenCount;
        }

        private bool ValidateNonFixedLength(ReadOnlySpan<char> value)
        {
            int pos = 0;
            int segmentIndex = 0;
            for (; segmentIndex < _tokens.Count; segmentIndex++)
            {
                PatternToken? expectedToken = _tokens[segmentIndex];
                int count = expectedToken.MaxLength;
                if (expectedToken.IsFixedLength)
                {
                    if (!ProcessFixedLengthTest(expectedToken, value.Slice(pos)))
                    {
                        return false;
                    }

                    pos += expectedToken.MaxLength;
                }
                else if (!ProcessNonFixedLengthTest(expectedToken, value.Slice(pos), out count))
                {
                    return false;
                }

                pos += count;
            }

            return value.Length == pos && segmentIndex == _tokens.Count;
        }

        private static bool ProcessFixedLengthTest(PatternToken expectedToken, ReadOnlySpan<char> value)
        {
            if (expectedToken.MaxLength > value.Length)
            {
                return false;
            }

            for (int occurrence = 0; occurrence < expectedToken.MaxLength; occurrence++)
            {
                char c = value[occurrence];
                if (!expectedToken.IsMatch(c))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool ProcessNonFixedLengthTest(PatternToken expectedToken, ReadOnlySpan<char> value, out int occurrence)
        {
            occurrence = 0;
            for (; occurrence < expectedToken.MaxLength; occurrence++)
            {
                if (occurrence >= value.Length)
                {
                    return occurrence >= expectedToken.MinLength && occurrence <= expectedToken.MaxLength;
                }

                char c = value[occurrence];
                if (!expectedToken.IsMatch(c))
                {
                    return false;
                }
            }

            return occurrence >= expectedToken.MinLength && occurrence <= expectedToken.MaxLength;
        }

#else
        public bool Validate(string value)
        {
            return _validationFunc(value);
        }

        private unsafe bool ValidateFixedLength(string value)
        {
            int length = value.Length;
            int tokenCount = _tokens.Count;
            int pos = 0;
            int segmentIndex = 0;
            fixed (char* ptr = value)
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

        private bool ValidateNonFixedLength(string value)
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

        private static bool ProcessFixedLengthTest(PatternToken expectedToken, string value, ref int pos)
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

        private static bool ProcessNonFixedLengthTest(PatternToken expectedToken, string value, ref int pos)
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

        private Func<string, bool> GetValidationMethod(Pattern pattern)
        {
            // If no tokens, always fail.
            if (pattern.Tokens.Count == 0)
            {
                return _ => false;
            }

            // Short-circuit, if all tests are fixed length, use faster validation.
            if (pattern.IsFixedLength)
            {
                return ValidateFixedLength;
            }

            return ValidateNonFixedLength;
        }
#endif
    }
}
