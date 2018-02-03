using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace IbanNet
{
	/// <summary>
	/// Represents an IBAN.
	/// </summary>
	[TypeConverter(typeof(IbanTypeConverter))]
	public sealed class Iban
	{
		/// <summary>
		/// The supported IBAN output formats.
		/// </summary>
		public static class Formats
		{
			/// <summary>
			/// Partitions an IBAN into 4 character segments separated with a space.
			/// </summary>
			public const string Partitioned = "S";

			/// <summary>
			/// An IBAN without whitespace.
			/// </summary>
			public const string Flat = "F";
		}

		private static readonly Regex NormalizeRegex = new Regex(@"\s+", RegexOptions.CultureInvariant);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		private readonly string _iban;

		/// <summary>
		/// Gets or sets the <see cref="IIbanValidator"/> used to validate an IBAN.
		/// </summary>
		// ReSharper disable once MemberCanBePrivate.Global
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
				case Formats.Partitioned:
					// Split into 4 char segments.
					var segments = _iban.Partition(4).Select(p => string.Join("", p));
					return string.Join(" ", segments);

				case null:
					throw new ArgumentNullException(nameof(format), $"The format is required. Supported formats are '{Formats.Flat}' (flat) and '{Formats.Partitioned}' (partitioned by space).");

				default:
					throw new ArgumentException($"The format '{format}' is invalid. Supported formats are '{Formats.Flat}' (flat) and '{Formats.Partitioned}' (partitioned by space).", nameof(format));
			}
		}

		/// <summary>Returns a string that represents the current <see cref="Iban"/>.</summary>
		/// <returns>A string that represents the current <see cref="Iban"/>.</returns>
		public override string ToString()
		{
			return ToString(Formats.Flat);
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

			if (TryParse(value, out var iban, out var validationResult))
			{
				return iban;
			}

			throw new IbanFormatException($"The value '{value}' is not a valid IBAN.", validationResult);
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

			return TryParse(value, out iban, out var _);
		}

		/// <summary>
		/// Attempts to parse the specified <paramref name="value"/> into an <see cref="Iban"/>.
		/// </summary>
		/// <param name="value">The IBAN value to parse.</param>
		/// <param name="iban">The <see cref="Iban"/> if the <paramref name="value"/> is parsed successfully.</param>
		/// <param name="validationResult">The validation result.</param>
		/// <returns>true if the <paramref name="value"/> is parsed successfully, or false otherwise</returns>
		private static bool TryParse(string value, out Iban iban, out IbanValidationResult validationResult)
		{
			iban = null;
			validationResult = IbanValidationResult.IllegalCharacters;
			if (value == null)
			{
				return false;
			}

			var normalizedValue = Normalize(value);
			if ((validationResult = Validator.Validate(normalizedValue)) == IbanValidationResult.Valid)
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
			return obj is Iban iban && Equals(iban);
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