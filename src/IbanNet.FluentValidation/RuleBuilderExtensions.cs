using FluentValidation;

namespace IbanNet.FluentValidation
{
    /// <summary>
    /// FluentValidation rule builder extensions.
    /// </summary>
    public static class RuleBuilderExtensions
    {
        /// <summary>
        /// Defines an IBAN validator on the current rule builder, but only for string properties.
        /// Validation will fail if the value returned by the lambda is not a valid international bank account number.
        /// </summary>
        /// <typeparam name="T">Type of object being validated</typeparam>
        /// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
        /// <param name="ibanValidator">The <see cref="IIbanValidator" /> instance to use for validation.</param>
        /// <param name="strict">
        /// When true, the input must strictly match the IBAN format rules.
        /// When false, whitespace is ignored and strict character casing enforcement is disabled (meaning, the user can input in lower and uppercase). This mode is a bit more forgiving when dealing with user-input. However it does require after successful validation, that you parse the user input with <see cref="IIbanParser" /> to normalize/sanitize the input and to be able to format the IBAN in correct electronic format.
        ///
        /// <para>Default is <see langword="true" />. (this may change in future major release)</para>
        /// </param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> Iban<T>
        (
            this IRuleBuilder<T, string> ruleBuilder,
            IIbanValidator ibanValidator,
            bool strict = true
        )
        {
            if (ruleBuilder is null)
            {
                throw new ArgumentNullException(nameof(ruleBuilder));
            }

            return ruleBuilder.SetValidator(new FluentIbanValidator<T>(ibanValidator) { Strict = strict });
        }
    }
}
