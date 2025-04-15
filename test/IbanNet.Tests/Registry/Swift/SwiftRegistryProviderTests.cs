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
}
