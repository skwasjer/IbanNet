using IbanNet.Registry;
using IbanNet.Validation.Results;

namespace IbanNet
{
	/// <summary>
	/// Represents the validator result.
	/// </summary>
	public class ValidationResult
	{
		/// <summary>
		/// <see cref="ValidationRuleResult.Success"/> if validation succeeded. Otherwise, indicates the reason of failure.
		/// </summary>
		public ValidationRuleResult Result { get; set; } = ValidationRuleResult.Success;

		/// <summary>
		/// Gets whether validation is successful.
		/// </summary>
		public bool IsValid => Equals(Result, ValidationRuleResult.Success);

		/// <summary>
		/// Gets the validated IBAN.
		/// </summary>
		public string? Value { get; set; }

		/// <summary>
		/// Gets the country info that matches the iban, if any.
		/// </summary>
		public CountryInfo? Country { get; set; }
	}
}
