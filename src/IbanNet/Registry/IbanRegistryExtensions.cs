using System.Collections.Generic;
using System.Linq;

namespace IbanNet.Registry
{
	/// <summary>
	/// Extensions for <see cref="IIbanRegistry" />.
	/// </summary>
	internal static class IbanRegistryExtensions
	{
		/// <summary>
		/// Returns the enumerable of <see cref="IbanCountry" /> as a writable dictionary.
		/// </summary>
		/// <param name="countries">The enumerable of IBAN countries.</param>
		/// <returns>A writable dictionary.</returns>
		public static IDictionary<string, IbanCountry> ToDictionary(this IEnumerable<KeyValuePair<string, IbanCountry>> countries)
		{
			if (countries is IDictionary<string, IbanCountry> dictionary && !dictionary.IsReadOnly)
			{
				return dictionary;
			}

			return countries.ToDictionary(
				c => c.Key,
				c => c.Value
			);
		}
	}
}
