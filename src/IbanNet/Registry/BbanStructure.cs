using System;
using System.Diagnostics;
using IbanNet.Validation;

namespace IbanNet.Registry
{
    /// <summary>
    /// Contains information about the BBAN structure.
    /// </summary>
    [DebuggerStepThrough]
    public class BbanStructure : StructureSection
    {
        internal BbanStructure()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureSection" /> class using specified parameters.
        /// </summary>
        /// <param name="structure">The structure.</param>
        /// <param name="structureValidationFactory">The structure validation factory.</param>
        [Obsolete("Will be removed in v5.0. Use the overload accepting Pattern.")]
        public BbanStructure(string structure, IStructureValidationFactory structureValidationFactory)
            : base(structure, structureValidationFactory)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BbanStructure" /> class using specified parameters.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        /// <param name="position">The position where the pattern occurs within the parent structure.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="pattern" /> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="position" /> is less than 0.</exception>
        public BbanStructure(Pattern pattern, int position = 0)
            : base(pattern, position)
        {
        }
    }
}
