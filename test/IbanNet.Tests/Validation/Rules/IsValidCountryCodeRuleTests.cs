using IbanNet.Registry;
using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules;

public class IsValidCountryCodeRuleTests
{
    [Fact]
    public void Given_no_country_code_when_validating_it_should_return_error()
    {
        var context = new ValidationRuleContext(string.Empty);
        var sut = new IsValidCountryCodeRule(new IbanRegistry
        {
            Providers =
            {
                new IbanRegistryListProvider(Array.Empty<IbanCountry>())
            }
        });

        // Act
        ValidationRuleResult actual = sut.Validate(new ValidationRuleContext(string.Empty));

        // Assert
        actual.Should().BeOfType<UnknownCountryCodeResult>();
        context.Country.Should().BeNull();
    }

    [Fact]
    public void Given_known_country_code_when_validating_it_should_return_success()
    {
        var context = new ValidationRuleContext("XX");
        var country = new IbanCountry("XX");
        var sut = new IsValidCountryCodeRule(new IbanRegistry
        {
            Providers =
            {
                new IbanRegistryListProvider(new[] { country })
            }
        });

        // Act
        ValidationRuleResult actual = sut.Validate(context);

        // Assert
        actual.Should().Be(ValidationRuleResult.Success);
        context.Country.Should().BeSameAs(country);
    }

    [Fact]
    public void Given_unknown_country_code_when_validating_it_should_return_error()
    {
        var context = new ValidationRuleContext("XX");
        var sut = new IsValidCountryCodeRule(new IbanRegistry
        {
            Providers =
            {
                new IbanRegistryListProvider(Array.Empty<IbanCountry>())
            }
        });

        // Act
        ValidationRuleResult actual = sut.Validate(new ValidationRuleContext("XX"));

        // Assert
        actual.Should().BeOfType<UnknownCountryCodeResult>();
        context.Country.Should().BeNull();
    }
}