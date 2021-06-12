using System;
using FluentAssertions;
using IbanNet.Registry;
using TestHelpers.Fixtures;
using Xunit;

namespace TestHelpers.Specs
{
    public abstract class ShouldResolveGeneratorSpec : DiSpec
    {
        protected ShouldResolveGeneratorSpec(IDependencyInjectionFixture fixture) : base(fixture)
        {
        }

        protected override void Given()
        {
            Fixture.Configure(builder => { });
        }

        [Fact]
        public void When_resolving_generator_it_should_not_throw()
        {
            // Assert
            Func<IIbanGenerator> act = () => Subject.GetService<IIbanGenerator>();

            // Act
            act.Should().NotThrow().Which.Should().NotBeNull();
        }
    }
}
