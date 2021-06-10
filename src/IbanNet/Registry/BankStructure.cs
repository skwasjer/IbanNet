using System;
using System.Diagnostics;
using IbanNet.Registry.Patterns;
using IbanNet.Validation;

namespace IbanNet.Registry
{
    /// <summary>
    /// Defines a bank section of a structure.
    /// </summary>
    [DebuggerStepThrough]
    public class BankStructure : StructureSection
    {
        internal BankStructure()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BankStructure" /> class using specified parameters.
        /// </summary>
        /// <param name="structure">The structure.</param>
        /// <param name="structureValidationFactory">The structure validation factory.</param>
        [Obsolete("Will be removed in v5.0. Use the overload accepting Pattern.")]
        public BankStructure(string structure, IStructureValidationFactory structureValidationFactory)
            : base(structure, structureValidationFactory)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BankStructure" /> class using specified parameters.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        /// <param name="position">The position where the pattern occurs within the parent structure.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="pattern" /> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="position" /> is less than 0.</exception>
        public BankStructure(Pattern pattern, int position = 0)
            : base(pattern, position)
        {
        }
    }
}
