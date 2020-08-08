using System;
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
        /// Gets the IBAN value to validate.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Gets or sets the country info that applies to the IBAN, if any.
        /// </summary>
        public IbanCountry? Country { get; set; }
    }
}
