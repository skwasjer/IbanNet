using IbanNet.Registry;

namespace IbanNet.Validation.Rules
{
    /// <summary>
    /// The validation context for a validation rule.
    /// </summary>
    public class ValidationRuleContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRuleContext" /> class.
        /// </summary>
        /// <param name="value">The IBAN value to validate.</param>
        public ValidationRuleContext(string value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRuleContext" /> class.
        /// </summary>
        /// <param name="value">The IBAN value to validate.</param>
        /// <param name="country"></param>
        public ValidationRuleContext(string value, IbanCountry country)
            : this(value)
        {
            Country = country;
        }

        /// <summary>
        /// Gets the IBAN value to validate.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Gets the country specific format information that applies to the IBAN, if any.
        /// </summary>
        public IbanCountry? Country { get; internal set; }
    }
}
