using IbanNet.Validation.Rules;

namespace IbanNet.Extensions;

/// <summary>
/// Switzerland specific extensions for <see cref="Iban" />.
/// </summary>
public static class SwissIbanExtensions
{
    /// <summary>
    /// Gets whether the IBAN is a valid QR-IBAN from a Swiss or Liechtenstein account.
    /// <para>
    /// A valid QR-IBAN must have a valid QR-IID, i.e. the bank number must be within the [30000, 31999] range (both ends inclusive).
    /// </para>
    /// <para>
    /// The formal definition of IID, QR-IID and QR-IBAN can be found in the
    /// [Swiss Implementation Guidelines for the QR-bill](https://www.paymentstandards.ch/dam/downloads/ig-qr-bill-en.pdf).
    /// </para>
    /// <param name="iban">The iban.</param>
    /// <example>
    /// <list type="bullet">
    ///   <item>
    ///     <description>This property returns <see langword="true"/> for a QR-IBAN: <c>CH72 3000 0000 1234 5678 9</c> (IID = 30000)</description>
    ///   </item>
    ///   <item>
    ///     <description>This property returns <see langword="false"/> for a standard IBAN: <c>CH76 0900 0000 1234 5678 9</c> (IID = 9000)</description>
    ///   </item>
    /// </list>
    /// </example>
    /// </summary>
    public static bool IsQrIban(this Iban iban)
    {
        if (iban is null)
        {
            throw new ArgumentNullException(nameof(iban));
        }

        return QrIbanRule.IsValid(iban.Country, iban.BankIdentifier ?? "");
    }
}
