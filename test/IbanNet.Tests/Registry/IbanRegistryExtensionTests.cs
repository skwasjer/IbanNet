using IbanNet.Registry.Swift;

namespace IbanNet.Registry;

public static class IbanRegistryExtensionTests
{
    public sealed class ExcludingCountriesTests
    {
        private readonly IbanRegistry _sourceRegistry;

        public ExcludingCountriesTests()
        {
            _sourceRegistry = new IbanRegistry { Providers = { new SwiftRegistryProvider() } };
            _sourceRegistry.Providers.IsReadOnly.Should().BeFalse();
        }

        [Fact]
        public void Given_that_registry_is_null_when_excluding_countries_it_should_throw()
        {
            IIbanRegistry? registry = null;

            // Act
            Func<IIbanRegistry> act = () => registry!.ExcludingCountries("NL");

            // Assert
            act.Should()
                .Throw<ArgumentNullException>()
                .WithParameterName(nameof(registry));
        }

        [Fact]
        public void Given_that_country_code_is_excluded_when_accessing_the_registry_it_should_not_contain_those_countries()
        {
            string[] countryCodes = ["NL", "BE"];
            _sourceRegistry.Select(c => c.TwoLetterISORegionName).Should().Contain(countryCodes);

            // Act
            IIbanRegistry registry = _sourceRegistry.ExcludingCountries(countryCodes);

            // Assert
            registry.Should()
                .NotBeSameAs(_sourceRegistry, "it should only exclude specified countries")
                .And.HaveCount(_sourceRegistry.Count - countryCodes.Length)
                .And.Subject.Select(c => c.TwoLetterISORegionName)
                .Should()
                .NotContain(countryCodes);
        }

        [Fact]
        public void When_excluding_countries_the_resulting_registry_should_have_the_same_readonly_providers_as_the_source()
        {
            string[] countryCodes = ["NL", "BE"];
            _sourceRegistry.Providers.IsReadOnly.Should().BeFalse("we have not accessed the providers yet");

            // Act
            IIbanRegistry registry = _sourceRegistry.ExcludingCountries(countryCodes);

            // Assert
            registry.Should().NotBeSameAs(_sourceRegistry);
            registry.Providers.Should()
                .HaveCount(_sourceRegistry.Providers.Count)
                .And.AllSatisfy(p => registry.Providers.IndexOf(p).Should().Be(_sourceRegistry.Providers.IndexOf(p)), "the providers should be copied from the source registry and be in the same order");
            registry.Providers.IsReadOnly.Should().BeTrue("the providers are a direct reference to the source registry and should not be modifiable");
        }

        [Fact]
        public void Given_that_providers_from_the_source_registry_are_already_readonly_when_materializing_it_should_have_same_reference()
        {
            string[] countryCodes = ["NL", "BE"];
            _ = _sourceRegistry.Count; // Materialize the source registry.
            _sourceRegistry.Providers.IsReadOnly.Should().BeTrue();

            // Act
            IIbanRegistry registry = _sourceRegistry.ExcludingCountries(countryCodes);

            // Assert
            registry.Should().NotBeSameAs(_sourceRegistry);
            registry.Providers.Should().BeSameAs(_sourceRegistry.Providers);
        }

        [Fact]
        public void Given_that_list_is_null_when_excluding_countries_it_should_return_same_instance()
        {
            string[]? excludedCountryCodes = null;

            // Act
            Func<IIbanRegistry> act = () => _sourceRegistry.ExcludingCountries(excludedCountryCodes!);

            // Assert
            act.Should()
                .NotThrow()
                .Subject.Should()
                .BeSameAs(_sourceRegistry);
        }

        [Fact]
        public void Given_that_list_is_empty_when_excluding_countries_it_should_return_same_instance()
        {
            string[] excludedCountryCodes = [];

            // Act
            Func<IIbanRegistry> act = () => _sourceRegistry.ExcludingCountries(excludedCountryCodes);

            // Assert
            act.Should()
                .NotThrow()
                .Subject.Should()
                .BeSameAs(_sourceRegistry);
        }
    }

