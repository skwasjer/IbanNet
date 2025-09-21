using IbanNet.Registry;
using IbanNet.Validation.Results;

namespace IbanNet;

public class ValidationResultTests
{
    private static readonly ValidationResult SuccessResult = new() { AttemptedValue = "NL91ABNA0417164300", Country = IbanRegistry.Default["NL"] };

    [Fact]
    public void Given_that_result_is_created_with_default_ctor_when_getting_isValid_it_should_return_false()
    {
        var result = new ValidationResult();
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Given_that_result_is_success_when_getting_isValid_it_should_return_true()
    {
        SuccessResult.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Given_that_result_has_an_error_when_getting_isValid_it_should_return_false()
    {
        ValidationResult sut = SuccessResult with { Error = new ErrorResult("Error") };

        sut.Error.Should().NotBeNull();
        sut.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Given_that_result_is_missing_country_when_getting_isValid_it_should_return_false()
    {
        ValidationResult sut = SuccessResult with { Country = null };

        sut.Country.Should().BeNull();
        sut.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Given_that_result_is_missing_attemptedValue_when_getting_isValid_it_should_return_false()
    {
        ValidationResult sut = SuccessResult with { AttemptedValue = null };

        sut.AttemptedValue.Should().BeNull();
        sut.IsValid.Should().BeFalse();
    }
}
