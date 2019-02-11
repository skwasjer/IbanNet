using System.Globalization;
using System.Linq;
using System.Numerics;

namespace IbanNet.Validation.Rules
{
	/// <summary>
	/// Asserts that the check digits are valid.
	/// </summary>
	internal class Mod97Rule : IIbanValidationRule
	{
		private static readonly int CharCodeA = 'A';

		/// <summary>
		/// Validates the IBAN against this rule.
		/// </summary>
		/// <param name="iban">The IBAN to validate.</param>
		/// <returns>true if the IBAN is valid, or false otherwise</returns>
		public IbanValidationResult Validate(string iban)
		{
			var upperIban = iban.ToUpperInvariant();
			var shiftedIban = upperIban.Substring(4) + upperIban.Substring(0, 4);

			var iso13616 = string.Join("", 
				shiftedIban.Select(c => char.IsNumber(c) 
					? c.ToString() 
					: (c - CharCodeA + 10).ToString()
				)
			);

			var largeInteger = BigInteger.Parse(iso13616, CultureInfo.InvariantCulture);
			return largeInteger % 97 == 1
				? IbanValidationResult.Valid
				: IbanValidationResult.InvalidCheckDigits;
		}
	}
}
