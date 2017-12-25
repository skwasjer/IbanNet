using System.Text.RegularExpressions;

namespace IbanNet.ValidationRules
{
	/// <summary>
	/// Asserts that the IBAN matches a specific regular expression.
	/// </summary>
	internal abstract class RegexRule : IIbanValidationRule
	{
		protected RegexRule(Regex regex)
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
		public Regex Regex { get; }

		/// <summary>
		/// The validation result to use when this rule is not valid.
		/// </summary>
		public abstract IbanValidationResult InvalidResult { get; }

		/// <summary>
		/// Validates the IBAN against this rule.
		/// </summary>
		/// <param name="iban">The IBAN to validate.</param>
		/// <returns>true if the IBAN is valid, or false otherwise</returns>
		public virtual bool Validate(string iban)
		{
			return Regex.IsMatch(iban);
		}
	}
}