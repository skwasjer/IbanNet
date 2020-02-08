using System.Collections.Generic;

namespace IbanNet.Registry
{
	/// <summary>
	/// Provides IBAN registry data.
	/// </summary>
	public interface IIbanRegistryProvider : IReadOnlyCollection<IbanCountry>
	{
	}
}
