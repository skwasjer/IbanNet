using System;
using System.Collections.Generic;
using IbanNet.Registry;
using IbanNet.Validation.Rules;

namespace IbanNet
{
    /// <summary>
    /// Options for <see cref="IbanValidator" />.
    /// </summary>
    public class IbanValidatorOptions
    {
        private ValidationMethod _method = ValidationMethod.Strict;

        /// <summary>
        /// Gets or sets the IBAN country registry factory. Defaults to <see cref="IbanRegistry.Default" />.
        /// </summary>
        public IIbanRegistry Registry { get; set; } = IbanRegistry.Default;

        /// <summary>
        /// Gets or sets the validation method. Defaults to <see cref="IbanNet.ValidationMethod.Strict" />.
        /// </summary>
        [Obsolete("From v5.0 onwards, IbanNet will always validate in strict mode.")]
        public ValidationMethod Method
        {
            get => _method;
            set
            {
                if (!Enum.IsDefined(typeof(ValidationMethod), value))
                {
                    throw new ArgumentException(Resources.ArgumentException_ValidationMethod_is_invalid, nameof(value));
                }

                _method = value;
            }
        }

        /// <summary>
        /// Gets or sets custom rules to apply after built-in IBAN validation has taken place.
        /// </summary>
#pragma warning disable CA2227 // Collection properties should be read only - Fix in next major version
        public ICollection<IIbanValidationRule> Rules { get; set; } = new List<IIbanValidationRule>();
#pragma warning restore CA2227 // Collection properties should be read only
    }
}
