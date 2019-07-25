using System.Collections.Generic;
using IbanNet.Registry;

namespace IbanNet
{
	/// <summary>
	/// Describes the countries that a validator supports.
	/// </summary>
	internal interface ICountryValidationSupport
	{
		/// <summary>
		/// Gets the supported countries.
		/// </summary>
		IReadOnlyDictionary<string, CountryInfo> SupportedCountries { get; }
	}
}