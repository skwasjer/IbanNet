using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using IbanNet.DependencyInjection.FluentAssertions;
using IbanNet.FakeRules;
using IbanNet.Registry;
using IbanNet.Registry.Swift;
using Moq;
using TestHelpers;
using Xunit;

namespace IbanNet.DependencyInjection
{
    public class IbanNetOptionsBuilderTests
    {
        private readonly Mock<IIbanNetOptionsBuilder> _builderStub;
        private readonly IIbanNetOptionsBuilder _builder;

        protected IbanNetOptionsBuilderTests()
        {
            _builder = GetBuilderStub();
            _builderStub = Mock.Get(_builder);
        }

        private static IIbanNetOptionsBuilder GetBuilderStub()
        {
            var builderMock = new Mock<IIbanNetOptionsBuilder>();
            builderMock
                .Setup(m => m.Configure(It.IsAny<Action<DependencyResolverAdapter, IbanValidatorOptions>>()))
                .Returns(builderMock.Object);

            return builderMock.Object;
        }

        public class ExtensionTests : IbanNetOptionsBuilderTests
        {
            [Fact]
            public void Given_registry_is_configured_it_should_set_registry()
            {
                IEnumerable<IbanCountry> limitedCountries = IbanRegistry.Default
                    .Where((country, i) => i % 2 == 0)
                    .ToList();

                // Act
                IIbanNetOptionsBuilder returnedBuilder = _builder.UseRegistry(new IbanRegistry
                {
                    Providers =
                    {
                        new IbanRegistryListProvider(limitedCountries)
                    }
                });

                // Assert
                _builderStub.Should().HaveConfiguredRegistry(limitedCountries);
                returnedBuilder.Should().BeSameAs(_builderStub.Object);
            }

            [Fact]
            public void Given_registry_without_providers_is_configured_it_should_throw()
            {
                var registry = new IbanRegistry();

                // Act
                Action act = () => _builder.UseRegistry(registry);

                // Assert
                act.Should()
                    .Throw<ArgumentException>()
                    .WithMessage(Resources.The_registry_has_no_providers + "*")
                    .And.ParamName.Should()
                    .Be(nameof(registry));
            }

            [Fact]
            public void Given_registry_provider_is_configured_it_should_use_provider()
            {
                var customProvider = new IbanRegistryListProvider(new[] { new IbanCountry("XX") });

                // Act
                IIbanNetOptionsBuilder returnedBuilder = _builder.UseRegistryProvider(customProvider);

                // Assert
                _builderStub.Should().HaveConfiguredRegistry(customProvider);
                returnedBuilder.Should().BeSameAs(_builderStub.Object);
            }

            [Fact]
            public void Given_multiple_registry_providers_are_configured_it_should_use_providers()
            {
                var customProvider = new IbanRegistryListProvider(new[] { new IbanCountry("XX") });

                // Act
                IIbanNetOptionsBuilder returnedBuilder = _builder.UseRegistryProvider(new SwiftRegistryProvider(), customProvider);

                // Assert
                _builderStub.Should().HaveConfiguredRegistry(new SwiftRegistryProvider().Concat(customProvider));
                returnedBuilder.Should().BeSameAs(_builderStub.Object);
            }

            [Fact]
            public void Given_rule_is_configured_via_factory_it_should_add_instance_to_rule_collection()
            {
                var configuredRule = new TestValidationRule();

                // Act
                IIbanNetOptionsBuilder returnedBuilder = _builder.WithRule(() => configuredRule);

                // Assert
                _builderStub.Should()
                    .HaveConfiguredRule<TestValidationRule>()
                    .And.Contain(configuredRule);
                returnedBuilder.Should().BeSameAs(_builderStub.Object);
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

            public static IEnumerable<object[]> NullArgumentTestCases()
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
                        Mock.Of<IIbanRegistry>()),
                    DelegateTestCase.Create(
                        IbanNetOptionsBuilderExtensions.UseRegistryProvider,
                        instance,
                        new IIbanRegistryProvider[0]),
                    DelegateTestCase.Create(
                        IbanNetOptionsBuilderExtensions.WithRule<TestValidationRule>,
                        instance),
                    DelegateTestCase.Create<IIbanNetOptionsBuilder, Func<TestValidationRule>, IIbanNetOptionsBuilder>(
                        IbanNetOptionsBuilderExtensions.WithRule,
                        instance,
                        () => new TestValidationRule())
                }.Flatten();
            }
        }
    }
}
