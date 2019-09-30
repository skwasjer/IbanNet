using System;

namespace IbanNet.Validation.NationalCheckDigits
{
	internal class ItalianNationalCheckDigitsValidator
		: INationalCheckDigitsValidator
	{
		public bool Validate(string iban)
		{
			string upperIban = iban.ToUpperInvariant();
			string bban = upperIban.Substring(5);
			
			int sum = 0;
			for (int i = 0; i < bban.Length; i++)
			{
				sum += MapCharacter(bban[i], i % 2 == 1);
			}

			char checkDigit = (char)('A' + (sum % 26));
			return checkDigit == upperIban[4];
		}

		private int MapCharacter(char c, bool isEven)
		{
			switch (c)
			{
				case '0': return isEven ? 0 : 1;
				case '1': return isEven ? 1 : 0;
				case '2': return isEven ? 2 : 5;
				case '3': return isEven ? 3 : 7;
				case '4': return isEven ? 4 : 9;
				case '5': return isEven ? 5 : 13;
				case '6': return isEven ? 6 : 15;
				case '7': return isEven ? 7 : 17;
				case '8': return isEven ? 8 : 19;
				case '9': return isEven ? 9 : 21;
				case 'A': return isEven ? 0 : 1;
				case 'B': return isEven ? 1 : 0;
				case 'C': return isEven ? 2 : 5;
				case 'D': return isEven ? 3 : 7;
				case 'E': return isEven ? 4 : 9;
				case 'F': return isEven ? 5 : 13;
				case 'G': return isEven ? 6 : 15;
				case 'H': return isEven ? 7 : 17;
				case 'I': return isEven ? 8 : 19;
				case 'J': return isEven ? 9 : 21;
				case 'K': return isEven ? 10 : 2;
				case 'L': return isEven ? 11 : 4;
				case 'M': return isEven ? 12 : 18;
				case 'N': return isEven ? 13 : 20;
				case 'O': return isEven ? 14 : 11;
				case 'P': return isEven ? 15 : 3;
				case 'Q': return isEven ? 16 : 6;
				case 'R': return isEven ? 17 : 8;
				case 'S': return isEven ? 18 : 12;
				case 'T': return isEven ? 19 : 14;
				case 'U': return isEven ? 20 : 16;
				case 'V': return isEven ? 21 : 10;
				case 'W': return isEven ? 22 : 22;
				case 'X': return isEven ? 23 : 25;
				case 'Y': return isEven ? 24 : 24;
				case 'Z': return isEven ? 25 : 23;
				default: throw new NotSupportedException();
			}
		}
	}
}