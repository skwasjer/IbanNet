using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace IbanNet.Registry
{
    /// <summary>
    /// Represents the IBAN registry used by the validator.
    /// </summary>
    public interface IIbanRegistry : IReadOnlyCollection<IbanCountry>
    {
        /// <summary>
        /// Gets the registry providers.
        /// </summary>
        IList<IIbanRegistryProvider> Providers { get; }

        /// <summary>
        /// Tries to get the <see cref="IbanCountry" /> by <paramref name="twoLetterISORegionName" />.
        /// </summary>
        /// <param name="twoLetterISORegionName">The 2 letter ISO region name.</param>
        /// <param name="country"></param>
        /// <returns>true if the country was found, false otherwise</returns>
        // ReSharper disable once InconsistentNaming
        bool TryGetValue(string twoLetterISORegionName, [NotNullWhen(true)] out IbanCountry? country);

        /// <summary>
        /// Gets the <see cref="IbanCountry" /> by <paramref name="twoLetterISORegionName" />.
        /// </summary>
        /// <param name="twoLetterISORegionName">The 2 letter ISO region name.</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException">Thrown when the country is not found by the provided <paramref name="twoLetterISORegionName" />.</exception>
        // ReSharper disable once InconsistentNaming
        IbanCountry this[string twoLetterISORegionName] { get; }
    }
}
