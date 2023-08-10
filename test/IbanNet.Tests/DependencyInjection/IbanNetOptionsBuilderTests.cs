using IbanNet.DependencyInjection.FluentAssertions;
using IbanNet.FakeRules;
using IbanNet.Registry;
using IbanNet.Registry.Swift;
using TestHelpers;

namespace IbanNet.DependencyInjection;

public class IbanNetOptionsBuilderTests
{
    private readonly IIbanNetOptionsBuilder _builderStub;

    protected IbanNetOptionsBuilderTests()
    {
        _builderStub = GetBuilderStub();
    }

    private static IIbanNetOptionsBuilder GetBuilderStub()
    {
        IIbanNetOptionsBuilder? builderMock = Substitute.For<IIbanNetOptionsBuilder>();
        builderMock
            .Configure(Arg.Any<Action<DependencyResolverAdapter, IbanValidatorOptions>>())
            .Returns(builderMock);

        return builderMock;
    }

    public class ExtensionTests : IbanNetOptionsBuilderTests
    {
        [Fact]
        public void Given_registry_is_configured_it_should_set_registry()
        {
            IEnumerable<IbanCountry> limitedCountries = IbanRegistry.Default
                .Where((_, i) => i % 2 == 0)
                .ToList();

            // Act
            IIbanNetOptionsBuilder returnedBuilder = _builderStub.UseRegistry(new IbanRegistry
            {
                Providers =
                {
                    new IbanRegistryListProvider(limitedCountries)
                }
            });

            // Assert
            _builderStub.Should().HaveConfiguredRegistry(limitedCountries);
            returnedBuilder.Should().BeSameAs(_builderStub);
        }

        [Fact]
        public void Given_registry_without_providers_is_configured_it_should_throw()
        {
            var registry = new IbanRegistry();

            // Act
            Action act = () => _builderStub.UseRegistry(registry);

            // Assert
            act.Should()
                .Throw<ArgumentException>()
                .WithMessage(Resources.The_registry_has_no_providers + "*")
                .WithParameterName(nameof(registry));
        }

        [Fact]
        public void Given_registry_provider_is_configured_it_should_use_provider()
        {
            var customProvider = new IbanRegistryListProvider(new[] { new IbanCountry("XX") });

            // Act
            IIbanNetOptionsBuilder returnedBuilder = _builderStub.UseRegistryProvider(customProvider);

            // Assert
            _builderStub.Should().HaveConfiguredRegistry(customProvider);
            returnedBuilder.Should().BeSameAs(_builderStub);
        }

        [Fact]
        public void Given_multiple_registry_providers_are_configured_it_should_use_providers()
        {
            var customProvider = new IbanRegistryListProvider(new[] { new IbanCountry("XX") });

            // Act
            IIbanNetOptionsBuilder returnedBuilder = _builderStub.UseRegistryProvider(new SwiftRegistryProvider(), customProvider);

            // Assert
            _builderStub.Should().HaveConfiguredRegistry(new SwiftRegistryProvider().Concat(customProvider));
            returnedBuilder.Should().BeSameAs(_builderStub);
        }

        [Fact]
        public void Given_rule_is_configured_via_factory_it_should_add_instance_to_rule_collection()
        {
            var configuredRule = new TestValidationRule();

            // Act
            IIbanNetOptionsBuilder returnedBuilder = _builderStub.WithRule(() => configuredRule);

            // Assert
            _builderStub.Should()
                .HaveConfiguredRule<TestValidationRule>()
                .And.Contain(configuredRule);
            returnedBuilder.Should().BeSameAs(_builderStub);
        }
    }

    public class NullArgumentTests : IbanNetOptionsBuilderTests
    {
        [Theory]
        [MemberData(nameof(NullArgumentTestCases))]
        public void Given_null_instance_when_calling_method_it_should_throw(params object[] args)
        {
            NullArgumentTest.Execute(args);
        }

        public static IEnumerable<object?[]> NullArgumentTestCases()
        {
            IIbanNetOptionsBuilder instance = GetBuilderStub();

            return new NullArgumentTestCases
            {
                DelegateTestCase.Create<IIbanNetOptionsBuilder, Action<IbanValidatorOptions>, IIbanNetOptionsBuilder>(
                    IbanNetOptionsBuilderExtensions.Configure,
                    instance,
                    _ => { }),
                DelegateTestCase.Create(
                    IbanNetOptionsBuilderExtensions.WithRule,
                    instance,
                    typeof(TestValidationRule)),
                DelegateTestCase.Create(
                    IbanNetOptionsBuilderExtensions.UseRegistry,
                    instance,
                    Substitute.For<IIbanRegistry>()),
                DelegateTestCase.Create(
                    IbanNetOptionsBuilderExtensions.UseRegistryProvider,
                    instance,
                    Array.Empty<IIbanRegistryProvider>()),
                DelegateTestCase.Create(
                    IbanNetOptionsBuilderExtensions.WithRule<TestValidationRule>,
                    instance),
                DelegateTestCase.Create(
                    IbanNetOptionsBuilderExtensions.WithRule,
                    instance,
                    () => new TestValidationRule())
            }.Flatten();
        }
    }
}
