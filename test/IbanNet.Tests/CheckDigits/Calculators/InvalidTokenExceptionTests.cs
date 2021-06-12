using System;
using FluentAssertions;
using Newtonsoft.Json;
using TestHelpers.Specs;
using Xunit;

namespace IbanNet.CheckDigits.Calculators
{
    public class InvalidTokenExceptionTests : BaseExceptionTests<InvalidTokenException>
    {
#if NETFRAMEWORK || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0
        [Fact]
        public void Given_exception_with_parameters_it_should_serialize_and_deserialize()
        {
            var exception = new InvalidTokenException(23, 'c');

            string jsonWithException = JsonConvert.SerializeObject(exception);

            // Act
            Exception actual = JsonConvert.DeserializeObject<InvalidTokenException>(jsonWithException);

            // Assert
            actual.Should().BeEquivalentTo(exception);
        }
#endif
    }
}
