using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using IbanNet.Validation;

namespace IbanNet.Registry
{
	internal class IbanRegistryListProvider : IIbanRegistryProvider
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly ICollection<IbanCountry> _countries;

		/// <summary>
		/// Initializes a new instance of <see cref="IbanRegistry" /> initialized with specified <paramref name="countries" />.
		/// </summary>
		public IbanRegistryListProvider(IEnumerable<IbanCountry> countries, IStructureValidationFactory structureValidationFactory)
		{
			_countries = countries?.ToList() ?? throw new ArgumentNullException(nameof(countries));
			StructureValidationFactory = structureValidationFactory ?? throw new ArgumentNullException(nameof(structureValidationFactory));
		}

		public IStructureValidationFactory StructureValidationFactory { get; }

		public IEnumerator<IbanCountry> GetEnumerator()
		{
			return _countries.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public int Count => _countries.Count;
	}
}
