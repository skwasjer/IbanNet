using System;
using FluentAssertions;
using IbanNet.Registry;
using IbanNet.Validation.Results;
using Newtonsoft.Json;
using TestHelpers.Specs;
using Xunit;

namespace IbanNet
{
    public class IbanFormatExceptionTests : BaseExceptionTests<IbanFormatException>
    {
        [Fact]
        public void When_creating_instance_with_validation_result_it_should_set_properties()
        {
            const string message = "message";
            var result = new ValidationResult();

            // Act
            var actual = new IbanFormatException(message, result);

            // Assert
            actual.Message.Should().Be(message);
            actual.Result.Should().Be(result);
        }

#if NETFRAMEWORK || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0
        [Fact]
        public void Given_exception_with_parameters_it_should_serialize_and_deserialize()
        {
            new IbanRegistry().TryGetValue("NL", out IbanCountry ibanCountry);

            const string message = "message";

            var result = new ValidationResult
            {
                AttemptedValue = "iban",
                Country = ibanCountry,
                Error = new IllegalCharactersResult()
            };

            var exception = new IbanFormatException(message, result);

            string jsonWithException = JsonConvert.SerializeObject(exception);

            // Act
            Exception actual = JsonConvert.DeserializeObject<IbanFormatException>(jsonWithException);

            // Assert
            IbanFormatException actualTyped = actual.Should()
                .BeOfType<IbanFormatException>()
                .Which;
            actualTyped.Should()
                .BeEquivalentTo(exception, opts => opts.Excluding(ex => ex.Result));
            actualTyped.Result.Should().BeNull($"no serialization support is implemented for {nameof(ValidationResult)}");
        }
#endif
    }
}
