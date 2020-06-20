using FluentAssertions;
using IbanNet;
using IbanNet.DependencyInjection;
using TestHelpers.Fixtures;
using Xunit;

namespace TestHelpers.Specs
{
    public abstract class ConfiguredValidationMethodSpec : DiSpec
    {
        protected ConfiguredValidationMethodSpec(IDependencyInjectionFixture fixture)
            : base(fixture)
        {
        }

        protected override void Given()
        {
        }

        protected override DependencyResolverAdapter CreateSubject()
        {
            return null;
        }

        [Theory]
        [InlineData(null, ValidationMethod.Strict)] // Default to strict mode.
        [InlineData(true, ValidationMethod.Strict)] // Strict mode.
        [InlineData(false, ValidationMethod.Loose)] // Loose mode.
        public void Given_validation_method_when_adding_it_should_use_correct_method(bool? strict, ValidationMethod expectedValidationMethod)
        {
            // Act
            Fixture.Configure(builder =>
            {
                switch (strict)
                {
                    case null:
                        break;
                    case true:
                        builder.UseStrictValidation();
                        break;
                    case false:
                        builder.UseLooseValidation();
                        break;
                }
            });
            DependencyResolverAdapter adapter = Fixture.Build();

            // Assert
            IIbanValidator validator = adapter.GetRequiredService<IIbanValidator>();
            validator.Should()
                .BeOfType<IbanValidator>()
                .Which.Options.Method.Should()
                .Be(expectedValidationMethod);
        }
    }
}
