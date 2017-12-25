using System;
using System.Runtime.Serialization;

namespace IbanNet
{
	/// <summary>
	/// The exception that is thrown when the format of an IBAN is invalid.
	/// </summary>
	[Serializable]
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
			Result = (IbanValidationResult) info.GetValue(nameof(Result), typeof(IbanValidationResult));
		}

		/// <summary>
		/// Gets the validation result.
		/// </summary>
		public IbanValidationResult Result { get; }

		/// <summary>When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the exception.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is a null reference (Nothing in Visual Basic). </exception>
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);

			info.AddValue(nameof(Result), Result);
		}
	}
}
