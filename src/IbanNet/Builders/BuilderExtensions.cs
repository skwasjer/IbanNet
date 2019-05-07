using System;
using IbanNet.Registry;

namespace IbanNet.Builders
{
    /// <summary>
    /// Extensions for builders.
    /// </summary>
    public static class BuilderExtensions
    {
        /// <summary>
        /// Gets an <see cref="IbanBuilder"/> for this country.
        /// </summary>
        /// <param name="ibanCountry">The country.</param>
        /// <returns>An instance of <see cref="IbanBuilder"/> for the country specified in <paramref name="ibanCountry"/>.</returns>
        public static IbanBuilder GetIbanBuilder(this IbanCountry ibanCountry)
        {
            return (IbanBuilder)new IbanBuilder().WithCountry(ibanCountry);
        }

        /// <summary>
        /// Gets an <see cref="BbanBuilder"/> for this country.
        /// </summary>
        /// <param name="ibanCountry">The country.</param>
        /// <returns>An instance of <see cref="BbanBuilder"/> for the country specified in <paramref name="ibanCountry"/>.</returns>
        public static BbanBuilder GetBbanBuilder(this IbanCountry ibanCountry)
        {
            return (BbanBuilder)new BbanBuilder().WithCountry(ibanCountry);
        }

        /// <summary>
        /// Adds the specified <paramref name="countryCode"/> to the builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="countryCode">The country code.</param>
        /// <param name="registry">The IBAN registry to resolve the country from.</param>
        /// <returns>The builder to continue chaining.</returns>
        public static IBankAccountBuilder WithCountry(this IBankAccountBuilder builder, string countryCode, IIbanRegistry registry)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (registry is null)
            {
                throw new ArgumentNullException(nameof(registry));
            }

            if (!registry.TryGetValue(countryCode, out IbanCountry country))
            {
                throw new ArgumentException(Resources.ArgumentException_Builder_The_country_code_is_not_registered, nameof(countryCode));
            }

            return builder.WithCountry(country);
        }
    }
}
