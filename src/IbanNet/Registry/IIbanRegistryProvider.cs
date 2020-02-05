using System.Collections.Generic;
using IbanNet.Validation;

namespace IbanNet.Registry
{
	/// <summary>
	/// Provides a way to load IBAN registry data.
	/// </summary>
	public interface IIbanRegistryProvider
	{
		/// <summary>
		/// Gets the structure validation factory to use when validating IBAN's matching any country code provided by <see cref="Load"/>.
		/// </summary>
		IStructureValidationFactory StructureValidationFactory { get; }

		/// <summary>
		/// Builds an enumerable of <see cref="IbanCountry" />.
		/// </summary>
		/// <returns>an enumerable of <see cref="IbanCountry" />.</returns>
		IEnumerable<IbanCountry> Load();
	}
}
