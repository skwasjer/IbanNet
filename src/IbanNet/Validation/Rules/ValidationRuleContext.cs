using System;
using IbanNet.Registry;

namespace IbanNet.Validation.Rules
{
	/// <summary>
	/// The validation context for a validation rule.
	/// </summary>
	public class ValidationRuleContext
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationRuleContext"/> class.
		/// </summary>
		/// <param name="value">The IBAN value to validate.</param>
		/// <param name="country">The country info (if any).</param>
		public ValidationRuleContext(string value, CountryInfo? country)
		{
			Value = value ?? throw new ArgumentNullException(nameof(value));
			Country = country;
		}

		/// <summary>
		/// Gets the IBAN value to validate.
		/// </summary>
		public string Value { get; }

		/// <summary>
		/// Gets or sets the country info that applies to the IBAN, if any.
		/// </summary>
		public CountryInfo? Country { get; }
	}
}
