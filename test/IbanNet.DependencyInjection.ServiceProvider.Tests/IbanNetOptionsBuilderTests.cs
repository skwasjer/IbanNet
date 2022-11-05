using IbanNet.Validation.Rules;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TestHelpers;
using TestHelpers.FakeRules;
using TestHelpers.FluentAssertions;

namespace IbanNet.DependencyInjection.ServiceProvider;

public class IbanNetOptionsBuilderTests
{
    private readonly IServiceCollection _serviceCollection;
    private readonly MicrosoftDependencyInjectionIbanNetOptionsBuilder _builder;

    protected IbanNetOptionsBuilderTests()
    {
        _serviceCollection = new ServiceCollection()
            .AddTransient<DependencyResolverAdapter, ServiceProviderDependencyResolverAdapter>();
        _builder = new MicrosoftDependencyInjectionIbanNetOptionsBuilder(_serviceCollection);
    }

    public class ExtensionTests : IbanNetOptionsBuilderTests
    {
        [Fact]
        public void Given_rule_is_configured_via_factory_and_serviceCollection_it_should_add_instance_to_rule_collection()
        {
            var configuredRule = new TestValidationRule();
            IServiceProvider providedServiceProvider = null;
            IIbanNetOptionsBuilder returnedBuilder = _builder
                .WithRule(s =>
                {
                    providedServiceProvider = s;
                    return configuredRule;
                });

            // Act
            IServiceProvider services = _serviceCollection.BuildServiceProvider();

            // Assert
            IbanValidatorOptions opts = services.GetService<IOptions<IbanValidatorOptions>>().Value;
            opts.Should()
                .HaveRule<IIbanValidationRule>()
                .And.HaveCount(1)
                .And.Subject.Single()
                .Should()
                .BeSameAs(configuredRule);
            providedServiceProvider.Should().NotBeNull();
            returnedBuilder.Should().BeSameAs(_builder);
        }
    }

    public class NullArgumentTests : IbanNetOptionsBuilderTests
    {
        [Theory]
        [MemberData(nameof(BuilderExtensionsWithoutBuilderInstance))]
        public void Given_null_instance_when_calling_method_it_should_throw(params object[] args)
        {
            NullArgumentTest.Execute(args);
        }

        public static IEnumerable<object[]> BuilderExtensionsWithoutBuilderInstance()
        {
            var instance = new MicrosoftDependencyInjectionIbanNetOptionsBuilder(new ServiceCollection());

            return new NullArgumentTestCases
            {
                // Instance
                DelegateTestCase.Create<Action<DependencyResolverAdapter, IbanValidatorOptions>, IIbanNetOptionsBuilder>(
                    instance.Configure,
                    (s, o) => { }),

                // Extensions
                DelegateTestCase.Create<IIbanNetOptionsBuilder, Func<IServiceProvider, TestValidationRule>, IIbanNetOptionsBuilder>(
                    IbanNetOptionsBuilderExtensions.WithRule,
                    instance,
                    s => new TestValidationRule()),
                DelegateTestCase.Create<IIbanNetOptionsBuilder, Action<IServiceProvider, IbanValidatorOptions>, IIbanNetOptionsBuilder>(
                    IbanNetOptionsBuilderExtensions.Configure,
                    instance,
                    (s, opts) => { })
            }.Flatten();
        }
    }
}