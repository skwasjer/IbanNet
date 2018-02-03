using System.Text.RegularExpressions;

namespace IbanNet.ValidationRules
{
	/// <summary>
	/// Asserts that the IBAN matches a specific regular expression.
	/// </summary>
	internal abstract class RegexRule : IIbanValidationRule
	{
		private RegexRule(Regex regex)
		{
			Regex = regex;
		}

		protected RegexRule(string pattern)
			: this(new Regex(pattern, RegexOptions.CultureInvariant | RegexOptions.Singleline))
		{
		}

		/// <summary>
		/// Gets the regex used to test the IBAN.
		/// </summary>
		// ReSharper disable once MemberCanBePrivate.Global
		protected Regex Regex { get; }

		/// <summary>
		/// Validates the IBAN against this rule.
		/// </summary>
		/// <param name="iban">The IBAN to validate.</param>
		/// <returns>true if the IBAN is valid, or false otherwise</returns>
		public virtual IbanValidationResult Validate(string iban)
		{
			return Regex.IsMatch(iban)
				? IbanValidationResult.Valid
				: IbanValidationResult.IllegalCharacters;
		}
	}
}