using IbanNet.Registry;
using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules;

public class RejectCountryRuleTests
{
    [Theory]
    [InlineData("NL91ABNA0417164300")]
    [InlineData("BE68539007547034")]
    public void Given_that_country_code_is_rejected_when_validating_it_should_return_return_error(string value)
    {
        var sut = new RejectCountryRule(["NL", "BE"]);
        IbanCountry ibanCountry = IbanRegistry.Default[value.Substring(0, 2)];

        // Act
        ValidationRuleResult actual = sut.Validate(new ValidationRuleContext(value, ibanCountry));

        // Assert
        actual.Should()
            .BeOfType<CountryNotAcceptedResult>()
            .Which.ErrorMessage.Should()
            .Be(string.Format(Resources.CountryNotAcceptedResult_Bank_account_numbers_from_country_0_are_not_accepted, ibanCountry.DisplayName));
    }

    [Theory]
    [InlineData("NL91ABNA0417164300")]
    [InlineData("BE68539007547034")]
    public void Given_that_country_code_is_not_rejected_when_validating_it_should_return_success(string value)
    {
        var sut = new RejectCountryRule(["DE", "FR"]);
        IbanCountry ibanCountry = IbanRegistry.Default[value.Substring(0, 2)];

        // Act
        ValidationRuleResult actual = sut.Validate(new ValidationRuleContext(value, ibanCountry));

        // Assert
        actual.Should().Be(ValidationRuleResult.Success);
    }

    [Fact]
    public void Given_that_list_is_null_when_creating_rule_it_should_throw()
    {
        IEnumerable<string>? rejectedCountryCodes = null;

        // Act
        Func<RejectCountryRule> act = () => new RejectCountryRule(rejectedCountryCodes!);

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(rejectedCountryCodes));
    }

    [Fact]
    public void Given_that_list_is_empty_when_creating_rule_it_should_throw()
    {
        IEnumerable<string> rejectedCountryCodes = [];

        // Act
        Func<RejectCountryRule> act = () => new RejectCountryRule(rejectedCountryCodes);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage(Resources.ArgumentException_At_least_one_country_code_must_be_provided + "*")
            .WithParameterName(nameof(rejectedCountryCodes))
            .Which.Should().BeOfType<ArgumentException>();
    }

    [Fact]
    public void Given_that_context_is_null_when_validating_it_should_throw()
    {
        var sut = new RejectCountryRule(["DE", "FR"]);
        ValidationRuleContext? context = null;

        // Act
        Func<ValidationRuleResult> act = () => sut.Validate(context!);

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(nameof(context));
    }
}
