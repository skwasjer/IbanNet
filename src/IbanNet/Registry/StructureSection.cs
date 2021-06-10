using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using IbanNet.Registry.Patterns;
using IbanNet.Validation;

namespace IbanNet.Registry
{
    /// <summary>
    /// Defines a section of a structure.
    /// </summary>
    [DebuggerStepThrough]
    public abstract class StructureSection : IStructureSection
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _example;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _structure;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int? _length;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int _position;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IStructureValidationFactory _structureValidationFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureSection" /> class.
        /// </summary>
        protected internal StructureSection()
            : this(string.Empty, new NullStructureValidationFactory())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureSection" /> class using specified parameters.
        /// </summary>
        /// <param name="structure">The structure.</param>
        /// <param name="structureValidationFactory">The validation factory.</param>
        [Obsolete("Will be removed in v5.0. Use the overload accepting Pattern.")]
        protected StructureSection(string structure, IStructureValidationFactory structureValidationFactory)
        {
            _example = string.Empty;
            Pattern = null!;
            _structure = structure ?? throw new ArgumentNullException(nameof(structure));
            _structureValidationFactory = structureValidationFactory ?? throw new ArgumentNullException(nameof(structureValidationFactory));
        }

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

            _position = position;
            _example = string.Empty;
            _structure = pattern.ToString();
            _structureValidationFactory = new NullStructureValidationFactory();
        }

        /// <summary>
        /// Gets or sets the position within the structure.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when setting new value to less than 0.</exception>
        public int Position
        {
            get => _position;
            [Obsolete("Will be removed in v5.0. Specify the position via the ctor.")]
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _position = value;
            }
        }

        /// <summary>
        /// Gets the section length.
        /// </summary>
        public int Length
        {
            get => _length ??= Pattern?.Tokens.Sum(t => t.MaxLength) ?? 0;
            [Obsolete("Will be removed in v5.0. The length will be inferred from Pattern.")]
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _length = value;
            }
        }

        /// <summary>
        /// Gets or sets the section example.
        /// </summary>
        public string Example
        {
            get => _example;
            set => _example = value ?? string.Empty;
        }

        /// <summary>
        /// Gets the pattern.
        /// </summary>
        public Pattern Pattern { get; }

        /// <summary>
        /// Gets or sets the structure.
        /// </summary>
        [Obsolete("Will be removed in v5.0. Use Pattern instead.")]
        public string Structure
        {
            get => Pattern?.ToString() ?? _structure;
            internal set => _structure = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the structure validation factory.
        /// </summary>
        [Obsolete("Will be removed in v5.0. Use Pattern instead.")]
        public IStructureValidationFactory ValidationFactory
        {
            get => _structureValidationFactory;
            internal set => _structureValidationFactory = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}
