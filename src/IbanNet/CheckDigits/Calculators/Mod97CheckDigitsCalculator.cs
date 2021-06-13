using System;
using IbanNet.Extensions;

namespace IbanNet.CheckDigits.Calculators
{
    /// <summary>
    /// Computes check digits using mod 97 algorithm.
    /// </summary>
    public class Mod97CheckDigitsCalculator : ICheckDigitsCalculator
    {
        /// <inheritdoc />
        public int Compute(char[] value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            ulong current = 0;

            // ReSharper disable once ForCanBeConvertedToForeach - justification : performance
            for (int i = 0; i < value.Length; i++)
            {
                char c = value[i];
                if (c.IsAsciiDigit())
                {
                    // - Shift by 1 digit
                    // - Subtract '0' to get value 0, 1, 2.
                    current = unchecked((current * 10 + c - '0') % 97);
                }
                else if (c.IsAsciiLetter())
                {
                    // - For letters, always is two digits so shift 2 digits.
                    // - Use bitwise OR with ' ' (space, 0x20) to convert char to lowercase.
                    // - Then subtract 'a' to get value 0, 1, 2, etc.
                    // - Last, add 10 so: - a = 10, b = 11, c = 12, etc.
                    current = unchecked((current * 100 + (uint)(c | ' ') - 'a' + 10) % 97);
                }
                else
                {
                    throw new InvalidTokenException(i, c);
                }
            }

            return (int)current;
        }
    }
}
