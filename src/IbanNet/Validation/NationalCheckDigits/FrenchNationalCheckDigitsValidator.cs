using System.Collections;
using System.Globalization;
using System.Linq;
using System.Numerics;

namespace IbanNet.Validation.NationalCheckDigits
{
	internal class FrenchNationalCheckDigitsValidator
		: INationalCheckDigitsValidator
	{
		private static readonly int CharCodeA = 'A';

		public bool Validate(string iban)
		{
			string upperIban = iban.ToUpperInvariant();
			string bban = upperIban.Substring(4);

			string transformedBban = string.Join("",
				bban.Select(c => char.IsNumber(c)
					? c.ToString()
					: MapLetter(c).ToString()
				)
			);

			BigInteger largeInteger = BigInteger.Parse(transformedBban, CultureInfo.InvariantCulture);
			return largeInteger % 97 == 0;
		}

		private int MapLetter(char c)
		{
			if (c <= 'I')
			{
				return (c - 'A') + 1;
			}
            else if (c <= 'R')
			{
				return (c - 'J') + 1;
			}
			else
			{
				return (c - 'S') + 2;
			}
		}
	}
}