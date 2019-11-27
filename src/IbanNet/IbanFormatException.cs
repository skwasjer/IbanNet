using System;

namespace IbanNet
{
	/// <summary>
	/// The exception that is thrown when the format of an IBAN is invalid.
	/// </summary>
	public class IbanFormatException : FormatException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="IbanFormatException"/> class using specified message and validation result.
		/// </summary>
		/// <param name="message">The error message.</param>
		/// <param name="validationResult">The validation result.</param>
		public IbanFormatException(string message, ValidationResult validationResult)
			: this(message, validationResult, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="IbanFormatException"/> class using specified message and validation result.
		/// </summary>
		/// <param name="message">The error message.</param>
		/// <param name="validationResult">The validation result.</param>
		/// <param name="innerException">The inner exception.</param>
		public IbanFormatException(string message, ValidationResult? validationResult, Exception? innerException)
			: base(message, innerException)
		{
			Result = validationResult;
		}

		/// <summary>
		/// Gets the validation result.
		/// </summary>
		public ValidationResult? Result { get; }
	}
}
