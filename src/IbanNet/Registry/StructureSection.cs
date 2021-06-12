using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using IbanNet.Registry.Patterns;

namespace IbanNet.Registry
{
    /// <summary>
    /// Defines a section of a structure.
    /// </summary>
    [DebuggerStepThrough]
    public abstract class StructureSection
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _example;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int? _length;

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureSection" /> class using specified parameters.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        /// <param name="position">The position where the pattern occurs within the parent structure.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="pattern" /> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="position" /> is less than 0.</exception>
        protected StructureSection(Pattern pattern, int position = 0)
        {
            Pattern = pattern ?? throw new ArgumentNullException(nameof(pattern));
            if (position < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(position), string.Format(CultureInfo.CurrentCulture, Resources.The_value_cannot_be_less_than_0, 0));
            }

            Position = position;
            _example = string.Empty;
        }

        /// <summary>
        /// Gets or sets the position within the structure.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when setting new value to less than 0.</exception>
        public int Position { get; }

        /// <summary>
        /// Gets the section length.
        /// </summary>
        public int Length
        {
            get => _length ??= Pattern.Tokens.Sum(t => t.MaxLength);
        }

        /// <summary>
        /// Gets or sets the section example.
        /// </summary>
        [AllowNull]
        public string Example
        {
            get => _example;
            set => _example = value ?? string.Empty;
        }

        /// <summary>
        /// Gets the pattern.
        /// </summary>
        public Pattern Pattern { get; }
    }
}
