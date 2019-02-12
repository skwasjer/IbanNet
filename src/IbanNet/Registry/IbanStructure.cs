using System;
using System.Diagnostics;

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
		/// Initializes a new instance of the <see cref="IbanStructure"/> class using specified parameters.
		/// </summary>
		/// <param name="structure">The structure.</param>
		// ReSharper disable once UnusedMember.Global
		public IbanStructure(string structure)
			: base(structure)
		{
		}

		/// <summary>
		/// Gets or sets the date the IBAN came in effect.
		/// </summary>
		public DateTimeOffset EffectiveDate { get; set; }
	}
}