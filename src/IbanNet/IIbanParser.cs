using System;
using System.Diagnostics.CodeAnalysis;

namespace IbanNet
{
    /// <summary>
    /// Provides parsing of international bank account numbers into an <see cref="Iban" />.
    /// </summary>
    public interface IIbanParser
    {
        /// <summary>
        /// Parses the specified <paramref name="value" /> into an <see cref="Iban" />.
        /// </summary>
        /// <param name="value">The IBAN value to parse.</param>
        /// <returns>an <see cref="Iban" /> if the <paramref name="value" /> is parsed successfully</returns>
        /// <exception cref="ArgumentNullException">Thrown when the specified <paramref name="value" /> is null.</exception>
        /// <exception cref="IbanFormatException">Thrown when the specified <paramref name="value" /> is not a valid IBAN.</exception>
        Iban Parse(string value);

        /// <summary>
        /// Attempts to parse the specified <paramref name="value" /> into an <see cref="Iban" />.
        /// </summary>
        /// <param name="value">The IBAN value to parse.</param>
        /// <param name="iban">The <see cref="Iban" /> if the <paramref name="value" /> is parsed successfully.</param>
        /// <returns><see langword="true" /> if the <paramref name="value" /> is parsed successfully, or <see langword="false" /> otherwise</returns>
        bool TryParse(string? value, [NotNullWhen(true)] out Iban? iban);
    }
}
