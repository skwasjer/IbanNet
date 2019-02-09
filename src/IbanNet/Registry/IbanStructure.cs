using System;
using System.Diagnostics;

namespace IbanNet.Registry
{
	/// <summary>
	/// Describes an IBAN structure.
	/// </summary>
	public class IbanStructure : IStructureSection
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
		/// Gets the date the IBAN came in effect.
		/// </summary>
		public DateTimeOffset EffectiveDate { get; internal set; }
	}
}