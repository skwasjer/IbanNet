using System.Globalization;
using IbanNet.Registry;

namespace IbanNet.Builders;

/// <summary>
/// Extensions for builders.
/// </summary>
public static class BuilderExtensions
{
    /// <summary>
    /// Gets an <see cref="IbanBuilder" /> for this country.
    /// </summary>
    /// <param name="country">The country.</param>
    /// <returns>An instance of <see cref="IbanBuilder" /> for the country specified in <paramref name="country" />.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="country" /> is null.</exception>
    public static IbanBuilder GetIbanBuilder(this IbanCountry country)
    {
        return (IbanBuilder)new IbanBuilder().WithCountry(country);
    }

    /// <summary>
    /// Gets an <see cref="BbanBuilder" /> for this country.
    /// </summary>
    /// <param name="country">The country.</param>
    /// <returns>An instance of <see cref="BbanBuilder" /> for the country specified in <paramref name="country" />.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="country" /> is null.</exception>
    public static BbanBuilder GetBbanBuilder(this IbanCountry country)
    {
        return (BbanBuilder)new BbanBuilder().WithCountry(country);
    }

    /// <summary>
    /// Adds the specified <paramref name="countryCode" /> to the builder.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="countryCode">The country code.</param>
    /// <param name="registry">The IBAN registry to resolve the country from.</param>
    /// <returns>The builder to continue chaining.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="countryCode" /> or <paramref name="registry" /> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="countryCode" /> is not defined in the <paramref name="registry" />.</exception>
    public static IBankAccountBuilder WithCountry(this IBankAccountBuilder builder, string countryCode, IIbanRegistry registry)
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        if (countryCode is null)
        {
            throw new ArgumentNullException(nameof(countryCode));
        }

        if (registry is null)
        {
            throw new ArgumentNullException(nameof(registry));
        }

        if (!registry.TryGetValue(countryCode, out IbanCountry? country))
        {
            throw new ArgumentException(string.Format(
                    CultureInfo.CurrentCulture,
                    Resources.ArgumentException_The_country_code_0_is_not_registered,
                    countryCode),
                nameof(countryCode)
            );
        }

        return builder.WithCountry(country);
    }
}