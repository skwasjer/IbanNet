using System;
using System.Diagnostics;
using IbanNet.Registry.Patterns;

namespace IbanNet.Registry
{
    /// <summary>
    /// Describes an IBAN structure.
    /// </summary>
    [DebuggerStepThrough]
    public class IbanStructure : StructureSection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IbanStructure" /> class using specified parameters.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        public IbanStructure(Pattern pattern)
            : base(pattern)
        {
        }

        /// <summary>
        /// Gets the date the IBAN came in effect.
        /// </summary>
        public DateTimeOffset EffectiveDate { get; init; }
    }
}
