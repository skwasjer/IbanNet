using System;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Validators;
using Moq;
using Xunit;

namespace IbanNet.FluentValidation
{
    public class RuleBuilderExtensionsTests
    {
        [Fact]
        public void Given_null_builder_when_registering_validator_it_should_throw()
        {
            IRuleBuilder<object, string> ruleBuilder = null;

            // Act
            // ReSharper disable once AssignNullToNotNullAttribute
            Action act = () => ruleBuilder.Iban(Mock.Of<IIbanValidator>());

            // Assert
            act.Should()
                .ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should()
                .Be(nameof(ruleBuilder));
        }

        [Fact]
        public void Given_null_validator_when_registering_validator_it_should_throw()
        {
            IRuleBuilder<object, string> ruleBuilder = Mock.Of<IRuleBuilder<object, string>>();
            IIbanValidator ibanValidator = null;

            // Act
            // ReSharper disable once AssignNullToNotNullAttribute
            Action act = () => ruleBuilder.Iban(ibanValidator);

            // Assert
            act.Should()
                .ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should()
                .Be(nameof(ibanValidator));
        }

        [Fact]
        public void When_registering_validator_it_should_add_validator_to_rule()
        {
            var ruleBuilderMock = new Mock<IRuleBuilder<object, string>>();
            IIbanValidator ibanValidator = Mock.Of<IIbanValidator>();

            // Act
            // ReSharper disable once AssignNullToNotNullAttribute
            ruleBuilderMock.Object.Iban(ibanValidator);

            // Assert
            ruleBuilderMock.Verify(m => m.SetValidator(It.Is<IPropertyValidator<object, string>>(x => x.GetType() == typeof(FluentIbanValidator<object>))), Times.Once);
        }
    }
}
