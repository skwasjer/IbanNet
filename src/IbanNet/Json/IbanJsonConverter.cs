#if NET5_0_OR_GREATER
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using IbanNet.Registry;

namespace IbanNet.Json
{
    /// <summary>
    /// A JSON converter for the <see cref="Iban" /> type (and for the System.Text.Json namespace).
    /// </summary>
    public sealed class IbanJsonConverter : JsonConverter<Iban>
    {
        private readonly IIbanParser _ibanParser;

        /// <summary>
        /// Initializes a new instance of the <see cref="IbanJsonConverter" /> class using the <see cref="IbanRegistry.Default" />.
        /// </summary>
        public IbanJsonConverter()
            : this(new IbanParser(IbanRegistry.Default))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IbanJsonConverter" /> class using specified <paramref name="ibanParser" />.
        /// </summary>
        /// <param name="ibanParser"></param>
        // ReSharper disable once MemberCanBePrivate.Global
        public IbanJsonConverter(IIbanParser ibanParser)
        {
            _ibanParser = ibanParser ?? throw new ArgumentNullException(nameof(ibanParser));
        }

        /// <inheritdoc />
        public override Iban? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? strValue = reader.GetString();
            if (string.IsNullOrWhiteSpace(strValue))
            {
                return null;
            }

            if (!_ibanParser.TryParse(strValue, out Iban? iban))
            {
                throw new JsonException();
            }

            return iban;
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, Iban value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(IbanFormat.Electronic));
        }
    }
}
#endif
