namespace IbanNet.Registry
{
	/// <summary>
	/// Describes a section of a structure.
	/// </summary>
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
		string Structure { get; }
	}
}