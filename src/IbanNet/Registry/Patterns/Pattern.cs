using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace IbanNet.Registry.Patterns
{
    /// <summary>
    /// Encapsulates the pattern or subsection of a pattern of an IBAN or BBAN. Patterns consist of one or more tokens, each which describes the expected character and number of occurrences in that pattern.
    /// </summary>
    public abstract class Pattern
    {
        private string? _pattern;
        private readonly ITokenizer<PatternToken>? _tokenizer;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ReadOnlyCollection<PatternToken>? _tokens;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool? _fixedLength;

        /// <summary>
        /// Initializes a new instance of the <see cref="Pattern" /> class using a <paramref name="pattern" /> and <paramref name="tokenizer" />.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        /// <param name="tokenizer">The tokenizer to break the pattern in individual tokens down with.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="pattern" /> or <paramref name="tokenizer" /> is null.</exception>
        protected Pattern(string pattern, ITokenizer<PatternToken> tokenizer)
        {
            _pattern = pattern ?? throw new ArgumentNullException(nameof(pattern));
            _tokenizer = tokenizer ?? throw new ArgumentNullException(nameof(tokenizer));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pattern" /> class using specified <paramref name="tokens" />.
        /// </summary>
        /// <param name="tokens">The individual tokens describing the pattern.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="tokens" /> is null.</exception>
        protected Pattern(IEnumerable<PatternToken> tokens)
        {
            if (tokens is null)
            {
                throw new ArgumentNullException(nameof(tokens));
            }

            _tokens = new ReadOnlyCollection<PatternToken>(tokens.ToList());
        }

        /// <summary>
        /// Gets the individual tokens describing the pattern.
        /// </summary>
        /// <exception cref="PatternException">Thrown when the pattern is invalid.</exception>
        public IReadOnlyList<PatternToken> Tokens =>
            _tokens ??= new ReadOnlyCollection<PatternToken>(_tokenizer!.Tokenize(_pattern!).ToList());

        /// <summary>
        /// Gets whether or not the pattern is of fixed length.
        /// </summary>
        public bool IsFixedLength => _fixedLength ??= Tokens.Aggregate(true, (current, token) => current & token.IsFixedLength);

        /// <inheritdoc />
        public override string ToString()
        {
#if NET5_0_OR_GREATER || NETSTANDARD2_1
            return _pattern ??= string.Join(',', Tokens);
#else
            return _pattern ??= string.Join(",", Tokens);
#endif
        }
    }
}
