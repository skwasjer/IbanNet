using System.Diagnostics;

namespace IbanNet.Registry
{
	/// <summary>
	/// Contains information about the BBAN structure.
	/// </summary>
	public class BbanStructure : IStructureSection
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		// Not relevant.
		int IStructureSection.Position { get; } = 0;

		/// <inheritdoc />
		public int Length { get; internal set; }

		/// <inheritdoc />
		public string Example { get; internal set; }

		/// <inheritdoc />
		public string Structure { get; internal set; }

		/// <summary>
		/// Gets the bank identifier structure section.
		/// </summary>
		public IStructureSection Bank { get; internal set; }

		/// <summary>
		/// Gets the branch identifier structure section.
		/// </summary>
		public IStructureSection Branch { get; internal set; }
	}
}