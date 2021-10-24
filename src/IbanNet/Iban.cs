using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading;
using IbanNet.Extensions;
using IbanNet.Registry;
using IbanNet.TypeConverters;

namespace IbanNet
{
    /// <summary>
    /// Represents an IBAN.
    /// </summary>
    [TypeConverter(typeof(IbanTypeConverter))]
#if NET5_0_OR_GREATER
    [System.Text.Json.Serialization.JsonConverter(typeof(Json.IbanJsonConverter))]
#endif
    public sealed class Iban
        : IEquatable<Iban>,
          IFormattable
    {
        /// <summary>
        /// The maximum length of any IBAN, from any country.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal const int MaxLength = 34;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly Func<IIbanValidator> DefaultFactory = () => new IbanValidator();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string _iban;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static Lazy<IIbanValidator> _validatorInstance = new(
            DefaultFactory,
            LazyThreadSafetyMode.ExecutionAndPublication
        );

        /// <summary>
        /// Gets or sets the <see cref="IIbanValidator" /> used to validate an IBAN.
        /// <para>
        /// Note: avoid using this member, it's only use case is allowing type conversion and may be obsolete in future.
        /// </para>
        /// </summary>
        public static IIbanValidator Validator
        {
            get => _validatorInstance.Value;
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            set => _validatorInstance = value is null
                ? new Lazy<IIbanValidator>(DefaultFactory, true)
                : new Lazy<IIbanValidator>(() => value, true);
        }

        internal Iban(string iban, IbanCountry ibanCountry)
        {
            _iban = NormalizeOrNull(iban) ?? throw new ArgumentNullException(nameof(iban));
            Country = ibanCountry ?? throw new ArgumentNullException(nameof(ibanCountry));
        }

        /// <summary>
        /// Gets the country.
        /// </summary>
        public IbanCountry Country { get; }

        /// <summary>
        /// Gets the BBAN part of the IBAN.
        /// </summary>
        public string Bban => Extract(Country.Bban) ?? _iban.Substring(4);

        /// <summary>
        /// Gets the bank identifier, or <see langword="null" /> if bank identifier cannot be extracted.
        /// </summary>
        public string? BankIdentifier => Extract(Country.Bank);

        /// <summary>
        /// Gets the branch identifier, or <see langword="null" /> if branch identifier cannot be extracted.
        /// </summary>
        public string? BranchIdentifier => Extract(Country.Branch);

        /// <summary>Returns a string that represents the current <see cref="Iban" />.</summary>
        /// <example>
        /// <see cref="IbanFormat.Print" /> => NL91 ABNA 0417 1643 00
        /// <see cref="IbanFormat.Electronic" /> => NL91ABNA0417164300
        /// <see cref="IbanFormat.Obfuscated" /> => XXXXXXXXXXXXXXXXXX4300
        /// </example>
        /// <param name="format">The format to use.</param>
        /// <returns>A string that represents the current <see cref="Iban" />.</returns>
        public string ToString(IbanFormat format)
        {
            IFormattable fmt = this;
            return format switch
            {
                IbanFormat.Electronic => fmt.ToString("E", null),
                IbanFormat.Obfuscated => fmt.ToString("O", null),
                IbanFormat.Print => fmt.ToString("P", null),
                _ => throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.ArgumentException_The_format_0_is_invalid, format), nameof(format))
            };
        }

        /// <summary>Returns a string that represents the current <see cref="Iban" />.</summary>
        /// <returns>A string that represents the current <see cref="Iban" />.</returns>
        public override string ToString()
        {
            return ((IFormattable)this).ToString(null, null);
        }

        /// <summary>Formats the value of the current instance using the specified format.</summary>
        /// <example>
        /// P => NL91 ABNA 0417 1643 00
        /// E => NL91ABNA0417164300
        /// O => XXXXXXXXXXXXXXXXXX4300
        /// </example>
        /// <param name="format">The format to use.
        /// -or-
        /// A <see langword="null"/> reference to use the default format defined for the type of the <see cref="IFormattable" /> implementation.
        /// </param>
        /// <returns>The value of the current instance in the specified format.</returns>
        public string ToString(string? format)
        {
            return ((IFormattable)this).ToString(format, null);
        }

        /// <summary>Formats the value of the current instance using the specified format.</summary>
        /// <example>
        /// P => NL91 ABNA 0417 1643 00
        /// E => NL91ABNA0417164300
        /// O => XXXXXXXXXXXXXXXXXX4300
        /// </example>
        /// <param name="format">The format to use.
        /// -or-
        /// A <see langword="null"/> reference to use the default format defined for the type of the <see cref="IFormattable" /> implementation.
        /// </param>
        /// <param name="formatProvider">The provider to use to format the value.
        /// -or-
        /// A <see langword="null"/> reference to obtain the numeric format information from the current locale setting of the operating system.</param>
        /// <returns>The value of the current instance in the specified format.</returns>
        string IFormattable.ToString(string? format, IFormatProvider? formatProvider)
        {
            const int visibleChars = 4;
            const int segmentSize = 4;
            return (format ?? "E").ToUpper() switch
            {
                "E" => _iban,
                "O" => new string('X', _iban.Length - visibleChars)
                  + _iban.Substring(_iban.Length - visibleChars, visibleChars),
                "P" => string.Join(" ",
                    _iban
                        .Partition(segmentSize)
                        .Select(p => new string(p.ToArray()))
                ),
                _ => throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.ArgumentException_The_format_0_is_invalid, format), nameof(format))
            };
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns><see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
        public bool Equals(Iban? other)
        {
            return string.Equals(_iban, other?._iban, StringComparison.Ordinal);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.</returns>
        public override bool Equals(object? obj)
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
#if NETSTANDARD2_1 || NET5_0_OR_GREATER
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

        internal static string? NormalizeOrNull([NotNullIfNotNull("value")] string? value)
        {
            if (value is null)
            {
                return null;
            }

            int length = value.Length;
            char[] buffer = new char[length];
            int pos = 0;
            // ReSharper disable once ForCanBeConvertedToForeach - justification : performance
            for (int i = 0; i < length; i++)
            {
                char ch = value[i];
                if (ch.IsWhitespace())
                {
                    continue;
                }

                if (ch.IsAsciiLetter())
                {
                    // Inline upper case.
                    buffer[pos++] = (char)(ch & ~' ');
                }
                else
                {
                    buffer[pos++] = ch;
                }
            }

            return new string(buffer, 0, pos);
        }

        private string? Extract(StructureSection? structure)
        {
            if (structure?.Pattern is null or NullPattern)
            {
                return null;
            }

            if (structure.Position + structure.Length > _iban.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(structure));
            }

            return structure.Length == 0
                ? null
#if USE_SPANS
                : new string(_iban.AsSpan(structure.Position, structure.Length));
#else
                : _iban.Substring(structure.Position, structure.Length);
#endif
        }
    }
}
