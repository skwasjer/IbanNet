using FluentValidation;
using FluentValidation.Results;
using IbanNet.Validation.Results;
using ValidationResultAlias = FluentValidation.Results.ValidationResult;

namespace IbanNet.FluentValidation
{
    public class IntegrationTests
    {
        private readonly TestModel _testModel;

        public IntegrationTests()
        {
            _testModel = new TestModel();
        }

        [Theory]
        [MemberData(nameof(InvalidTestCases))]
        public void Given_a_model_with_invalid_iban_when_validating_should_contain_validation_errors
        (
            string attemptedIbanValue,
            bool strict,
            ErrorResult expectedError
        )
        {
            _testModel.BankAccountNumber = attemptedIbanValue;

            const string expectedFormattedPropertyName = "Bank Account Number";
            var expectedValidationFailure = new ValidationFailure(nameof(_testModel.BankAccountNumber), $"'{expectedFormattedPropertyName}' is not a valid IBAN.")
            {
                AttemptedValue = attemptedIbanValue,
                ErrorCode = "FluentIbanValidator",
                FormattedMessagePlaceholderValues = new Dictionary<string, object>
                {
                    { "PropertyName", expectedFormattedPropertyName },
                    { "PropertyValue", attemptedIbanValue },
                    { "Error", expectedError }
                }
            };

            // Act
            var sut = new TestModelValidator(new IbanValidator(), strict);
            ValidationResultAlias actual = sut.Validate(_testModel);

            // Assert
            actual.IsValid.Should().BeFalse("because one validation error should have occurred");
            actual.Errors.Should()
                .HaveCount(1, "because one validation error should have occurred")
                .And.Subject.Single()
                .Should()
                .BeEquivalentTo(expectedValidationFailure);
        }

        public static IEnumerable<object[]> InvalidTestCases()
        {
            yield return new object[] { "nl91ABNA0417164300", true, new InvalidStructureResult(0) };
            yield return new object[] { "PL611090101400000712198128741", true, new InvalidLengthResult() };
            yield return new object[] { "PL611090101400000712198128741", false, new InvalidLengthResult() };
            yield return new object[] { "PL61 1090 10140000071219812874", true, new IllegalCharactersResult(4) };
            yield return new object[] { "AE07033123456789012345", true, new InvalidLengthResult() };
            yield return new object[] { "AE07033123456789012345", false, new InvalidLengthResult() };
            yield return new object[] { "AE070 331 234567890123456", true, new IllegalCharactersResult(5) };
            yield return new object[] { "MT84malt011000012345mtlcast001S", true, new InvalidStructureResult(4) };
        }

        [Theory]
        [InlineData("nl91ABNA0417164300", false)]
        [InlineData("PL61109010140000071219812874", true)]
        [InlineData("PL61109010140000071219812874", false)]
        [InlineData("PL61 1090 10140000071219812874", false)]
        [InlineData("AE070331234567890123456", true)]
        [InlineData("AE070331234567890123456", false)]
        [InlineData("AE07 0331 234567890123456", false)]
        [InlineData("MT84MALT011000012345mtlcast001S", true)]
        [InlineData("MT84malt011000012345mtlcast001S", false)]
        public void Given_a_model_with_iban_when_validating_should_not_contain_validation_errors(string attemptedIbanValue, bool strict)
        {
            _testModel.BankAccountNumber = attemptedIbanValue;

            // Act
            var sut = new TestModelValidator(new IbanValidator(), strict);
            ValidationResultAlias actual = sut.Validate(_testModel);

            // Assert
            actual.IsValid.Should().BeTrue("because no validation errors should have occurred");
        }

        private class TestModelValidator : AbstractValidator<TestModel>
        {
            public TestModelValidator(IIbanValidator ibanValidator, bool strict)
            {
                RuleFor(x => x.BankAccountNumber).Iban(ibanValidator, strict);
            }
        }
    }
}
