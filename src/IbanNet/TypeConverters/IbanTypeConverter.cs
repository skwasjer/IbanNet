using System;
using System.ComponentModel;
using System.Globalization;

namespace IbanNet.TypeConverters
{
    /// <summary>
    /// Provides a way of converting an <see cref="Iban" /> from and to other types.
    /// </summary>
    public sealed class IbanTypeConverter : TypeConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IbanTypeConverter" /> class.
        /// </summary>
        // ReSharper disable once EmptyConstructor
        public IbanTypeConverter()
        {
        }

        /// <inheritdoc />
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        /// <inheritdoc />
        public override object? ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            switch (value)
            {
                case null:
                    return null;

                case string strValue:
                    IbanParser parser = GetParser(context);
                    if (parser.TryParse(strValue, out Iban? iban))
                    {
                        return iban;
                    }

                    break;
            }

            return base.ConvertFrom(context, culture, value);
        }

        private static IbanParser GetParser(IServiceProvider? services)
        {
            // Request validator from service provider if available.
            IIbanValidator validator = (IIbanValidator?)services?.GetService(typeof(IIbanValidator)) ?? Iban.Validator;
            return new IbanParser(validator);
        }
    }
}
