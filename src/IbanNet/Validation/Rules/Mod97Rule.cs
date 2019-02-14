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

		/// <inheritdoc />
		public void Validate(ValidationContext context)
		{
			string upperIban = context.Value.ToUpperInvariant();
			string shiftedIban = upperIban.Substring(4) + upperIban.Substring(0, 4);

			string iso13616 = string.Join("", 
				shiftedIban.Select(c => char.IsNumber(c) 
					? c.ToString() 
					: (c - CharCodeA + 10).ToString()
				)
			);

			BigInteger largeInteger = BigInteger.Parse(iso13616, CultureInfo.InvariantCulture);
			if (largeInteger % 97 != 1)
			{
				context.Result = IbanValidationResult.InvalidCheckDigits;
			}
		}
	}
}
