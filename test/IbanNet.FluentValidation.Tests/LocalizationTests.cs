using TestHelpers.Specs;

namespace IbanNet.FluentValidation;

// This test class does some resource localization spot checks on a select few keys (per language and/or locale).
public sealed class LocalizationTests : LocalizationSpecBase
{
    protected override Type ResourceType => typeof(Resources);

    [Theory]
    [InlineData("ca-ES", nameof(Resources.Not_a_valid_IBAN), "'{PropertyName}' no és un IBAN vàlid.")]
    [InlineData("de-DE", nameof(Resources.Not_a_valid_IBAN), "'{PropertyName}' ist keine gültige IBAN.")]
    [InlineData("en-US", nameof(Resources.Not_a_valid_IBAN), "'{PropertyName}' is not a valid IBAN.")]
    [InlineData("nl-NL", nameof(Resources.Not_a_valid_IBAN), "'{PropertyName}' is geen valide IBAN.")]
    public override void Resource_should_be_localized(string cultureCode, string key, string expectedMessage)
    {
        base.Resource_should_be_localized(cultureCode, key, expectedMessage);
    }
}
