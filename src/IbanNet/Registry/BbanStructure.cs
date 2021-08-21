using System;
using System.Diagnostics;
using IbanNet.Registry.Patterns;

namespace IbanNet.Registry
{
    /// <summary>
    /// Contains information about the BBAN structure.
    /// </summary>
    [DebuggerStepThrough]
    public class BbanStructure : StructureSection
    {
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
