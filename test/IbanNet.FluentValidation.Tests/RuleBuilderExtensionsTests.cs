using FluentValidation;
using FluentValidation.Validators;

namespace IbanNet.FluentValidation;

public class RuleBuilderExtensionsTests
{
    [Fact]
    public void Given_null_builder_when_registering_validator_it_should_throw()
    {
        IRuleBuilder<object, string>? ruleBuilder = null;

        // Act
        Action act = () => ruleBuilder!.Iban(Substitute.For<IIbanValidator>());

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(ruleBuilder));
    }

    [Fact]
    public void Given_null_validator_when_registering_validator_it_should_throw()
    {
        IRuleBuilder<object, string> ruleBuilder = Substitute.For<IRuleBuilder<object, string>>();
        IIbanValidator? ibanValidator = null;

        // Act
        Action act = () => ruleBuilder.Iban(ibanValidator!);

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(ibanValidator));
    }

    [Fact]
    public void When_registering_validator_it_should_add_validator_to_rule()
    {
        IRuleBuilder<object, string>? ruleBuilderMock = Substitute.For<IRuleBuilder<object, string>>();
        IIbanValidator ibanValidator = Substitute.For<IIbanValidator>();

        // Act
        ruleBuilderMock.Iban(ibanValidator);

        // Assert
        ruleBuilderMock.Received(1).SetValidator(Arg.Is<IPropertyValidator<object, string>>(x => x is FluentIbanValidator<object>));
    }
}
