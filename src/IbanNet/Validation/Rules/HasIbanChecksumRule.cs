using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules
{
    /// <summary>
    /// Asserts that the IBAN checksum digits are not 00, 01 or 99.
    /// </summary>
    internal sealed class HasIbanChecksumRule : IIbanValidationRule
    {
        /// <inheritdoc />
        public ValidationRuleResult Validate(ValidationRuleContext context)
        {
            string iban = context.Value;
            if (iban.Length < 4
                // 00 and 01 are invalid.
             || iban[2] == '0' && (iban[3] == '0' || iban[3] == '1')
                // 99 is invalid.
             || iban[2] == '9' && iban[3] == '9')
            {
                return new IllegalCharactersResult(3);
            }

            return ValidationRuleResult.Success;
        }
    }
}
