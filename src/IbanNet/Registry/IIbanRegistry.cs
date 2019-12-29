using System.Collections.Generic;

namespace IbanNet.Registry
{
	/// <summary>
	/// Represents the IBAN registry used by the validator.
	/// </summary>
	public interface IIbanRegistry : IReadOnlyCollection<IbanCountry>
	{
		/// <summary>
		/// Gets the registry mapped as dictionary by country code.
		/// </summary>
		IDictionary<string, IbanCountry> Dictionary { get; }
	}
}
