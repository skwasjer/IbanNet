using TestHelpers.Specs;

namespace IbanNet.DataAnnotations;

// This test class does some resource localization spot checks on a select few keys (per language and/or locale).
public sealed class LocalizationTests : LocalizationSpecBase
{
    protected override Type ResourceType => typeof(Resources);

    [Theory]
    [InlineData("ca-ES", nameof(Resources.IbanAttribute_Invalid), "El camp '{0}' No és un IBAN vàlid.")]
    [InlineData("de-DE", nameof(Resources.IbanAttribute_Invalid), "Das Feld '{0}' ist keine gültige IBAN.")]
    [InlineData("en-US", nameof(Resources.IbanAttribute_Invalid), "The field '{0}' is not a valid IBAN.")]
    [InlineData("nl-NL", nameof(Resources.IbanAttribute_Invalid), "Het veld '{0}' is geen valide IBAN.")]
    public override void Resource_should_be_localized(string cultureCode, string key, string expectedMessage)
    {
        base.Resource_should_be_localized(cultureCode, key, expectedMessage);
    }
}
