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
			: base(message)
		{
			Result = validationResult ?? throw new ArgumentNullException(nameof(validationResult));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="IbanFormatException"/> class using specified message and validation result.
		/// </summary>
		/// <param name="message">The error message.</param>
		/// <param name="innerException">The inner exception.</param>
		public IbanFormatException(string message, Exception? innerException)
			: base(message, innerException)
		{
		}

		/// <summary>
		/// Gets the validation result.
		/// </summary>
		public ValidationResult? Result { get; }
	}
}
