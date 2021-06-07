using System;
using System.Collections.Generic;
using System.Linq;
using IbanNet.Registry;

namespace IbanNet.Validation
{
    /// <summary>
    /// Wraps one or more providers and selects the first found <see cref="IStructureValidationFactory" /> for a given country code.
    /// </summary>
    internal class CompositeStructureValidationFactory : IStructureValidationFactory
    {
        private readonly IDictionary<string, IStructureValidationFactory> _structureValidationFactoriesByCountry;

        public CompositeStructureValidationFactory(IEnumerable<IIbanRegistryProvider> providers)
        {
            if (providers is null)
            {
                throw new ArgumentNullException(nameof(providers));
            }

            _structureValidationFactoriesByCountry = new Dictionary<string, IStructureValidationFactory>();
            InitStructureValidationFactories(providers, _structureValidationFactoriesByCountry);
        }

        public IStructureValidator CreateValidator(string twoLetterISORegionName, string pattern)
        {
            if (_structureValidationFactoriesByCountry.TryGetValue(twoLetterISORegionName, out IStructureValidationFactory? factory))
            {
                return factory.CreateValidator(twoLetterISORegionName, pattern);
            }

            throw new InvalidOperationException($"No structure validation factory for country code '{twoLetterISORegionName}'.");
        }

        private static void InitStructureValidationFactories(IEnumerable<IIbanRegistryProvider> providers, IDictionary<string, IStructureValidationFactory> factories)
        {
            foreach (IIbanRegistryProvider provider in providers)
            {
                foreach (IbanCountry country in provider)
                {
                    if (country.Iban.ValidationFactory is { } && !factories.ContainsKey(country.TwoLetterISORegionName))
                    {
                        factories.Add(country.TwoLetterISORegionName, country.Iban.ValidationFactory);
                    }
                }
            }
        }
    }
}
