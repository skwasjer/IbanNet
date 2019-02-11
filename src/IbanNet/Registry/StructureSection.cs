namespace IbanNet.Registry
{
	/// <summary>
	/// Defines a section of a structure.
	/// </summary>
	internal class StructureSection : IStructureSection
	{
		/// <inheritdoc />
		public int Position { get; internal set; }

		/// <inheritdoc />
		public int Length { get; internal set; }

		/// <inheritdoc />
		public string Example { get; internal set; }

		/// <inheritdoc />
		public string Structure { get; internal set; }
	}
}