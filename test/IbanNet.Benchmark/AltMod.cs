using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace IbanNet.Benchmark;

internal static class AltMod
{ 
    /// <summary>Checks the Mod97 constraint.</summary>
    [Pure]
    public static int Mod97(char[] iban)
    {
        ulong num = 0;

        // Calculate the first 4 characters (country and checksum) last
        for (var i = 4; i < iban.Length; i++)
        {
            num = Next(num, iban[i]);

            // If we wait longer, we could overflow.
            if (num >> 57 is not 0) num %= 97;
        }

        // If we wait longer, we could overflow.
        if (num >> 44 is not 0) num %= 97;

        for (var i = 0; i < 4; i++)
        {
            num = Next(num, iban[i]);
        }

        return (int)(num % 97);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static ulong Next(ulong num, char ch)
            => ch <= '9'
            ? (num * 10) + ch - '0'
            : (num * 100) + ch - 'A' + 10;
    }
}
