using System.Collections.Generic;
using IbanNet.Validation;

namespace IbanNet.Registry
{
	/// <summary>
	/// Provides IBAN registry data.
	/// </summary>
	public interface IIbanRegistryProvider : IReadOnlyCollection<IbanCountry>
	{
		/// <summary>
		/// Gets the structure validation factory to use when validating IBAN's matching any country code from this provider.
		/// </summary>
		IStructureValidationFactory StructureValidationFactory { get; }
	}
}
