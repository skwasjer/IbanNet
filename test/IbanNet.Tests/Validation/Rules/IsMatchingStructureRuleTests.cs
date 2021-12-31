using IbanNet.Registry;
using IbanNet.Registry.Swift;
using IbanNet.Validation.Results;

namespace IbanNet.Validation.Rules
{
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

        [Fact]
        public void Given_invalid_value_when_validating_it_should_return_error()
        {
            const string testValue = "XXXX";
            var country = new IbanCountry("NL")
            {
                Iban = new IbanStructure(new IbanSwiftPattern("NL2!n"))
            };

            // Act
            ValidationRuleResult actual = _sut.Validate(new ValidationRuleContext(testValue)
            {
                Country = country
            });

            // Assert
            actual.Should().BeOfType<InvalidStructureResult>();
        }
    }
}
