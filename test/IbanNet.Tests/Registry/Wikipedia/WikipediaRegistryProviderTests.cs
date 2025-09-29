using TestHelpers;

namespace IbanNet.Registry.Wikipedia;

public class WikipediaRegistryProviderTests : BaseRegistryProviderSpec<WikipediaRegistryProvider>
{
    public WikipediaRegistryProviderTests() : base(110)
    {
    }

    protected override Task<WikipediaRegistryProvider> CreateSubjectAsync()
    {
        return Task.FromResult(new WikipediaRegistryProvider());
    }

    [Fact]
    [Trait("Category", "PublicApi")]
    public Task Provider_should_match_expected()
    {
        VerifySettings settings = VerifyHelpers.GetDefaultSettings(GetType());
        settings.UseDirectory("Snapshots");
        settings.UseFileName($"{nameof(WikipediaRegistryProvider)}");
        return Verify(Subject, settings);
    }
}
