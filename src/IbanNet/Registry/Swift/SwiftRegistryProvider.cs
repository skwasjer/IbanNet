using System.Collections;
using System.Diagnostics;
using IbanNet.CodeGen;

namespace IbanNet.Registry.Swift;

/// <summary>
/// This IBAN registry provider contains IBAN/BBAN/SEPA information for all known IBAN countries.
/// </summary>
public partial class SwiftRegistryProvider : IIbanRegistryProvider
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Lazy<List<IbanCountry>> _countries = new(() => Load().ToList());

    /// <inheritdoc />
    public IEnumerator<IbanCountry> GetEnumerator()
    {
        return _countries.Value.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc />
    public int Count { get => _countries.Value.Count; }

    [RegistrySource(RegistrySource.Swift, @"Files\swift_iban_registry_202412.r99.txt")]
    private static partial IEnumerable<IbanCountry> Load();
}
