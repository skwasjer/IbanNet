using IbanNet.Registry.Patterns;

namespace TestHelpers;

internal sealed class VerifyPatternTokenJsonConverter : WriteOnlyJsonConverter<PatternToken>
{
    public override void Write(VerifyJsonWriter writer, PatternToken value)
    {
        writer.WriteStartObject();
        writer.WriteMember(value, value.Category, nameof(value.Category));
        writer.WriteMember(value, value.Value, nameof(value.Value));
        writer.WriteMember(value, value.IsFixedLength, nameof(value.IsFixedLength));
        if (value.IsFixedLength)
        {
            writer.WriteMember(value, value.MaxLength, "Length");
        }
        else
        {
            writer.WriteMember(value, value.MinLength, nameof(value.MinLength));
            writer.WriteMember(value, value.MaxLength, nameof(value.MaxLength));
        }

        writer.WriteEndObject();
    }
}
