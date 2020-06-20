using System;
using FluentAssertions;
using IbanNet;
using TestHelpers.Fixtures;
using Xunit;

namespace TestHelpers.Specs
{
    public abstract class ShouldResolveParserSpec : DiSpec
    {
        protected ShouldResolveParserSpec(IDependencyInjectionFixture fixture) : base(fixture)
        {
        }

        protected override void Given()
        {
            Fixture.Configure(builder => { });
        }

        [Fact]
        public void When_resolving_parser_it_should_not_throw()
        {
            // Assert
            Func<IIbanParser> act = () => Subject.GetService<IIbanParser>();

            // Act
            act.Should().NotThrow().Which.Should().NotBeNull();
        }
    }
}
