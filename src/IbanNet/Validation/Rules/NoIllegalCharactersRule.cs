using IbanNet.Extensions;
using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules
{
	/// <summary>
	/// Asserts that the IBAN does not contain any illegal characters.
	/// </summary>
	internal class NoIllegalCharactersRule : IIbanValidationRule
	{
		/// <inheritdoc />
		public ValidationRuleResult Validate(ValidationRuleContext context)
		{
			string iban = context.Value;
			// ReSharper disable once LoopCanBeConvertedToQuery : justification -> faster
			// ReSharper disable once ForCanBeConvertedToForeach : justification -> faster
			for (int i = 0; i < iban.Length; i++)
			{
				char c = iban[i];
				// All chars must be 0-9, a-z or A-Z.
				if (!c.IsAlphaNumeric())
				{
					return new IllegalCharactersResult();
				}
			}

			return ValidationRuleResult.Success;
		}
	}
}
