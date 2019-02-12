using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IbanNet.Registry
{
	/// <summary>
	/// Represents SEPA information.
	/// </summary>
	public class SepaInfo
	{
		/// <summary>
		/// Gets whether this region is a SEPA country.
		/// </summary>
		public bool IsMember { get; set; }

		/// <summary>
		/// Gets a list of included SEPA countries.
		/// </summary>
		public IReadOnlyCollection<string> IncludedCountries { get; set; } = new ReadOnlyCollection<string>(new string[0]);
	}
}