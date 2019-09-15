using System.Linq;

namespace IbanNet.CheckDigits.Calculators
{
	/// <summary>
	/// Represents a base calculator which computes check digits for a given input string.
	/// </summary>
	public abstract class CheckDigitsCalculator
	{
		/// <summary>
		/// Gets the char code for 'A'.
		/// </summary>
		protected const int CharCodeA = 'A';

		/// <summary>
		/// Gets the char code for '0'.
		/// </summary>
		protected const int CharCode0 = '0';

		/// <summary>
		/// Returns the check digits for specified <paramref name="input"/>.
		/// </summary>
		/// <param name="input">The input string to compute check digits for.</param>
		/// <returns>The check digits.</returns>
		public int Compute(string input)
		{
			// Remove all whitespace.
			string normalizedInput = string.Join("", input.Where(c => !char.IsWhiteSpace(c)));

			string digits = ConvertFrom(normalizedInput);
			return Calculate(digits);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		protected abstract string ConvertFrom(string input);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="digits"></param>
		/// <returns></returns>
		protected abstract int Calculate(string digits);
	}
}