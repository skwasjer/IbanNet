using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace IbanNet.Registry
{
	/// <summary>
	/// Represents SEPA information.
	/// </summary>
	[DebuggerStepThrough]
	public class SepaInfo
	{
		/// <summary>
		/// Gets or sets whether this region is a SEPA country.
		/// </summary>
		public bool IsMember { get; set; }

		/// <summary>
		/// Gets or sets a list of included SEPA countries.
		/// </summary>
		public IReadOnlyCollection<string> IncludedCountries { get; set; } = new ReadOnlyCollection<string>(new string[0]);
	}
}