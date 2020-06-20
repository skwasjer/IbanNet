using System;
using FluentAssertions;
using IbanNet;
using IbanNet.Registry;
using TestHelpers.Fixtures;
using Xunit;

namespace TestHelpers.Specs
{
    public abstract class ShouldUseDefaultRegistrySpec : DiSpec
    {
        private IIbanValidator _initialValidator;

        protected ShouldUseDefaultRegistrySpec(IDependencyInjectionFixture fixture)
            : base(fixture)
        {
        }

        protected override void Given()
        {
            _initialValidator = Iban.Validator;
            Fixture.Configure(builder => { });
        }

        [Fact]
        public void Given_registry_is_not_configured_when_resolving_it_should_not_throw()
        {
            // Act & assert
            Func<IIbanValidator> act2 = () => Subject.GetRequiredService<IIbanValidator>();
            act2.Should()
                .NotThrow("it should use default registry if none is set")
                .Which.Should()
                .NotBeNull()
                .And.Subject
                .Should()
                .BeOfType<IbanValidator>()
                .Which.Options.Registry.Should()
                .BeEquivalentTo(IbanRegistry.Default, opts => opts.WithStrictOrdering());
        }

        [Fact]
        public void When_resolving_it_should_not_preserve_static_validator()
        {
            IIbanValidator validator = Subject.GetRequiredService<IIbanValidator>();
            validator.Should().NotBeSameAs(_initialValidator);
        }
    }
}