    public sealed class IncludingCountriesTests
    {
        private readonly IbanRegistry _sourceRegistry;

        public IncludingCountriesTests()
        {
            _sourceRegistry = new IbanRegistry { Providers = { new SwiftRegistryProvider() } };
            _sourceRegistry.Providers.IsReadOnly.Should().BeFalse();
        }

        [Fact]
        public void Given_that_registry_is_null_when_including_countries_it_should_throw()
        {
            IIbanRegistry? registry = null;

            // Act
            Func<IIbanRegistry> act = () => registry!.IncludingCountries("NL");

            // Assert
            act.Should()
                .Throw<ArgumentNullException>()
                .WithParameterName(nameof(registry));
        }

        [Fact]
        public void Given_that_country_code_is_included_when_accessing_the_registry_it_should_only_contain_those_countries()
        {
            string[] countryCodes = ["NL", "BE"];
            _sourceRegistry.Select(c => c.TwoLetterISORegionName)
                .Should()
                .Contain(countryCodes)
                .And.HaveCountGreaterThan(countryCodes.Length);

            // Act
            IIbanRegistry registry = _sourceRegistry.IncludingCountries(countryCodes);

            // Assert
            registry.Should()
                .NotBeSameAs(_sourceRegistry, "it should only include specified countries")
                .And.HaveCount(countryCodes.Length)
                .And.Subject.Select(c => c.TwoLetterISORegionName)
                .Should()
                .BeEquivalentTo(countryCodes);
        }

        [Fact]
        public void When_including_countries_the_resulting_registry_should_have_the_same_readonly_providers_as_the_source()
        {
            string[] countryCodes = ["NL", "BE"];
            _sourceRegistry.Providers.IsReadOnly.Should().BeFalse("we have not accessed the providers yet");

            // Act
            IIbanRegistry registry = _sourceRegistry.IncludingCountries(countryCodes);

            // Assert
            registry.Should().NotBeSameAs(_sourceRegistry);
            registry.Providers.Should()
                .HaveCount(_sourceRegistry.Providers.Count)
                .And.AllSatisfy(p => registry.Providers.IndexOf(p).Should().Be(_sourceRegistry.Providers.IndexOf(p)), "the providers should be copied from the source registry and be in the same order");
            registry.Providers.IsReadOnly.Should().BeTrue("the providers are a direct reference to the source registry and should not be modifiable");
        }

        [Fact]
        public void Given_that_providers_from_the_source_registry_are_already_readonly_when_materializing_it_should_have_same_reference()
        {
            string[] countryCodes = ["NL", "BE"];
            _ = _sourceRegistry.Count; // Materialize the source registry.
            _sourceRegistry.Providers.IsReadOnly.Should().BeTrue();

            // Act
            IIbanRegistry registry = _sourceRegistry.ExcludingCountries(countryCodes);

            // Assert
            registry.Should().NotBeSameAs(_sourceRegistry);
            registry.Providers.Should().BeSameAs(_sourceRegistry.Providers);
        }

        [Fact]
        public void Given_that_list_is_null_when_including_countries_it_should_return_same_instance()
        {
            string[]? includedCountryCodes = null;

            // Act
            Func<IIbanRegistry> act = () => _sourceRegistry.IncludingCountries(includedCountryCodes!);

            // Assert
            act.Should()
                .NotThrow()
                .Subject.Should()
                .BeSameAs(_sourceRegistry);
        }

        [Fact]
        public void Given_that_list_is_empty_when_including_countries_it_should_return_same_instance()
        {
            string[] includedCountryCodes = [];

            // Act
            Func<IIbanRegistry> act = () => _sourceRegistry.IncludingCountries(includedCountryCodes);

            // Assert
            act.Should()
                .NotThrow()
                .Subject.Should()
                .BeSameAs(_sourceRegistry);
        }
    }
}
