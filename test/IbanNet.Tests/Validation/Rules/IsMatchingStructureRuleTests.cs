using IbanNet.Registry;
using IbanNet.Registry.Patterns;
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
            Iban = new PatternDescriptor(new TestPattern("AD2!n4!n4!n12!c", new SwiftPatternTokenizer()))
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
    [InlineData("XX12ABC", 0, "the country code is different")]
    [InlineData("NA12ABC", 1, "the 2nd char in country code is different")]
    [InlineData("nl12ABC", 0, "the country code is not upper case")]
    [InlineData("NLXX", 2, "the check digits are not numeric")]
    [InlineData("NL12ABCD", 7, "the input is too long")]
    [InlineData("NL12AB", 6, "the input is not long enough")]
    public void Given_invalid_value_when_validating_it_should_return_error(string testValue, int expectedErrorPos, string because)
    {
        var country = new IbanCountry("NL")
        {
            Iban = new PatternDescriptor(new TestPattern("NL2!n3!a", new SwiftPatternTokenizer()))
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
