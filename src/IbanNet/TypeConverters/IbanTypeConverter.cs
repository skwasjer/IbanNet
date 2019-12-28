using System;
using System.ComponentModel;
using System.Globalization;

namespace IbanNet.TypeConverters
{
	/// <summary>
	/// Provides a way of converting an <see cref="Iban"/> from and to other types.
	/// </summary>
	public class IbanTypeConverter : TypeConverter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="IbanTypeConverter"/> class.
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
					// Request validator from service provider if available.
					// We don't use it atm., but if DI extensions are used, it triggers setting static validator.
					// TODO: refactor so that TryParse can use resolved instance instead of relying on static validator member.
					if (context is IServiceProvider services)
					{
						services.GetService(typeof(IIbanValidator));
					}

					if (Iban.TryParse(strValue, out Iban? iban))
					{
						return iban;
					}

					break;
			}

			return base.ConvertFrom(context, culture, value);
		}
	}
}
