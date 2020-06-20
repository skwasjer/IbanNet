using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using FluentAssertions;
using IbanNet.Registry;
using IbanNet.Validation.Results;
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

#if NETFRAMEWORK || NETCOREAPP3_0 || NETCOREAPP3_1
        [Fact]
        public void Given_validation_result_it_should_serialize_and_deserialize_and_ignore_result()
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

            using var ms = new MemoryStream();
            var formatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.CrossMachine));

            // Act
            formatter.Serialize(ms, exception);
            ms.Position = 0;
            var actual = formatter.Deserialize(ms) as Exception;

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
