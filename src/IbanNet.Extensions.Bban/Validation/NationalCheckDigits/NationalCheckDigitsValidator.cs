using System;
using System.Collections.Generic;
using IbanNet.CheckDigits.Calculators;

namespace IbanNet.Validation.NationalCheckDigits
{
	internal abstract class NationalCheckDigitsValidator
	{
		private readonly CheckDigitsCalculator _checkDigitsCalculator;

		protected NationalCheckDigitsValidator(CheckDigitsCalculator checkDigitsCalculator, params string[] supportedCountryCodes)
		{
			_checkDigitsCalculator = checkDigitsCalculator ?? throw new ArgumentNullException(nameof(checkDigitsCalculator));
			SupportedCountryCodes = supportedCountryCodes ?? throw new ArgumentNullException(nameof(supportedCountryCodes));
		}

		/// <summary>
		/// Gets the country codes this national check digits validator applies to.
		/// </summary>
		public IEnumerable<string> SupportedCountryCodes { get; }

		/// <summary>
		/// Validates a BBAN for valid national check digits.
		/// </summary>
		public virtual bool Validate(string bban)
		{
			string checkString = GetCheckString(bban);
			int computedCheckDigits = _checkDigitsCalculator.Compute(checkString);

			return GetExpectedCheckDigits(bban) == computedCheckDigits;
		}

		/// <summary>
		/// Gets the portion of the BBAN for which to compute the check digits.
		/// </summary>
		protected abstract string GetCheckString(string bban);

		/// <summary>
		/// Gets the expected check digits value.
		/// </summary>
		protected abstract int GetExpectedCheckDigits(string bban);
	}
}
