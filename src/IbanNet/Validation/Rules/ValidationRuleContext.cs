using IbanNet.Registry;

namespace IbanNet.Validation.Rules
{
	/// <summary>
	/// Represents the validation context for a validation rule.
	/// </summary>
	public class ValidationRuleContext
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationRuleContext"/> class.
		/// </summary>
		/// <param name="value">The value (if any).</param>
		/// <param name="country">The country info (if any).</param>
		public ValidationRuleContext(string value, CountryInfo country)
		{
			Value = value;
			Country = country;
		}

		/// <summary>
		/// <see cref="IbanValidationResult.Valid"/> if validation succeeded. Otherwise, indicates the reason of failure. 
		/// </summary>
		public IbanValidationResult Result { get; set; }

		/// <summary>
		/// Gets whether validation is successful.
		/// </summary>
		public bool IsValid => Result == IbanValidationResult.Valid;

		/// <summary>
		/// Gets the validated iban value.
		/// </summary>
		public string Value { get; }

		/// <summary>
		/// Gets the country info that matches the iban, if any.
		/// </summary>
		public CountryInfo Country { get; }
	}
}