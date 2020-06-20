using System;
using System.Diagnostics;
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
        // ReSharper disable once UnusedMember.Global
        public IbanStructure(string structure, IStructureValidationFactory structureValidationFactory)
            : base(structure, structureValidationFactory)
        {
        }

        /// <summary>
        /// Gets or sets the date the IBAN came in effect.
        /// </summary>
        public DateTimeOffset EffectiveDate { get; set; }
    }
}
