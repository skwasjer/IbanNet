using System.Text.RegularExpressions;

namespace IbanNet.Validation.Rules
{
	/// <summary>
	/// Asserts that the IBAN matches a specific regular expression.
	/// </summary>
	internal abstract class RegexRule : IIbanValidationRule
	{
		protected RegexRule(string pattern)
		{
			Regex = new Regex(pattern, RegexOptions.CultureInvariant | RegexOptions.Singleline);
		}

		/// <summary>
		/// Gets the regex used to test the IBAN.
		/// </summary>
		// ReSharper disable once MemberCanBePrivate.Global
		protected Regex Regex { get; }

		/// <inheritdoc />
		public virtual void Validate(ValidationContext context)
		{
			if (!Regex.IsMatch(context.Value))
			{
				context.Result = IbanValidationResult.IllegalCharacters;
			}
		}
	}
}