using IbanNet;
using TestHelpers.Fixtures;

namespace TestHelpers.Specs;

public abstract class ShouldPreserveStaticValidatorSpec : DiSpec
{
    private IIbanValidator _initialValidator = default!;

    protected ShouldPreserveStaticValidatorSpec(IDependencyInjectionFixture fixture) : base(fixture)
    {
    }

    protected override void Given()
    {
        Iban.Validator = _initialValidator = Substitute.For<IIbanValidator>();
        Fixture.Configure(_ => { });
    }

    [Fact]
    public void When_resolving_it_should_not_set_static_validator()
    {
        // Assert
        IIbanValidator? validator = Subject.GetService<IIbanValidator>();

        // Act
        Iban.Validator.Should()
            .BeSameAs(_initialValidator)
            .And.NotBeSameAs(validator);
    }
}
