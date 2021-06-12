using System;

namespace IbanNet.Registry
{
    /// <summary>
    /// Describes a section of a structure.
    /// </summary>
    [Obsolete("Will be removed in v5.0.")]
    public interface IStructureSection
    {
        /// <summary>
        /// Gets the position within the structure.
        /// </summary>
        int Position { get; }

        /// <summary>
        /// Gets the section length.
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Gets the section example.
        /// </summary>
        string Example { get; }

        /// <summary>
        /// Gets the structure.
        /// </summary>
#pragma warning disable CA1716 // Identifiers should not match keywords - justification: renaming would break API contract. Alternative could be 'Pattern'. Fix in next major version
        string Structure { get; }
#pragma warning restore CA1716 // Identifiers should not match keywords
    }
}
