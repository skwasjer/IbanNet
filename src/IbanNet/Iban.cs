using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace IbanNet
{
	/// <summary>
	/// Represents an IBAN.
	/// </summary>
	public sealed class Iban
	{
		private static readonly Regex NormalizeRegex = new Regex(@"\s+", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		private readonly string _iban;

		/// <summary>
		/// Gets or sets the <see cref="IIbanValidator"/> used to validate an IBAN.
		/// </summary>
		public static IIbanValidator Validator { get; set; } = new IbanValidator();

		private Iban(string iban)
		{
			_iban = iban;
		}

		/// <summary>Returns a string that represents the current <see cref="Iban"/>.</summary>
		/// <example>
		/// F => NL91ABNA0417164300
		/// S => NL91 ABNA 0417 1643 00
		/// </example>
		/// <param name="format">The format to use. F = flat, S = partitioned by space.</param>
		/// <returns>A string that represents the current <see cref="Iban"/>.</returns>
		public string ToString(string format)
		{
			switch (format)
			{
				// Flat
				case "F":
					return _iban;

				// Partitioned by space
				case "S":
					// Split into 4 char segments.
					var segments = _iban.Partition(4).Select(p => string.Join("", p));
					return string.Join(" ", segments);

				case null:
					throw new ArgumentNullException(nameof(format), "The format is required. Supported formats are 'F' (flat) and 'S' (partitioned by space).");

				default:
					throw new ArgumentException($"The format '{format}' is invalid. Supported formats are 'F' (flat) and 'S' (partitioned by space).", nameof(format));
			}
		}

		/// <summary>Returns a string that represents the current <see cref="Iban"/>.</summary>
		/// <returns>A string that represents the current <see cref="Iban"/>.</returns>
		public override string ToString()
		{
			return ToString("S");
		}

		/// <summary>
		/// Parses the specified <paramref name="value"/> into an <see cref="Iban"/>.
		/// </summary>
		/// <param name="value">The IBAN value to parse.</param>
		/// <returns>an <see cref="Iban"/> if the <paramref name="value"/> is parsed successfully</returns>
		/// <exception cref="ArgumentNullException">Thrown when the specified <paramref name="value"/> is null.</exception>
		/// <exception cref="FormatException">Thrown when the specified <paramref name="value"/> is not a valid IBAN.</exception>
		public static Iban Parse(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			Iban iban;
			if (TryParse(value, out iban))
			{
				return iban;
			}

			throw new FormatException($"The value '{value}' is not a valid IBAN.");
		}

		/// <summary>
		/// Attempts to parse the specified <paramref name="value"/> into an <see cref="Iban"/>.
		/// </summary>
		/// <param name="value">The IBAN value to parse.</param>
		/// <param name="iban">The <see cref="Iban"/> if the <paramref name="value"/> is parsed successfully.</param>
		/// <returns>true if the <paramref name="value"/> is parsed successfully, or false otherwise</returns>
		public static bool TryParse(string value, out Iban iban)
		{
			iban = null;
			if (value == null)
			{
				return false;
			}

			var normalizedValue = Normalize(value);
			if (Validator.Validate(normalizedValue) == IbanValidationResult.Valid)
			{
				iban = new Iban(normalizedValue);
				return true;
			}

			return false;
		}

		private static string Normalize(string value)
		{
			return NormalizeRegex.Replace(value, "");
		}
	}
}