using Autofac;
using IbanNet.Validation.Rules;
using TestHelpers;
using TestHelpers.FakeRules;
using TestHelpers.FluentAssertions;

namespace IbanNet.DependencyInjection.Autofac;

public class IbanNetOptionsBuilderTests
{
    private readonly AutofacIbanNetOptionsBuilder _builder;
    private readonly ContainerBuilder _containerBuilder;

    protected IbanNetOptionsBuilderTests()
    {
        var module = new IbanNetModule(true);
        _builder = new AutofacIbanNetOptionsBuilder(module);
        _containerBuilder = new ContainerBuilder();
        _containerBuilder.RegisterModule(module);
    }

    public class ExtensionTests : IbanNetOptionsBuilderTests
    {
        [Fact]
        public void Given_rule_is_configured_via_factory_and_serviceCollection_it_should_add_instance_to_rule_collection()
        {
            var configuredRule = new TestValidationRule();
            IComponentContext providedComponentContext = null;
            IIbanNetOptionsBuilder returnedBuilder = _builder
                .WithRule(ctx =>
                {
                    providedComponentContext = ctx;
                    return configuredRule;
                });

            // Act
            IContainer container = _containerBuilder.Build();

            // Assert
            IbanValidatorOptions opts = container.Resolve<IbanValidatorOptions>();
            opts.Should()
                .HaveRule<IIbanValidationRule>()
                .And.HaveCount(1)
                .And.Subject.Single()
                .Should()
                .BeSameAs(configuredRule);
            providedComponentContext.Should().NotBeNull();
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
            var instance = new AutofacIbanNetOptionsBuilder(new IbanNetModule(true));

            return new NullArgumentTestCases
            {
                // Instance
                DelegateTestCase.Create<Action<DependencyResolverAdapter, IbanValidatorOptions>, IIbanNetOptionsBuilder>(
                    instance.Configure,
                    (s, o) => { }),

                // Extensions
                DelegateTestCase.Create<IIbanNetOptionsBuilder, Func<IComponentContext, TestValidationRule>, IIbanNetOptionsBuilder>(
                    IbanNetOptionsBuilderExtensions.WithRule,
                    instance,
                    s => new TestValidationRule()),
                DelegateTestCase.Create<IIbanNetOptionsBuilder, Action<IComponentContext, IbanValidatorOptions>, IIbanNetOptionsBuilder>(
                    IbanNetOptionsBuilderExtensions.Configure,
                    instance,
                    (s, opts) => { })
            }.Flatten();
        }
    }
}