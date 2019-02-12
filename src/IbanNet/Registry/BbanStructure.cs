using System.Diagnostics;

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
		/// Initializes a new instance of the <see cref="StructureSection"/> class using specified parameters.
		/// </summary>
		/// <param name="structure">The structure.</param>
		// ReSharper disable once UnusedMember.Global
		public BbanStructure(string structure)
			: base(structure)
		{
		}

		/// <summary>
		/// Gets the bank identifier structure section.
		/// </summary>
		public BankStructure Bank { get; set; }

		/// <summary>
		/// Gets the branch identifier structure section.
		/// </summary>
		public BranchStructure Branch { get; set; }
	}
}