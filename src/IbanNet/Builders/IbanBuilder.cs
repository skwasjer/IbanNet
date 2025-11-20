using System.Globalization;
using IbanNet.CheckDigits;
using IbanNet.Registry;

namespace IbanNet.Builders;

/// <summary>
/// A builder to build international bank account numbers (IBAN).
/// <para>
/// The builder does not ensure validity of the returned IBAN. The check digits are valid, but the characters used may be invalid. Use a validator on the built IBAN to ensure validity.
/// </para>
/// </summary>
public sealed class IbanBuilder : IBankAccountBuilder
{
    private const int InverseMod97PlusExpectedCheckDigit = 97 + 1;

    private readonly BbanBuilder _bbanBuilder;

    private IbanCountry _country = null!;

    /// <summary>
    /// Initializes a new instance of the <see cref="IbanBuilder" /> class.
    /// </summary>
    public IbanBuilder()
    {
        _bbanBuilder = new BbanBuilder();
    }

    /// <inheritdoc />
    public IBankAccountBuilder WithCountry(IbanCountry country)
    {
        _bbanBuilder.WithCountry(country);
        _country = country;
        return this;
    }

    /// <inheritdoc />
    public IBankAccountBuilder WithBankAccountNumber(string bankAccountNumber, bool enablePadding = true)
    {
        _bbanBuilder.WithBankAccountNumber(bankAccountNumber, enablePadding);
        return this;
    }

    /// <inheritdoc />
    public IBankAccountBuilder WithBankIdentifier(string bankIdentifier, bool enablePadding = true)
    {
        _bbanBuilder.WithBankIdentifier(bankIdentifier, enablePadding);
        return this;
    }

    /// <inheritdoc />
    public IBankAccountBuilder WithBranchIdentifier(string branchIdentifier, bool enablePadding = true)
    {
        _bbanBuilder.WithBranchIdentifier(branchIdentifier, enablePadding);
        return this;
    }

    /// <inheritdoc />
    public string Build()
    {
        string countryCode;
        string bban;
        char[] buffer;

        try
        {
            bban = _bbanBuilder.Build();
            if (!_country.Iban.Pattern.Tokens.Any())
            {
                throw new InvalidOperationException(string.Format(
                    CultureInfo.CurrentCulture,
                    Resources.Exception_The_country_0_does_not_define_a_IBAN_pattern,
                    _country.TwoLetterISORegionName));
            }

            countryCode = _country.TwoLetterISORegionName;
            buffer = new char[_country.Iban.Length];

            // Compute check digit.
            CopyToBuffer(buffer,
                countryCode,
                0,
                bban);
        }
        catch (InvalidOperationException ex)
        {
            Exception? innerEx = ex is BankAccountBuilderException bex ? bex.InnerException : ex;
            throw new BankAccountBuilderException(Resources.Exception_Builder_The_IBAN_cannot_be_built, innerEx);
        }

        // Return IBAN.
        int checkDigits = InverseMod97PlusExpectedCheckDigit - Mod9710.Compute(buffer);
        CopyToBuffer(buffer, countryCode, checkDigits, bban);

        return new string(buffer);
    }

    private static void CopyToBuffer
    (
        char[] buffer,
        string countryCode,
        int checkDigits,
        string bban,
        int countryCodePos = 0,
        int checkDigitPos = 2,
        int bbanPos = 4)
    {
        countryCode.CopyTo(0, buffer, countryCodePos, countryCode.Length);
        buffer[checkDigitPos] = (char)((checkDigits / 10) + '0');
        buffer[checkDigitPos + 1] = (char)((checkDigits % 10) + '0');
        bban.CopyTo(0, buffer, bbanPos, bban.Length);
    }
}
