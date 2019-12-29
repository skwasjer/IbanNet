using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace IbanNet.Registry
{
	/// <summary>
	/// </summary>
	public class IbanRegistry : IIbanRegistry
	{
		/// <summary>
		/// Gets the default IBAN registry initialized with all the built-in countries.
		/// </summary>
		public static IbanRegistry Default { get; } = new IbanRegistry();

		/// <summary>
		/// Gets the registry mapped as dictionary by country code.
		/// </summary>
		public IDictionary<string, IbanCountry> Dictionary { get; }

		/// <summary>
		/// Initializes a new instance of <see cref="IbanRegistry" /> initialized with all the built-in countries.
		/// </summary>
		public IbanRegistry()
			: this(new IbanRegistryProvider())
		{
		}

		/// <summary>
		/// Initializes a new instance of <see cref="IbanRegistry" /> initialized by using specified provider.
		/// </summary>
		public IbanRegistry(IIbanRegistryProvider registryProvider)
			: this(registryProvider.Load())
		{
		}

		/// <summary>
		/// Initializes a new instance of <see cref="IbanRegistry" /> initialized with specified <paramref name="countries" />.
		/// </summary>
		public IbanRegistry(IEnumerable<IbanCountry> countries)
			: this(countries.ToDictionary(c => c.TwoLetterISORegionName))
		{
		}

		/// <summary>
		/// Initializes a new instance of <see cref="IbanRegistry" /> initialized with specified <paramref name="countries" />.
		/// </summary>
		public IbanRegistry(IEnumerable<KeyValuePair<string, IbanCountry>> countries)
		{
			Dictionary = new ReadOnlyDictionary<string, IbanCountry>(countries.ToDictionary());
		}

		/// <inheritdoc />
		public IEnumerator<IbanCountry> GetEnumerator() => Dictionary.Values.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		/// <inheritdoc />
		public int Count => Dictionary.Count;
	}
}
