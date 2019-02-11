using System.Collections.Generic;

namespace IbanNet.Registry
{
	/// <summary>
	/// Represents SEPA information.
	/// </summary>
	public class SepaInfo
	{
		internal SepaInfo()
		{
			IncludedCountries = new CountryInfo[0];
		}

		/// <summary>
		/// Gets whether this region is a SEPA country.
		/// </summary>
		public bool IsMember { get; internal set; }

		/// <summary>
		/// Gets a list of included SEPA countries.
		/// </summary>
		public IReadOnlyCollection<CountryInfo> IncludedCountries { get; internal set; }
	}
}