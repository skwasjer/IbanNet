using System.Collections.Generic;

namespace IbanNet.Registry
{
	/// <summary>
	/// Provides a way to load IBAN registry data.
	/// </summary>
	public interface IIbanRegistryProvider
	{
		/// <summary>
		/// Builds an enumerable of <see cref="IbanCountry" />.
		/// </summary>
		/// <returns>an enumerable of <see cref="IbanCountry" />.</returns>
		IEnumerable<IbanCountry> Load();
	}
}
