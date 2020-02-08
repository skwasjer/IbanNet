using System;
using System.Numerics;
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
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			var current = new BigInteger();

			// 'intermediate' is only used for math up until we reach 18 or more digits (before overflow).
			// Then, we shift 'current' n 'digits' to left (in base 10) and add 'intermediate' to it.
			// This means we can do most math using ulong, instead of with slower BigInteger.
			const int maxDigits = 18;
			int digits = 0;
			ulong intermediate = 0;

			void AddToCurrent()
			{
				current = current * (ulong)Power(10, digits) + intermediate;
			}

			// ReSharper disable once ForCanBeConvertedToForeach - justification : performance
			for (int i = 0; i < value.Length; i++)
			{
				char c = value[i];
				if (c.IsAsciiDigit())
				{
					// - Shift by 1 digit
					// - Subtract '0' to get value 0, 1, 2.
					intermediate = intermediate * 10 + c - '0';
					digits++;
				}
				else if (c.IsAsciiLetter())
				{
					// - For letters, always is two digits so shift 2 digits.
					// - Use bitwise OR with ' ' (space, 0x20) to convert char to lowercase.
					// - Then subtract 'a' to get value 0, 1, 2, etc.
					// - Last, add 10 so: - a = 10, b = 11, c = 12, etc.
					intermediate = intermediate * 100 + (uint)(c | ' ') - 'a' + 10;
					digits += 2;
				}
				else
				{
					throw new InvalidTokenException(i, c);
				}

				// Once we reach threshold, store intermediate and reset.
				if (digits >= maxDigits)
				{
					AddToCurrent();
					intermediate = 0;
					digits = 0;
				}
			}

			// Add remainder.
			if (digits > 0)
			{
				AddToCurrent();
			}

			return (int)(current % 97);
		}

		private static long Power(long @base, int exponent)
		{
			long result = 1L;
			while (exponent > 0)
			{
				if ((exponent & 1) == 0)
				{
					@base *= @base;
					exponent >>= 1;
				}
				else
				{
					result *= @base;
					--exponent;
				}
			}

			return result;
		}
	}
}
