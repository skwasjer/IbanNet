using System;
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
		public IbanValidationResult Result { get; internal set; }

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

		/// <summary>
		/// Gets the exception that occurred during validation (if any).
		/// </summary>
		public Exception Exception { get; private set; }

		/// <summary>
		/// Gets the error message that occurred during validation (if any).
		/// </summary>
		public string ErrorMessage { get; private set; }

		/// <summary>
		/// Signals the rule failed with specified <paramref name="errorMessage"/>.
		/// </summary>
		/// <param name="errorMessage">The error message.</param>
		public void Fail(string errorMessage)
		{
			Result = IbanValidationResult.Custom;
			ErrorMessage = errorMessage;
		}

		/// <summary>
		/// Signals the rule failed due to specified <paramref name="exception"/>.
		/// </summary>
		/// <param name="exception">The exception that is the reason for the validation failure.</param>
		public void Fail(Exception exception)
		{
			Result = IbanValidationResult.Custom;
			Exception = exception;
		}
	}
}