using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using IbanNet.Extensions;
using IbanNet.Registry.Parsing;

namespace IbanNet.Registry.Swift
{
    /// <remarks>
    /// https://www.swift.com/standards/data-standards/iban
    /// length
    /// ! = fixed
    /// marker
    /// </remarks>
    internal class SwiftPatternTokenizer : PatternTokenizer
    {
        private const int CountryCodeLength = 2;
        private static readonly char[] TokenChars = { 'n', 'a', 'c', 'e' };
        private static readonly PatternToken CountryCodeToken = new PatternToken(AsciiCategory.Letter, CountryCodeLength);

        internal SwiftPatternTokenizer() : base(TokenChars.Contains)
        {
        }

#if USE_SPANS
        public override IEnumerable<PatternToken> Tokenize(ReadOnlySpan<char> input)
        {
            var tokens = new List<PatternToken>();

            // Swift pattern starts with country code (e.g. first 2 char is a letter)?
            if (input.Length >= CountryCodeLength && input[0].IsAsciiLetter() && input[1].IsAsciiLetter())
            {
                tokens.Add(CountryCodeToken);
                input = input.Slice(CountryCodeLength);
            }

            tokens.AddRange(base.Tokenize(input));
            return tokens;
        }
#else
        public override IEnumerable<PatternToken> Tokenize(IEnumerable<char> input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input is char[] charBuffer)
            {
                return PatternTokensIterator(charBuffer);
            }

            if (input is string stringBuffer)
            {
                charBuffer = stringBuffer.ToCharArray();
            }
            else
            {
                charBuffer = input.ToArray();
            }

            return PatternTokensIterator(charBuffer).ToList();
        }

        private IEnumerable<PatternToken> PatternTokensIterator(char[] input)
        {
            char[] tokenizerInput = input;

            // Swift pattern starts with country code (e.g. first 2 char is a letter)?
            if (input.Length >= CountryCodeLength && input[0].IsAsciiLetter() && input[1].IsAsciiLetter())
            {
                yield return CountryCodeToken;

                tokenizerInput = new char[input.Length - CountryCodeLength];
                if (tokenizerInput.Length > 0)
                {
                    Array.Copy(input, CountryCodeLength, tokenizerInput, 0, tokenizerInput.Length);
                }
            }

            foreach (var token in base.Tokenize(tokenizerInput))
            {
                yield return token;
            }
        }
#endif

        protected override AsciiCategory GetCategory(string token)
        {
            if (token.Length < 2)
            {
                return AsciiCategory.Other;
            }

            // ReSharper disable once UseIndexFromEndExpression
            char tokenChar = token[token.Length - 1];
            return tokenChar switch
            {
                'n' => AsciiCategory.Digit,
                'a' => AsciiCategory.UppercaseLetter,
                'c' => AsciiCategory.AlphaNumeric,
                'e' => AsciiCategory.Space,
                _ => AsciiCategory.Other
            };
        }

        protected override int GetLength(string token, out bool isFixedLength)
        {
            if (token.Length < 2)
            {
                isFixedLength = true;
                return -1;
            }

            string lengthDescriptor = token.Substring(0, token.Length - 1);
            // ReSharper disable once UseIndexFromEndExpression
            isFixedLength = lengthDescriptor[lengthDescriptor.Length - 1] == '!';
            return int.Parse(
                lengthDescriptor.Substring(0, lengthDescriptor.Length - Convert.ToByte(isFixedLength)),
                NumberStyles.None,
                CultureInfo.InvariantCulture
            );
        }
    }
}
