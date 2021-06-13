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
                .Select(CreateToken)
                .ToList();
        }

        private PatternToken CreateToken(string token, int pos)
        {
            try
            {
                AsciiCategory asciiCategory = GetCategory(token);
                int occurrences = GetLength(token, out bool isFixedLength);
                if (asciiCategory == AsciiCategory.Other || occurrences <= 0)
                {
                    throw new PatternException(string.Format(CultureInfo.CurrentCulture, Resources.ArgumentException_The_structure_segment_0_is_invalid, token, pos));
                }

                return new PatternToken(asciiCategory, isFixedLength ? occurrences : 1, occurrences);
            }
            catch (Exception ex) when (
                ex is ArgumentException
             || ex is InvalidOperationException
             || ex is FormatException && !(ex is PatternException)
             || ex is IndexOutOfRangeException
                )
            {
                throw new PatternException(string.Format(CultureInfo.CurrentCulture, Resources.ArgumentException_The_structure_segment_0_is_invalid, token, pos), ex);
            }
        }

        protected abstract AsciiCategory GetCategory(string token);

        /// <summary>
        /// Gets the length of the token and whether or not it is fixed length.
        /// </summary>
        /// <param name="token">The token to get length for.</param>
        /// <param name="isFixedLength"><see langword="true"/> if the token is fixed length; otherwise, <see langword="false"/></param>
        /// <returns></returns>
        protected abstract int GetLength(string token, out bool isFixedLength);
    }
}
