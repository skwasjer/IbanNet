namespace IbanNet.CheckDigits.Calculators;

/// <summary>
/// Describes a calculator which computes check digits for a given input string.
/// </summary>
#pragma warning disable S1133
[Obsolete("Will be removed in 6.x.")]
#pragma warning restore S1133
public interface ICheckDigitsCalculator
{
    /// <summary>
    /// Returns the check digits for specified <paramref name="value" />.
    /// </summary>
    /// <param name="value">The input buffer to compute check digits for.</param>
    /// <returns>The check digits.</returns>
    /// <exception cref="InvalidTokenException">Thrown when an invalid character was encountered.</exception>
    int Compute(char[] value);
}
