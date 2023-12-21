using System.Collections;
#if NET8_0_OR_GREATER
using System.Collections.Frozen;
#endif
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using IbanNet.Registry.Swift;

namespace IbanNet.Registry;

/// <summary>
/// Represents a registry of IBAN countries.
/// </summary>
public class IbanRegistry : IIbanRegistry
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private IDictionary<string, IbanCountry>? _dictionary;
    private readonly object _syncObject = new();

    /// <summary>
    /// Gets the default IBAN registry initialized with all the built-in countries.
    /// </summary>
    public static IbanRegistry Default { get; } = new()
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

    /// <summary>
    /// Gets the registry providers.
    /// </summary>
    /// <remarks>Note: after using any of the registry members (iow. getting country information), you can no longer modify this list.</remarks>
    public IList<IIbanRegistryProvider> Providers { get; internal set; } = new List<IIbanRegistryProvider>();

    /// <inheritdoc />
    // ReSharper disable once InconsistentNaming
    public bool TryGetValue(string twoLetterISORegionName, [NotNullWhen(true)] out IbanCountry? country) => Dictionary.TryGetValue(twoLetterISORegionName, out country);

    /// <inheritdoc />
    // ReSharper disable once InconsistentNaming
    public IbanCountry this[string twoLetterISORegionName] => Dictionary[twoLetterISORegionName];

    /// <summary>
    /// Gets the registry mapped as dictionary by country code.
    /// </summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private IDictionary<string, IbanCountry> Dictionary
    {
        get
        {
            if (_dictionary is not null)
            {
                return _dictionary;
            }

            lock (_syncObject)
            {
                if (_dictionary is not null)
                {
                    return _dictionary;
                }

                var readOnlyProviders = new ReadOnlyCollection<IIbanRegistryProvider>(Providers.ToArray());
                try
                {
                    IEnumerable<KeyValuePair<string, IbanCountry>> countries = readOnlyProviders
                        .SelectMany(p => p)
                        // In case of duplicate country codes, select the first.
                        .GroupBy(c => c.TwoLetterISORegionName)
                        .Select(g => new KeyValuePair<string, IbanCountry>(g.Key, g.First()));
#if NET8_0_OR_GREATER
                    return _dictionary = countries.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase);
#else
                    return _dictionary = new ReadOnlyDictionary<string, IbanCountry>(countries
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value,
                            StringComparer.OrdinalIgnoreCase
                        )
                    );
#endif
                }
                finally
                {
                    // Freeze providers, so that the collection can no longer be modified after hydrating the registry.
                    Providers = readOnlyProviders;
                }
            }
        }
    }
}
