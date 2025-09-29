using TestHelpers;

namespace IbanNet.Registry.Swift;

public class SwiftRegistryProviderTests : BaseRegistryProviderSpec<SwiftRegistryProvider>
{
    public SwiftRegistryProviderTests() : base(88)
    {
    }

    protected override Task<SwiftRegistryProvider> CreateSubjectAsync()
    {
        return Task.FromResult(new SwiftRegistryProvider());
    }

    [Fact]
    [Trait("Category", "PublicApi")]
    public Task Provider_should_match_expected()
    {
        VerifySettings settings = VerifyHelpers.GetDefaultSettings(GetType());
        settings.UseDirectory("Snapshots");
        settings.UseFileName($"{nameof(SwiftRegistryProvider)}");
        return Verify(Subject, settings);
    }
}
