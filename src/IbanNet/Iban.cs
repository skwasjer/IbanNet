using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading;
using IbanNet.Extensions;
using IbanNet.TypeConverters;

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
        [Obsolete("Use the 'IbanFormat' enumeration.")]
#pragma warning disable CA1034 // Nested types should not be visible - justification: nested 'enumeration' using constants.
        public static class Formats
#pragma warning restore CA1034 // Nested types should not be visible
        {
            /// <summary>
            /// Partitions an IBAN into 4 character segments separated with a space.
            /// </summary>
            [Obsolete("Use the 'IbanFormat.Print' enumeration.")]
            public const string Partitioned = "S";

            /// <summary>
            /// An IBAN without whitespace.
            /// </summary>
            [Obsolete("Use the 'IbanFormat.Electronic' enumeration.")]
            public const string Flat = "F";
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string _iban;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static Lazy<IIbanValidator> _validatorInstance = new Lazy<IIbanValidator>(
            () => new IbanValidator(),
            LazyThreadSafetyMode.ExecutionAndPublication
        );

        /// <summary>
        /// Gets or sets the <see cref="IIbanValidator" /> used to validate an IBAN.
        /// <para>
        /// Note: avoid using this member, it's only use case is allowing type conversion and may be obsolete in future.
        /// </para>
        /// </summary>
        // ReSharper disable once MemberCanBePrivate.Global
        public static IIbanValidator Validator
        {
            get => _validatorInstance.Value;
            set => _validatorInstance = new Lazy<IIbanValidator>(() => value, true);
        }

        internal Iban(string iban)
        {
            _iban = NormalizeOrNull(iban) ?? throw new ArgumentNullException(nameof(iban));
        }

        internal static string? NormalizeOrNull(string? iban)
        {
            return iban.StripWhitespaceOrNull()?.ToUpperInvariant();
        }

        /// <summary>Returns a string that represents the current <see cref="Iban" />.</summary>
        /// <example>
        /// F => NL91ABNA0417164300
        /// S => NL91 ABNA 0417 1643 00
        /// </example>
        /// <param name="format">The format to use. F = flat, S = partitioned by space.</param>
        /// <returns>A string that represents the current <see cref="Iban" />.</returns>
        [Obsolete("Use the overload accepting the 'IbanFormat' enumeration.")]
        public string ToString(string format)
        {
            return format switch
            {
                Formats.Flat => ToString(IbanFormat.Electronic),

                Formats.Partitioned => ToString(IbanFormat.Print),

                null => throw new ArgumentNullException(
                    nameof(format),
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.ArgumentException_The_format_is_required_with_supported_formats,
                        Formats.Flat,
                        Formats.Partitioned
                    )
                ),

                _ => throw new ArgumentException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.ArgumentException_The_format_0_is_invalid_with_supported_formats,
                        format,
                        Formats.Flat,
                        Formats.Partitioned
                    ),
                    nameof(format)
                )
            };
        }

        /// <summary>Returns a string that represents the current <see cref="Iban" />.</summary>
        /// <example>
        /// <see cref="IbanFormat.Print"/> => NL91 ABNA 0417 1643 00
        /// <see cref="IbanFormat.Electronic"/> => NL91ABNA0417164300
        /// <see cref="IbanFormat.Obfuscated"/> => XXXXXXXXXXXXXXXXXX4300
        /// </example>
        /// <param name="format">The format to use.</param>
        /// <returns>A string that represents the current <see cref="Iban" />.</returns>
        public string ToString(IbanFormat format)
        {
            switch (format)
            {
                case IbanFormat.Electronic:
                    return _iban;

                case IbanFormat.Obfuscated:
                    const int visibleChars = 4;
                    return new string('X', _iban.Length - visibleChars)
                      + _iban.Substring(_iban.Length - visibleChars, visibleChars);

                case IbanFormat.Print:
                    // Split into 4 char segments.
                    IEnumerable<string> segments = _iban.Partition(4).Select(p => new string(p.ToArray()));
                    return string.Join(" ", segments);

                default:
                    throw new ArgumentException(
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.ArgumentException_The_format_0_is_invalid,
                            format
                        ),
                        nameof(format)
                    );
            }
        }

        /// <summary>Returns a string that represents the current <see cref="Iban" />.</summary>
        /// <returns>A string that represents the current <see cref="Iban" />.</returns>
        public override string ToString()
        {
            return ToString(IbanFormat.Electronic);
        }

        /// <summary>
        /// Parses the specified <paramref name="value" /> into an <see cref="Iban" />.
        /// </summary>
        /// <param name="value">The IBAN value to parse.</param>
        /// <returns>an <see cref="Iban" /> if the <paramref name="value" /> is parsed successfully</returns>
        /// <exception cref="ArgumentNullException">Thrown when the specified <paramref name="value" /> is null.</exception>
        /// <exception cref="IbanFormatException">Thrown when the specified <paramref name="value" /> is not a valid IBAN.</exception>
        [Obsolete("Use the `IbanParser` class.")]
        public static Iban Parse(string? value)
        {
            return new IbanParser(Validator).Parse(value);
        }

        /// <summary>
        /// Attempts to parse the specified <paramref name="value" /> into an <see cref="Iban" />.
        /// </summary>
        /// <param name="value">The IBAN value to parse.</param>
        /// <param name="iban">The <see cref="Iban" /> if the <paramref name="value" /> is parsed successfully.</param>
        /// <returns>true if the <paramref name="value" /> is parsed successfully, or false otherwise</returns>
        [Obsolete("Use the `IbanParser` class.")]
        public static bool TryParse(string? value, [NotNullWhen(true)] out Iban? iban)
        {
            return new IbanParser(Validator).TryParse(value, out iban);
        }

        private bool Equals(Iban other)
        {
            return string.Equals(_iban, other._iban, StringComparison.Ordinal);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object. </param>
        /// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is Iban iban && Equals(iban);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
#if NETSTANDARD2_1
            return _iban.GetHashCode(StringComparison.Ordinal);
#else
			return _iban.GetHashCode();
#endif
        }

        /// <summary>
        /// Determines whether the <see cref="Iban" />s are equal to each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Iban left, Iban right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether the <see cref="Iban" />s are unequal to each other.
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
