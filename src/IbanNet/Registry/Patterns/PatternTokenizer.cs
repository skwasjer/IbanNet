using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using IbanNet.Extensions;

namespace IbanNet.Registry.Patterns
{
    internal abstract class PatternTokenizer : ITokenizer<PatternToken>
    {
        private readonly Func<char, bool> _partitionOn;

        protected PatternTokenizer(Func<char, bool> partitionOn)
        {
            _partitionOn = partitionOn ?? throw new ArgumentNullException(nameof(partitionOn));
        }

        /// <inheritdoc />
#if USE_SPANS
        public virtual IEnumerable<PatternToken> Tokenize(ReadOnlySpan<char> input)
#else
        public virtual IEnumerable<PatternToken> Tokenize(IEnumerable<char> input)
#endif
        {
            return input
                .PartitionOn(_partitionOn)
                .Select(CreateToken);
        }

        private PatternToken CreateToken(string token)
        {
            AsciiCategory asciiCategory = GetCategory(token);
            int occurrences = GetLength(token, out bool isFixedLength);
            if (asciiCategory == AsciiCategory.Other || occurrences <= 0)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.ArgumentException_The_structure_segment_0_is_invalid, token), nameof(token));
            }

            return new PatternToken(asciiCategory, isFixedLength ? occurrences : 1, occurrences);
        }

        protected abstract AsciiCategory GetCategory(string token);

        /// <summary>
        /// Gets the length of the token and whether or not it is fixed length.
        /// </summary>
        /// <param name="token">The token to get length for.</param>
        /// <param name="isFixedLength"><see langword="true"/> if the token is fixed length; otherwise, <see langword="false"/></param>
        /// <returns></returns>
        protected virtual int GetLength(string token, out bool isFixedLength)
        {
            isFixedLength = true;
            return token.Length;
        }
    }
}
