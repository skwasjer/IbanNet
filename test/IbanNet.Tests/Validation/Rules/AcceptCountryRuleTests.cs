using IbanNet.Registry;
using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules;

public class AcceptCountryRuleTests
{
    [Theory]
    [InlineData("NL91ABNA0417164300")]
    [InlineData("BE68539007547034")]
    public void Given_that_country_code_is_accepted_when_validating_it_should_return_success(string value)
    {
        var sut = new AcceptCountryRule(new[] { "NL", "BE" });
        IbanCountry ibanCountry = IbanRegistry.Default[value.Substring(0, 2)];

        // Act
        ValidationRuleResult actual = sut.Validate(new ValidationRuleContext(value, ibanCountry));

        // Assert
        actual.Should().Be(ValidationRuleResult.Success);
    }

    [Theory]
    [InlineData("NL91ABNA0417164300")]
    [InlineData("BE68539007547034")]
    public void Given_that_country_code_is_not_accepted_when_validating_it_should_return_error(string value)
    {
        var sut = new AcceptCountryRule(new[] { "DE", "FR" });
        IbanCountry ibanCountry = IbanRegistry.Default[value.Substring(0, 2)];

        // Act
        ValidationRuleResult actual = sut.Validate(new ValidationRuleContext(value, ibanCountry));

        // Assert
        actual.Should()
            .BeOfType<CountryNotAcceptedResult>()
            .Which.ErrorMessage.Should()
            .Be(string.Format(Resources.CountryNotAcceptedResult_Bank_account_numbers_from_country_0_are_not_accepted, ibanCountry.DisplayName));
    }

    [Fact]
    public void Given_that_list_is_null_when_creating_rule_it_should_throw()
    {
        IEnumerable<string> acceptedCountryCodes = null;

        // Act
        // ReSharper disable once AssignNullToNotNullAttribute
        Func<AcceptCountryRule> act = () => new AcceptCountryRule(acceptedCountryCodes);

        // Assert
        act.Should()
            .ThrowExactly<ArgumentNullException>()
            .Which.ParamName.Should()
            .Be(nameof(acceptedCountryCodes));
    }

    [Fact]
    public void Given_that_list_is_empty_when_creating_rule_it_should_throw()
    {
        IEnumerable<string> acceptedCountryCodes = Enumerable.Empty<string>();

        // Act
        Func<AcceptCountryRule> act = () => new AcceptCountryRule(acceptedCountryCodes);

        // Assert
        act.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage(Resources.ArgumentException_At_least_one_country_code_must_be_provided + "*")
            .Which.ParamName.Should()
            .Be(nameof(acceptedCountryCodes));
    }

    [Fact]
    public void Given_that_context_is_null_when_validating_it_should_throw()
    {
        var sut = new AcceptCountryRule(new[] { "DE", "FR" });
        ValidationRuleContext context = null;

        // Act
        // ReSharper disable once AssignNullToNotNullAttribute
        Func<ValidationRuleResult> act = () => sut.Validate(context);

        // Assert
        act.Should()
            .ThrowExactly<ArgumentNullException>()
            .Which.ParamName.Should()
            .Be(nameof(context));
    }
}
