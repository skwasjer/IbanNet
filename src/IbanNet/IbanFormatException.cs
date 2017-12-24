using System;
using System.Runtime.Serialization;

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
		/// <param name="result">The validation result.</param>
		public IbanFormatException(string message, IbanValidationResult result) : base(message)
		{
			Result = result;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="IbanFormatException" /> class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		protected IbanFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>
		/// Gets the validation result.
		/// </summary>
		public IbanValidationResult Result { get; }
	}
}
