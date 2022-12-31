namespace IbanNet.Registry;

/// <summary>
/// Provides IBAN registry data.
/// </summary>
#pragma warning disable CA1710 // Identifiers should have correct suffix
public interface IIbanRegistryProvider : IReadOnlyCollection<IbanCountry>
#pragma warning restore CA1710 // Identifiers should have correct suffix
{
}
