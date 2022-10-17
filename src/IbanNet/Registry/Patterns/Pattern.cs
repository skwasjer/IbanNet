﻿using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

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
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private PatternValidator? _patternValidator;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int? _maxLength;

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

            var asList = tokens as IList<PatternToken>;
            _tokens = new ReadOnlyCollection<PatternToken>(asList ?? tokens.ToList());
            InitLength(_tokens);
        }

        /// <summary>
        /// Gets the individual tokens describing the pattern.
        /// </summary>
        /// <exception cref="PatternException">Thrown when the pattern is invalid.</exception>
        public IReadOnlyList<PatternToken> Tokens
        {
            get
            {
                if (_tokens is not null)
                {
                    return _tokens;
                }

                // Deferred load.
                _tokens = new ReadOnlyCollection<PatternToken>(_tokenizer!.Tokenize(_pattern!).ToList());
                InitLength(_tokens);
                return _tokens;
            }
        }

        /// <summary>
        /// Gets whether or not the pattern is of fixed length.
        /// </summary>
        public bool IsFixedLength
        {
            get
            {
                InitLength(Tokens);
                return _fixedLength!.Value;
            }
        }

        /// <summary>
        /// Gets the maximum length of this pattern.
        /// </summary>
        public int MaxLength
        {
            get
            {
                InitLength(Tokens);
                return _maxLength!.Value;
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
#if NETSTANDARD2_1 || NET6_0_OR_GREATER
            return _pattern ??= string.Join(',', Tokens);
#else
            return _pattern ??= string.Join(",", Tokens);
#endif
        }

#if USE_SPANS
        internal bool IsMatch(ReadOnlySpan<char> value, [NotNullWhen(false)] out int? errorPos)
        {
#else
        internal bool IsMatch(string value, [NotNullWhen(false)] out int? errorPos)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (value is null)
            {
                errorPos = -1;
                return false;
            }
#endif
            _patternValidator ??= new PatternValidator(Compress(Tokens), IsFixedLength);
            return _patternValidator.TryValidate(value, out errorPos);
        }

        private void InitLength(IEnumerable<PatternToken> tokens)
        {
            if (_fixedLength.HasValue)
            {
                return;
            }

            bool fixedLength = true;
            int maxLength = 0;
            foreach (PatternToken token in tokens)
            {
                fixedLength &= token.IsFixedLength;
                maxLength += token.MaxLength;
            }

            _fixedLength = fixedLength;
            _maxLength = maxLength;
        }

        /// <summary>
        /// Compresses a sequence of pattern tokens by combining aligned tokens of same ASCII type into a single token.
        /// </summary>
        /// <param name="tokens">A list of tokens.</param>
        /// <returns>A compressed list of tokens.</returns>
        internal static IReadOnlyList<PatternToken> Compress(IReadOnlyList<PatternToken> tokens)
        {
            if (tokens.Count == 0)
            {
                throw new ArgumentException("Expected list of tokens.", nameof(tokens));
            }

            var compressedTokens = new List<PatternToken>();
            PatternToken current = tokens[0];
            foreach (PatternToken token in tokens.Skip(1))
            {
                if (current.Category == token.Category)
                {
                    current = new PatternToken(token.Category, current.MinLength + token.MinLength, current.MaxLength + token.MaxLength);
                    continue;
                }

                compressedTokens.Add(current);

                current = token;
            }

            compressedTokens.Add(current);
            return compressedTokens;
        }
    }
}
