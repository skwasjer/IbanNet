using System;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace IbanNet.CheckDigits.Calculators
{
	/// <summary>
	/// Computes the expected national check digits for French bank account numbers, aka. clé RIB (Relevé d'identité bancaire).
	/// </summary>
	/// <remarks>
	/// https://fr.wikipedia.org/wiki/Cl%C3%A9_RIB
	/// </remarks>
	internal class CleRibCheckDigitsCalculator : CheckDigitsCalculator
	{
		protected override string ConvertFrom(string input)
		{
			if (input.Length < 21)
			{
				throw new ArgumentException($"The input '{input}' can not be validated using clé RIB.", nameof(input));
			}

			var sb = new StringBuilder();
			foreach (char c in input)
			{
				sb.Append(
					char.IsNumber(c)
						? c - CharCode0
						: ((char.ToUpperInvariant(c) - CharCodeA) % 9 + 1)
				);
			}

			return sb.ToString();
		}

		protected override int Calculate(string digits)
		{
			int b = int.Parse(digits.Substring(0, 5));
			int g = int.Parse(digits.Substring(5, 5));
			BigInteger c = BigInteger.Parse(digits.Substring(10), CultureInfo.InvariantCulture);

			BigInteger checkDigits = (97 - (89 * b + 15 * g + 3 * c) % 97);
			return (int)checkDigits;
		}
	}
}
