using System;
using System.Collections.Generic;

namespace IbanNet.Registry.Patterns
{
    internal class PatternValidator
    {
        private readonly IReadOnlyList<PatternToken> _tokens;
        private readonly Func<string, bool> _validationFunc;

        public PatternValidator(Pattern pattern)
        {
            _tokens = pattern.Tokens;
            _validationFunc = GetValidationMethod(pattern.IsFixedLength);
        }

        public bool Validate(string iban) => _validationFunc(iban);

        private unsafe bool ValidateFixedLength(string value)
        {
            int length = value.Length;
            int tokenCount = _tokens.Count;
            int pos = 0;
            int segmentIndex = 0;
            fixed (char* ptr = value)
            {
                // ReSharper disable once ForCanBeConvertedToForeach - justification : performance critical
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
                        char c = ptr[pos];
                        if (!isMatch(c))
                        {
                            return false;
                        }

                        pos++;
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

        private Func<string, bool> GetValidationMethod(bool fixedLength)
        {
            // Short-circuit, if all tests are fixed length, use faster validation.
            if (fixedLength)
            {
                return ValidateFixedLength;
            }
            return ValidateNonFixedLength;
        }
    }
}
