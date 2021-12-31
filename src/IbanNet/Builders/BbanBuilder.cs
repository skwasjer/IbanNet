using System.Globalization;
using IbanNet.Extensions;
using IbanNet.Registry;

namespace IbanNet.Builders
{
    /// <summary>
    /// A builder to build national bank account numbers (BBAN).
    /// <para>
    /// The builder does not ensure validity of the returned BBAN. Use a validator on the built BBAN to ensure validity.
    /// </para>
    /// </summary>
    public sealed class BbanBuilder : IBankAccountBuilder
    {
        private IbanCountry? _country;
        private char[]? _bankAccountNumber;
        private bool _bankAccountNumberPadding = true;
        private char[]? _bankIdentifier;
        private bool _bankIdentifierPadding = true;
        private char[]? _branchIdentifier;
        private bool _branchIdentifierPadding = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="BbanBuilder" /> class.
        /// </summary>
        // ReSharper disable once EmptyConstructor
        public BbanBuilder()
        {
        }

        /// <inheritdoc />
        public IBankAccountBuilder WithCountry(IbanCountry country)
        {
            _country = country ?? throw new ArgumentNullException(nameof(country));
            return this;
        }

        /// <inheritdoc />
        public IBankAccountBuilder WithBankAccountNumber(string bankAccountNumber, bool enablePadding = true)
        {
            // ReSharper disable once ConstantConditionalAccessQualifier
            _bankAccountNumber = bankAccountNumber?.ToCharArray() ?? throw new ArgumentNullException(nameof(bankAccountNumber));
            _bankAccountNumberPadding = enablePadding;
            return this;
        }

        /// <inheritdoc />
        public IBankAccountBuilder WithBankIdentifier(string bankIdentifier, bool enablePadding = true)
        {
            // ReSharper disable once ConstantConditionalAccessQualifier
            _bankIdentifier = bankIdentifier?.ToCharArray();
            _bankIdentifierPadding = enablePadding;
            return this;
        }

        /// <inheritdoc />
        public IBankAccountBuilder WithBranchIdentifier(string branchIdentifier, bool enablePadding = true)
        {
            // ReSharper disable once ConstantConditionalAccessQualifier
            _branchIdentifier = branchIdentifier?.ToCharArray();
            _branchIdentifierPadding = enablePadding;
            return this;
        }

        /// <inheritdoc />
        public string Build()
        {
            char[] buffer;
            try
            {
                if (_country is null)
                {
                    throw new InvalidOperationException(Resources.Exception_Builder_The_country_is_required);
                }

                if (!_country.Bban.Pattern.Tokens.Any())
                {
                    throw new InvalidOperationException(string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.Exception_The_country_0_does_not_define_a_BBAN_pattern,
                        _country.TwoLetterISORegionName));
                }

                buffer = new char[_country.Bban.Length].Fill('0');

                CopyToBuffer(_bankAccountNumber, buffer, _country.Bban, _bankAccountNumberPadding, _country.TwoLetterISORegionName, nameof(_country.Bban));
                CopyToBuffer(_bankIdentifier, buffer, _country.Bank, _bankIdentifierPadding, _country.TwoLetterISORegionName, nameof(_country.Bank));
                CopyToBuffer(_branchIdentifier, buffer, _country.Branch, _branchIdentifierPadding, _country.TwoLetterISORegionName, nameof(_country.Branch));
            }
            catch (InvalidOperationException ex)
            {
                throw new BankAccountBuilderException(Resources.Exception_Builder_The_BBAN_cannot_be_built, ex);
            }

            return new string(buffer);
        }

        private static void CopyToBuffer(char[]? source, char[] destination, StructureSection structure, bool padding, string countryCode, string name)
        {
            const int relativeToIbanPos = 4;

            if (structure.Length == 0 && source?.Length > 0)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_Builder_A_value_for_0_is_not_supported_for_country_code_1, name, countryCode));
            }

            CopyToBuffer(
                source,
                destination,
                structure.Position - relativeToIbanPos,
                structure.Length,
                padding);
        }

        private static void CopyToBuffer(char[]? source, char[] destination, int destinationPosition, int count, bool padding)
        {
            if (count == 0 || !(source?.Length > 0))
            {
                return;
            }

            int srcPos = count - source.Length;
            if (srcPos < 0 || (srcPos > 0 && !padding))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_Builder_The_value_0_does_not_have_the_correct_length_of_1, new string(source), count));
            }

            source.CopyTo(destination, destinationPosition + srcPos);
        }
    }
}
