using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;

namespace IbanNet.CheckDigits.Calculators
{
	/// <summary>
	/// Computes check digits using mod 97 algorithm.
	/// </summary>
	public class Mod97CheckDigitsCalculator : CheckDigitsCalculator
	{
		/// <inheritdoc />
		protected override string ConvertFrom(string input)
		{
			var sb = new StringBuilder();
			foreach (char c in input)
			{
				sb.Append(
					char.IsNumber(c)
						? c - CharCode0
						: char.ToUpperInvariant(c) - CharCodeA + 10
				);
			}

			return sb.ToString();
		}

		/// <inheritdoc />
		protected override int Calculate(string digits)
		{
			BigInteger largeInteger = BigInteger.Parse(digits, CultureInfo.InvariantCulture);
			return (int)(largeInteger % 97);
		}
	}
}