using System.Diagnostics;
using IbanNet.Registry.Patterns;

namespace IbanNet.Registry;

/// <summary>
/// Defines a branch section of a structure.
/// </summary>
[DebuggerStepThrough]
public class BranchStructure : StructureSection
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BranchStructure" /> class using specified parameters.
    /// </summary>
    /// <param name="pattern">The pattern.</param>
    /// <param name="position">The position where the pattern occurs within the parent structure.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="pattern" /> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="position" /> is less than 0.</exception>
    public BranchStructure(Pattern pattern, int position = 0)
        : base(pattern, position)
    {
    }
}