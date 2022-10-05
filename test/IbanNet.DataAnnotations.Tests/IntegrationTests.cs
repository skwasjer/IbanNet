#if NETCOREAPP
using System.ComponentModel.DataAnnotations;
using IbanNet.DependencyInjection.ServiceProvider;
using IbanNet.Validation.Results;
using Microsoft.Extensions.DependencyInjection;

namespace IbanNet.DataAnnotations
{
    public class IntegrationTests
    {
        [Theory]
        [MemberData(nameof(InvalidTestCases))]
        public void Given_a_model_with_invalid_iban_when_validating_should_contain_validation_errors
        (
            string attemptedIbanValue,
            bool strict,
            ErrorResult expectedError
        )
        {
            using ServiceProvider services = new ServiceCollection()
                .AddIbanNet()
                .BuildServiceProvider();
            var ctx = new ValidationContext(new object(), services, new Dictionary<object, object>());

            // Act
            var sut = new IbanAttribute { Strict = strict };
            System.ComponentModel.DataAnnotations.ValidationResult actual = sut.GetValidationResult(attemptedIbanValue, ctx);

            // Assert
            actual.Should().NotBe(System.ComponentModel.DataAnnotations.ValidationResult.Success, "because one validation error should have occurred");
            ctx.Items.Should()
                .ContainKey("Error")
                .WhoseValue.Should()
                .BeEquivalentTo(expectedError);
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
            using ServiceProvider services = new ServiceCollection()
                .AddIbanNet()
                .BuildServiceProvider();
            var ctx = new ValidationContext(new object(), services, new Dictionary<object, object>());

            // Act
            var sut = new IbanAttribute { Strict = strict };
            System.ComponentModel.DataAnnotations.ValidationResult actual = sut.GetValidationResult(attemptedIbanValue, ctx);

            // Assert
            actual.Should().Be(System.ComponentModel.DataAnnotations.ValidationResult.Success);
        }
    }
}
#endif
