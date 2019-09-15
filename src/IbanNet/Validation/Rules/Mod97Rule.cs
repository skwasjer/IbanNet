using IbanNet.CheckDigits.Calculators;

namespace IbanNet.Validation.Rules
{
	/// <summary>
	/// Asserts that the check digits are valid.
	/// </summary>
	internal class Mod97Rule : IIbanValidationRule
	{
		private const int ExpectedCheckDigit = 1;

		private readonly CheckDigitsCalculator _checkDigitsCalculator;

		public Mod97Rule()
		{
			_checkDigitsCalculator = new Mod97CheckDigitsCalculator();
		}

		/// <inheritdoc />
		public void Validate(ValidationRuleContext context)
		{
			string upperIban = context.Value.ToUpperInvariant();
			string shiftedIban = upperIban.Substring(4) + upperIban.Substring(0, 4);

			if (_checkDigitsCalculator.Compute(shiftedIban) != ExpectedCheckDigit)
			{
				context.Result = IbanValidationResult.InvalidCheckDigits;
			}
		}
	}
}
