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
		/// Gets whether validation is successful.
		/// </summary>
		public bool IsValid => Error is null;

		/// <summary>
		/// Gets the validated IBAN.
		/// </summary>
		public string? Value { get; set; }

		/// <summary>
		/// Gets the country info that matches the iban, if any.
		/// </summary>
		public CountryInfo? Country { get; set; }

		/// <summary>
		/// Gets the error that occurred, if any.
		/// </summary>
		public ErrorResult? Error { get; set; }
	}
}
