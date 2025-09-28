using IbanNet.Registry.Patterns;

namespace TestHelpers;

internal sealed class VerifyPatternJsonConverter : WriteOnlyJsonConverter<Pattern>
{
    public override void Write(VerifyJsonWriter writer, Pattern value)
    {
        if (value.MaxLength == 0)
        {
            return;
        }

        writer.WriteStartObject();
        writer.WriteMember(value, value.ToString(), nameof(value.ToString) + "()");
        writer.WriteMember(value, value.IsFixedLength, nameof(value.IsFixedLength));
        writer.WriteMember(value, value.MaxLength, nameof(value.MaxLength));
        writer.WriteMember(value, value.Tokens, nameof(value.Tokens));
        writer.WriteEndObject();
    }
}
