using System;
using System.Collections.Generic;
using IbanNet.Registry.Patterns;

namespace IbanNet.Validation
{
    internal class StructureValidator : IStructureValidator
    {
        private readonly IReadOnlyList<PatternToken> _tokens;
        private readonly Func<string, bool> _validationFunc;

        public StructureValidator(IEnumerable<PatternToken> tokens)
        {
            var tokenList = new List<PatternToken>();
            bool fixedLength = true;
            foreach (PatternToken token in tokens)
            {
                tokenList.Add(token);
                fixedLength &= token.IsFixedLength;
            }

            _tokens = tokenList;
            _validationFunc = GetValidationMethod(fixedLength);
        }

        public StructureValidator(Pattern pattern)
        {
            _tokens = pattern.Tokens;
            _validationFunc = GetValidationMethod(pattern.IsFixedLength);
        }

        public bool Validate(string iban) => _validationFunc(iban);

        private bool ValidateFixedLength(string iban)
        {
            int pos = 0;
            int segmentIndex = 0;
            // ReSharper disable once ForCanBeConvertedToForeach - justification : performance critical
            for (; segmentIndex < _tokens.Count; segmentIndex++)
            {
                PatternToken expectedToken = _tokens[segmentIndex];
                if (pos + expectedToken.MaxLength > iban.Length)
                {
                    return false;
                }

                for (int occurrence = 0; occurrence < expectedToken.MaxLength; occurrence++)
                {
                    char c = iban[pos];
                    if (!expectedToken.IsMatch(c))
                    {
                        return false;
                    }

                    pos++;
                }
            }

            return iban.Length == pos && segmentIndex == _tokens.Count;
        }

        private bool ValidateNonFixedLength(string iban)
        {
            int pos = 0;
            int segmentIndex = 0;
            for (; segmentIndex < _tokens.Count; segmentIndex++)
            {
                PatternToken? expectedToken = _tokens[segmentIndex];
                if (expectedToken.IsFixedLength)
                {
                    if (!ProcessFixedLengthTest(expectedToken, iban, ref pos))
                    {
                        return false;
                    }
                }
                else if (!ProcessNonFixedLengthTest(expectedToken, iban, ref pos))
                {
                    return false;
                }
            }

            return iban.Length == pos && segmentIndex == _tokens.Count;
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
