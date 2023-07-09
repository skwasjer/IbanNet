using TestHelpers.Specs;

namespace IbanNet;

// This test class does some resource localization spot checks on a select few keys (per language and/or locale).
public sealed class LocalizationTests : LocalizationSpecBase
{
    protected override Type ResourceType => typeof(Resources);

    [Theory]
    [InlineData("ca-ES", nameof(Resources.InvalidLengthResult), "L'IBAN te una llargada incorrecta.")]
    [InlineData("en-US", nameof(Resources.InvalidLengthResult), "The IBAN has an incorrect length.")]
    [InlineData("nl-NL", nameof(Resources.InvalidLengthResult), "Dit IBAN heeft een ongeldige lengte.")]
    public override void Resource_should_be_localized(string cultureCode, string key, string expectedMessage)
    {
        base.Resource_should_be_localized(cultureCode, key, expectedMessage);
    }
}
