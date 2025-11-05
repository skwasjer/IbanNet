using System.Collections.ObjectModel;

namespace IbanNet.Registry;

/// <summary>
/// Extensions for <see cref="IIbanRegistry" />.
/// </summary>
public static class IbanRegistryExtensions
{
    /// <summary>
    /// Returns a new registry that excludes the specified <paramref name="countryCodes" />.
    /// </summary>
    /// <remarks>The source <paramref name="registry" /> from which this is derived is NOT modified.</remarks>
    /// <param name="registry">The source registry to filter and exclude the specified countries from.</param>
    /// <param name="countryCodes">The 2-letter country codes of the countries to exclude. If <see langword="null" /> or empty, the source registry will be returned unchanged.</param>
    /// <returns>A new registry instance excluding the specified <paramref name="countryCodes" />.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="registry" /> is <see langword="null" />.</exception>
    public static IIbanRegistry ExcludingCountries(this IIbanRegistry registry, params string[] countryCodes)
    {
        if (registry is null)
        {
            throw new ArgumentNullException(nameof(registry));
        }

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (countryCodes is null || countryCodes.Length == 0)
        {
            return registry;
        }

        return new IbanRegistry(c => !countryCodes.Contains(c.TwoLetterISORegionName, StringComparer.OrdinalIgnoreCase))
        {
            Providers = registry.Providers.IsReadOnly
                ? registry.Providers
                : new ReadOnlyCollection<IIbanRegistryProvider>(registry.Providers)
        };
    }

    /// <summary>
    /// Returns a new registry that only includes the specified <paramref name="countryCodes" />.
    /// </summary>
    /// <remarks>The source <paramref name="registry" /> from which this is derived is NOT modified.</remarks>
    /// <param name="registry">The source registry to filter and only include the specified countries from.</param>
    /// <param name="countryCodes">The 2-letter country codes of the countries to include. If <see langword="null" /> or empty, the source registry will be returned unchanged.</param>
    /// <returns>A new registry instance including only the specified <paramref name="countryCodes" />.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="registry" /> is <see langword="null" />.</exception>
    public static IIbanRegistry IncludingCountries(this IIbanRegistry registry, params string[] countryCodes)
    {
        if (registry is null)
        {
            throw new ArgumentNullException(nameof(registry));
        }

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (countryCodes is null || countryCodes.Length == 0)
        {
            return registry;
        }

        return new IbanRegistry(c => countryCodes.Contains(c.TwoLetterISORegionName, StringComparer.OrdinalIgnoreCase))
        {
            Providers = registry.Providers.IsReadOnly
                ? registry.Providers
                : new ReadOnlyCollection<IIbanRegistryProvider>(registry.Providers)
        };
    }
}
