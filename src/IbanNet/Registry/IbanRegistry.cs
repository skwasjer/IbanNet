using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace IbanNet.Registry
{
	/// <summary>
	/// Represents a registry of IBAN countries.
	/// </summary>
	public class IbanRegistry : IIbanRegistry
	{
		private IDictionary<string, IbanCountry>? _dictionary;

		/// <summary>
		/// Gets the default IBAN registry initialized with all the built-in countries.
		/// </summary>
		public static IbanRegistry Default { get; } = new IbanRegistry
		{
			// Read-only, so default can not be modified.
			Providers = new ReadOnlyCollection<IIbanRegistryProvider>(new IIbanRegistryProvider[] { new SwiftRegistryProvider() })
		};

		/// <summary>
		/// Initializes a new instance of <see cref="IbanRegistry" />.
		/// </summary>
		// ReSharper disable once EmptyConstructor
		public IbanRegistry()
		{
		}

		/// <inheritdoc />
		public IEnumerator<IbanCountry> GetEnumerator() => Dictionary.Values.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		/// <inheritdoc />
		public int Count => Dictionary.Count;

		/// <inheritdoc />
		public IList<IIbanRegistryProvider> Providers { get; internal set; } = new List<IIbanRegistryProvider>();

		/// <inheritdoc />
		// ReSharper disable once InconsistentNaming
		public bool TryGetValue(string twoLetterISORegionName, out IbanCountry country) => Dictionary.TryGetValue(twoLetterISORegionName, out country);

		/// <inheritdoc />
		// ReSharper disable once InconsistentNaming
		public IbanCountry this[string twoLetterISORegionName] => Dictionary[twoLetterISORegionName];

		/// <summary>
		/// Gets the registry mapped as dictionary by country code.
		/// </summary>
		private IDictionary<string, IbanCountry> Dictionary
		{
			get
			{
				return _dictionary ??= new ReadOnlyDictionary<string, IbanCountry>(Providers
					.SelectMany(p => p)
					// In case of duplicate country codes, select the first.
					.GroupBy(c => c.TwoLetterISORegionName)
					.ToDictionary(g => g.Key, g => g.First())
				);
			}
		}
	}
}
