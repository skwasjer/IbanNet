using System.Runtime.CompilerServices;
using IbanNet.Extensions;

namespace IbanNet.CheckDigits;

/// <summary>
/// This Mod-97,10 implementation expects a Base36-encoded string/buffer.
/// </summary>
internal static class Mod9710
{
    private const int MaxDigitsBeforeIntegerOverflow = 8;
    private const int Modulo = 97;

#if USE_SPANS
    internal static int Compute(ReadOnlySpan<char> value)
    {
        return Compute(value, value.Length);
    }
#else
    internal static unsafe int Compute(char[] value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        fixed (char* ptr = value)
        {
            return Compute(ptr, value.Length);
        }
    }

    internal static unsafe int Compute(string value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        fixed (char* ptr = value)
        {
            return Compute(ptr, value.Length);
        }
    }
#endif

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if USE_SPANS
    private static int Compute(ReadOnlySpan<char> buffer, int length)
#else
    private static unsafe int Compute(char* buffer, int length)
#endif
    {
        // We start processing the buffer from where the BBAN starts at position 4 (0-based index).
        // Once we hit the end of the buffer, we then wrap around and process the first 4 chars of the buffer.
        const int bbanStart = 4;
        int cursor = bbanStart - 1;
        int max = length - 1;

        int digits = 0;
        int remainder = 0;

        for (int i = 0; i < length; i++)
        {
            if (cursor < max)
            {
                cursor++;
            }
            else
            {
                // Restart from the beginning.
                cursor = 0;
            }

            char ch = buffer[cursor];
            int number = FromBase36(ch);
            if (number < 0)
            {
                throw new InvalidTokenException(cursor, ch);
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
