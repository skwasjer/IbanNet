namespace IbanNet.Registry
{
	/// <summary>
	/// Defines a bank section of a structure.
	/// </summary>
	public class BankStructure : StructureSection
	{
		internal BankStructure()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BankStructure"/> class using specified parameters.
		/// </summary>
		/// <param name="structure">The structure.</param>
		// ReSharper disable once UnusedMember.Global
		public BankStructure(string structure)
			: base(structure)
		{
		}
	}
}