#if !NET8_0_OR_GREATER
using IbanNet.Extensions;
#endif

namespace IbanNet.CheckDigits.Calculators;

/// <summary>
/// Computes check digits using mod 97 algorithm.
/// </summary>
#pragma warning disable S1133
[Obsolete("Will be removed in 6.x.")]
#pragma warning restore S1133
public class Mod97CheckDigitsCalculator : ICheckDigitsCalculator
{
    /// <inheritdoc />
    public int Compute(char[] value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        uint current = 0;
        int length = value.Length;

        // ReSharper disable once ForCanBeConvertedToForeach - justification : performance
        for (int i = 0; i < length; i++)
        {
            char ch = value[i];
#if NET8_0_OR_GREATER
            if (char.IsAsciiDigit(ch))
#else
            if (ch.IsAsciiDigit())
#endif
            {
                // - Shift by 1 digit
                // - Subtract '0' to get value 0, 1, 2.
                current = unchecked(((current * 10) + ch - '0') % 97);
            }
#if NET8_0_OR_GREATER
            else if (char.IsAsciiLetter(ch))
#else
            else if (ch.IsAsciiLetter())
#endif
            {
                // - For letters, always is two digits so shift 2 digits.
                // - Use bitwise OR with ' ' (space, 0x20) to convert char to lowercase.
                // - Then subtract 'a' to get value 0, 1, 2, etc.
                // - Last, add 10 so: - a = 10, b = 11, c = 12, etc.
                current = unchecked(((current * 100) + (uint)(ch | ' ') - 'a' + 10) % 97);
            }
            else
            {
                throw new InvalidTokenException(i, ch);
            }
        }

        return (int)current;
    }
}
