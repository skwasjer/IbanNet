using System.Collections;
using IbanNet.Registry.Patterns;

namespace IbanNet.Registry;

public sealed class PatternIntegrationTests
{
    [Theory]
    [ClassData(typeof(AllIbanPatterns))]
    public void Iban_patterns_should_be_valid(Type providerType, string countryCode, Pattern bban)
    {
        var expectedCountryToken = new PatternToken(countryCode);
        expectedCountryToken.Value.Should().Be(countryCode);
        expectedCountryToken.MinLength.Should().Be(2);
        expectedCountryToken.MaxLength.Should().Be(2);

        // Assert
        bban.Tokens[0].Should().BeEquivalentTo(expectedCountryToken);
        bban.Tokens.Skip(1).Should().NotContain(t => t.Category == AsciiCategory.None);
    }

    [Theory]
    [ClassData(typeof(AllBbanPatterns))]
    public void Bban_patterns_should_be_valid(Type providerType, string countryCode, Pattern bban)
    {
        // Assert
        bban.Tokens.Should().NotContain(t => t.Category == AsciiCategory.None);
    }

    [Theory]
    [ClassData(typeof(AllBankPatterns))]
    public void Bank_patterns_should_be_valid(Type providerType, string countryCode, Pattern bban)
    {
        // Assert
        bban.Tokens.Should().NotContain(t => t.Category == AsciiCategory.None);
    }

    [Theory]
    [ClassData(typeof(AllBranchPatterns))]
    public void Branch_patterns_should_be_valid(Type providerType, string countryCode, Pattern bban)
    {
        // Assert
        bban.Tokens.Should().NotContain(t => t.Category == AsciiCategory.None);
    }

    public sealed class AllIbanPatterns : AllPatternsBase
    {
        protected override Pattern GetPattern(IbanCountry country)
        {
            return country.Iban.Pattern;
        }
    }

    public sealed class AllBbanPatterns : AllPatternsBase
    {
        protected override Pattern GetPattern(IbanCountry country)
        {
            return country.Bban.Pattern;
        }
    }

    public sealed class AllBankPatterns : AllPatternsBase
    {
        protected override Pattern GetPattern(IbanCountry country)
        {
            return country.Bank.Pattern;
        }
    }

    public sealed class AllBranchPatterns : AllPatternsBase
    {
        protected override Pattern GetPattern(IbanCountry country)
        {
            return country.Branch.Pattern;
        }
    }

    public abstract class AllPatternsBase : IEnumerable<object[]>
    {
        protected abstract Pattern GetPattern(IbanCountry country);

        public IEnumerator<object[]> GetEnumerator()
        {
            foreach (IIbanRegistryProvider provider in typeof(IIbanRegistry).Assembly
                         .GetExportedTypes()
                         .Where(t => t is { IsClass: true, IsPublic: true, IsAbstract: false }
                          && typeof(IIbanRegistryProvider).IsAssignableFrom(t))
                         .Select(Activator.CreateInstance)
                         .OfType<IIbanRegistryProvider>())
            {
                Type providerType = provider.GetType();
                foreach (IbanCountry c in provider)
                {
                    yield return [providerType, c.TwoLetterISORegionName, GetPattern(c)];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
