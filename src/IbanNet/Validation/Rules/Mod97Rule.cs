using System.Numerics;
using IbanNet.Extensions;
using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules
{
	/// <summary>
	/// Asserts that the check digits are valid.
	/// </summary>
	internal class Mod97Rule : IIbanValidationRule
	{
		/// <inheritdoc />
		public ValidationRuleResult Validate(ValidationRuleContext context, string iban)
		{
			BigInteger largeInteger;
			largeInteger = BuildLargeInteger(iban, 4, iban.Length, new BigInteger());
			largeInteger = BuildLargeInteger(iban, 0, 4, largeInteger);

			return largeInteger % 97 == 1
				? ValidationRuleResult.Success
				: new BuiltInErrorResult(IbanValidationResult.InvalidCheckDigits);
		}

		private static BigInteger BuildLargeInteger(string value, int startPos, int endPos, BigInteger current)
		{
			for (int i = startPos; i < endPos; i++)
			{
				char c = value[i];
				if (c.IsAsciiDigit())
				{
					// Append digit.
					current = current * 10 + c - '0';
				}
				else
				{
					// Append char value:
					// - Use bitwise OR to convert char to lowercase.
					// - a = 10, b = 11, c = 12, etc.
					current = current * 100 + (c | ' ') - 'a' + 10;
				}
			}

			return current;
		}
	}
}
