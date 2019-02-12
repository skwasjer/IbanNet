using System.Diagnostics;

namespace IbanNet.Registry
{
	/// <summary>
	/// Defines a branch section of a structure.
	/// </summary>
	[DebuggerStepThrough]
	public class BranchStructure : StructureSection
	{
		internal BranchStructure()
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="BranchStructure"/> class using specified parameters.
		/// </summary>
		/// <param name="structure">The structure.</param>
		// ReSharper disable once UnusedMember.Global
		public BranchStructure(string structure)
			: base(structure)
		{
		}
	}
}