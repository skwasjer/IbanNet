using IbanNet;
using TestHelpers.Fixtures;

namespace TestHelpers.Specs;

public abstract class ShouldSetStaticValidatorSpec : DiSpec
{
    private IIbanValidator _initialValidator;

    protected ShouldSetStaticValidatorSpec(IDependencyInjectionFixture fixture) : base(fixture)
    {
    }

    protected override void Given()
    {
        Iban.Validator = _initialValidator = Mock.Of<IIbanValidator>();
        Fixture.Configure(builder => { });
    }

    [Fact]
    public void When_resolving_it_should_set_static_validator()
    {
        // Assert
        IIbanValidator validator = Subject.GetService<IIbanValidator>();

        // Act
        Iban.Validator.Should()
            .NotBeSameAs(_initialValidator)
            .And.BeSameAs(validator);
    }
}