using System;
using FluentAssertions;
using IbanNet.Registry;
using TestHelpers.Fixtures;
using Xunit;

namespace TestHelpers.Specs
{
    public abstract class ShouldResolveRegistry : DiSpec
    {
        protected ShouldResolveRegistry(IDependencyInjectionFixture fixture) : base(fixture)
        {
        }

        protected override void Given()
        {
            Fixture.Configure(builder => { });
        }

        [Fact]
        public void When_resolving_registry_it_should_not_throw()
        {
            // Assert
            Func<IIbanRegistry> act = () => Subject.GetService<IIbanRegistry>();

            // Act
            act.Should().NotThrow().Which.Should().NotBeNull();
        }

        [Fact]
        public void When_resolving_twice_it_should_return_same_instance()
        {
            // Assert
            IIbanRegistry first = Subject.GetService<IIbanRegistry>();
            IIbanRegistry second = Subject.GetService<IIbanRegistry>();

            // Act
            first.Should().BeSameAs(second);
        }
    }
}
