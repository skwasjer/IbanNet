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
		private static class Formats
		{
			public const string Partioned = "S";
			public const string Flat = "F";
		}

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
				case Formats.Flat:
					return _iban;

				// Partitioned by space
				case Formats.Partioned:
					// Split into 4 char segments.
					var segments = _iban.Partition(4).Select(p => string.Join("", p));
					return string.Join(" ", segments);

				case null:
					throw new ArgumentNullException(nameof(format), $"The format is required. Supported formats are '{Formats.Flat}' (flat) and '{Formats.Partioned}' (partitioned by space).");

				default:
					throw new ArgumentException($"The format '{format}' is invalid. Supported formats are '{Formats.Flat}' (flat) and '{Formats.Partioned}' (partitioned by space).", nameof(format));
			}
		}

		/// <summary>Returns a string that represents the current <see cref="Iban"/>.</summary>
		/// <returns>A string that represents the current <see cref="Iban"/>.</returns>
		public override string ToString()
		{
			return ToString(Formats.Partioned);
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
				iban = new Iban(normalizedValue.ToUpperInvariant());
				return true;
			}

			return false;
		}

		/// <summary>
		/// Normalizes an IBAN value by removing all whitespace (but does not change character casing).
		/// </summary>
		/// <param name="iban">The IBAN value.</param>
		/// <returns>a normalized IBAN value</returns>
		internal static string Normalize(string iban)
		{
			if (iban == null)
			{
				return null;
			}
			return NormalizeRegex.Replace(iban, "");
		}

		private bool Equals(Iban other)
		{
			return string.Equals(_iban, other._iban);
		}

		/// <summary>
		/// Determines whether the specified object is equal to the current object.
		/// </summary>
		/// <param name="obj">The object to compare with the current object. </param>
		/// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj is Iban && Equals((Iban)obj);
		}

		/// <summary>
		/// Serves as the default hash function.
		/// </summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			return (_iban != null ? _iban.GetHashCode() : 0);
		}

		/// <summary>
		/// Determines whether the <see cref="Iban"/>s are equal to eachother.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator ==(Iban left, Iban right)
		{
			return Equals(left, right);
		}

		/// <summary>
		/// Determines whether the <see cref="Iban"/>s are unequal to eachother.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator !=(Iban left, Iban right)
		{
			return !Equals(left, right);
		}
	}
}