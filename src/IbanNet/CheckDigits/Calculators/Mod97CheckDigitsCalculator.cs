using System.Runtime.CompilerServices;
using IbanNet.Extensions;

namespace IbanNet.CheckDigits.Calculators;

/// <summary>
/// Computes check digits using mod 97 algorithm.
/// </summary>
public class Mod97CheckDigitsCalculator : ICheckDigitsCalculator
{
    private const int MaxDigitsBeforeIntegerOverflow = 8;
    private const int Modulo = 97;

    /// <inheritdoc />
    public int Compute(char[] value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        int digits = 0;
        int remainder = 0;
        int length = value.Length;

        // ReSharper disable once ForCanBeConvertedToForeach - justification : performance
        for (int i = 0; i < length; i++)
        {
            char ch = value[i];
            int number = FromBase36(ch);
            if (number < 0)
            {
                throw new InvalidTokenException(i, ch);
            }

            // If the number we got is a two-digit number (i.e. >= 10) we need to shift left by 100, else by 10.
            int numberDigits = 2;
            int base10Shift = 100;
            if (number < 10)
            {
                numberDigits = 1;
                base10Shift = 10;
            }

            remainder = remainder * base10Shift + number;
            if (digits + numberDigits >= MaxDigitsBeforeIntegerOverflow)
            {
                // Compute the new remainder to avoid integer overflow and reset the number of digits.
                remainder %= Modulo;
                digits = remainder < 10 ? 1 : 2;
            }
            else
            {
                digits += numberDigits;
            }
        }

        if (remainder >= Modulo)
        {
            remainder %= Modulo;
        }

        return remainder;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int FromBase36(char ch)
    {
        if (char.IsAsciiDigit(ch))
        {
            // Subtract '0' to get value 0, 1, 2.
            return ch - '0';
        }

        if (char.IsAsciiLetter(ch))
        {
            // - Use bitwise OR with ' ' (space, 0x20) to convert char to lowercase.
            // - Then subtract 'a' to get value 0, 1, 2, etc.
            // - Last, add 10 so: - a = 10, b = 11, c = 12, etc.
            return (ch | ' ') - 'a' + 10;
        }

        return -1;
    }
}
