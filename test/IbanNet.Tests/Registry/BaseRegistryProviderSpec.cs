using TestHelpers;
using TestHelpers.Specs;

namespace IbanNet.Registry;

public abstract class BaseRegistryProviderSpec<T> : AsyncSpec<T>
    where T : IIbanRegistryProvider
{
    private readonly int _expectedCount;

    protected BaseRegistryProviderSpec(int expectedCount)
    {
        _expectedCount = expectedCount;
    }

    protected override Task GivenAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public void When_creating_registry_provider_it_should_have_n_items()
    {
        Subject.Should().NotBeEmpty();
        Subject.Count.Should().Be(_expectedCount);
    }

    [Fact]
    public void Max_length_should_be_less_or_equal_for_all_countries()
    {
        int maxLengthOfAllCountries = Subject.Max(c => c.Iban.Length);

        // Assert
        Iban.MaxLength.Should().BeGreaterOrEqualTo(maxLengthOfAllCountries);
    }

    [Fact]
    [Trait("Category", "PublicApi")]
    public Task Provider_should_match_expected()
    {
        string[] ns = typeof(T).Namespace!.Split('.');
        VerifySettings settings = VerifyHelpers.GetDefaultSettings(GetType());
        settings.UseDirectory(Path.Combine(ns[ns.Length - 1], "Snapshots"));
        settings.UseFileName(typeof(T).Name);
        return Verify(Subject, settings);
    }
}
