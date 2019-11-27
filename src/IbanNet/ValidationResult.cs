using System;
using IbanNet.Registry;

namespace IbanNet
{
	/// <summary>
	/// Represents the validator result.
	/// </summary>
	public sealed class ValidationResult
	{
		/// <summary>
		/// <see cref="IbanValidationResult.Valid"/> if validation succeeded. Otherwise, indicates the reason of failure. 
		/// </summary>
		public IbanValidationResult Result { get; set; }

		/// <summary>
		/// Gets whether validation is successful.
		/// </summary>
		public bool IsValid => Result == IbanValidationResult.Valid;

		/// <summary>
		/// Gets the validated IBAN.
		/// </summary>
		public string? Value { get; set; }

		/// <summary>
		/// Gets the country info that matches the iban, if any.
		/// </summary>
		public CountryInfo? Country { get; set; }

		/// <summary>
		/// Gets the error message that occurred during validation (if any).
		/// </summary>
		public string? ErrorMessage { get; set; }

		/// <summary>
		/// Gets the type of the validation rule that did not pass (if any).
		/// </summary>
		public Type? ValidationRuleType { get; set; }
	}
}
