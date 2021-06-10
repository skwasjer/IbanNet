using System;
using System.Collections.Generic;

namespace IbanNet.Registry.Patterns
{
    /// <summary>
    /// Provides a way to classify sections of an input string into separate tokens.
    /// </summary>
    /// <typeparam name="TToken"></typeparam>
    public interface ITokenizer<out TToken>
    {
        /// <summary>
        /// Tokenizes an input string into individual tokens.
        /// </summary>
        /// <param name="input">The input buffer.</param>
        /// <returns>Returns an enumerable of tokens describing the input buffer.</returns>
#if USE_SPANS
        IEnumerable<TToken> Tokenize(ReadOnlySpan<char> input);
#else
        IEnumerable<TToken> Tokenize(IEnumerable<char> input);
#endif
    }
}
