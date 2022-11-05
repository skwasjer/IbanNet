namespace IbanNet.Registry;

/// <summary>
/// Describes a way to generate test IBAN's.
/// </summary>
public interface IIbanGenerator
{
    /// <summary>
    /// Generates a random IBAN for specified IBAN pattern.
    /// <para>
    /// All characters except the country code and IBAN check digits are generated based on the structure defined for a country. The IBAN check digits are subsequently computed based on the generated data.
    /// </para>
    /// <para>
    /// While the IBAN produced will pass validation, it may not be an actual valid bank account and should ONLY be used for testing purposes.
    /// </para>
    /// </summary>
    /// <param name="countryCode">The country code.</param>
    /// <returns>A new random IBAN for specified <paramref name="countryCode" />.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="countryCode" /> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="countryCode" /> is not found in the registry.</exception>
    /// <exception cref="InvalidOperationException">Thrown when no BBAN pattern is registered for the <paramref name="countryCode" />.</exception>
    Iban Generate(string countryCode);
}