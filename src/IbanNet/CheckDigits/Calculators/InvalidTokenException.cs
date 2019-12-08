using System;

namespace IbanNet.CheckDigits.Calculators
{
	/// <summary>
	/// Exception that is thrown when an unexpected token/character is encountered while computing check digits.
	/// </summary>
	public class InvalidTokenException : InvalidOperationException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InvalidTokenException"/> using specified <paramref name="position"/> and the character that was not expected..
		/// </summary>
		/// <param name="position">The position in the string/char buffer where the unexpected character is located.</param>
		/// <param name="unexpectedChar">The character that was not expected.</param>
		public InvalidTokenException(int position, char unexpectedChar)
			: this(string.Format(Resources.InvalidTokenException_Expected_alphanumeric_character_at_position_0_but_found_1, position, unexpectedChar))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InvalidTokenException"/> using specified message.
		/// </summary>
		/// <param name="message">The error message.</param>
		public InvalidTokenException(string message)
			: base(message)
		{
		}
	}
}
