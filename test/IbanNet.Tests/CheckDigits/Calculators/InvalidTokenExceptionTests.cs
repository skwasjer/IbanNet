using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
        public void Given_validation_result_it_should_serialize_and_deserialize_and_ignore_result()
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
