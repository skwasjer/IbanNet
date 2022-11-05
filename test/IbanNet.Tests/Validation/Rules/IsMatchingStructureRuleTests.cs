using IbanNet.Registry;
using IbanNet.Registry.Swift;
using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules;

public class IsMatchingStructureRuleTests
{
    private readonly IsMatchingStructureRule _sut;

    public IsMatchingStructureRuleTests()
    {
        _sut = new IsMatchingStructureRule();
    }

    [Fact]
    public void Given_no_country_when_validating_it_should_return_error()
    {
        ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext(string.Empty));

        // Assert
        actual.Should().BeOfType<InvalidStructureResult>();
    }

    [Fact]
    public void Given_valid_value_when_validating_it_should_return_success()
    {
        const string testValue = "AD1200012030200359100100";
        var country = new IbanCountry("AD")
        {
            Iban = new IbanStructure(new IbanSwiftPattern("AD2!n4!n4!n12!c"))
        };

        // Act
        ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext(testValue)
        {
            Country = country
        });

        // Assert
        actual.Should().Be(ValidationRuleResult.Success);
    }

    [Theory]
    [InlineData("XXXX", 2, "the country code is only being tested against upper case")]
    [InlineData("XX12ABCD", 7, "the input is too long")]
    [InlineData("XX12AB", 6, "the input is not long enough")]
    public void Given_invalid_value_when_validating_it_should_return_error(string testValue, int expectedErrorPos, string because)
    {
        var country = new IbanCountry("NL")
        {
            Iban = new IbanStructure(new IbanSwiftPattern("NL2!n3!a"))
        };

        // Act
        ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext(testValue)
        {
            Country = country
        });

        // Assert
        actual.Should()
            .BeOfType<InvalidStructureResult>()
            .Which.Position.Should()
            .Be(expectedErrorPos, because);
    }
}