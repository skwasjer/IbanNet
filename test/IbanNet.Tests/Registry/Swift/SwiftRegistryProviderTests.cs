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

    [Theory]
    [ClassData(typeof(ExpectedDefinitionsSubset))]
    public void When_definitions_are_loaded_should_contain(IbanCountry expectedIbanCountry)
    {
        IbanCountry? actual = Subject.Should()
            .Contain(c => c.TwoLetterISORegionName == expectedIbanCountry.TwoLetterISORegionName)
            .Which;
        actual.Should().BeEquivalentTo(expectedIbanCountry);
        actual.Iban.Pattern.ToString().Should().Be(expectedIbanCountry.Iban.Pattern.ToString());
        actual.Bban.Pattern.ToString().Should().Be(expectedIbanCountry.Bban.Pattern.ToString());
        actual.Bank.Pattern.ToString().Should().Be(expectedIbanCountry.Bank.Pattern.ToString());
        actual.Branch.Pattern.ToString().Should().Be(expectedIbanCountry.Branch.Pattern.ToString());
    }
}
