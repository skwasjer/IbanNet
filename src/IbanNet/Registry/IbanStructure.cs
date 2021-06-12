using System;
using System.Diagnostics;
using IbanNet.Registry.Patterns;
using IbanNet.Validation;

namespace IbanNet.Registry
{
    /// <summary>
    /// Describes an IBAN structure.
    /// </summary>
    [DebuggerStepThrough]
    public class IbanStructure : StructureSection
    {
        internal IbanStructure()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IbanStructure" /> class using specified parameters.
        /// </summary>
        /// <param name="structure">The structure.</param>
        /// <param name="structureValidationFactory">The structure validation factory.</param>
        [Obsolete("Will be removed in v5.0. Use the overload accepting Pattern.")]
        public IbanStructure(string structure, IStructureValidationFactory structureValidationFactory)
            : base(structure, structureValidationFactory)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IbanStructure" /> class using specified parameters.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        public IbanStructure(Pattern pattern)
            : base(pattern)
        {
        }

        /// <summary>
        /// Gets or sets the date the IBAN came in effect.
        /// </summary>
        public DateTimeOffset EffectiveDate { get; set; }
    }
}
