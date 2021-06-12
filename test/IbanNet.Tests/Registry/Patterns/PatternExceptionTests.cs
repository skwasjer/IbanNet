using System;
using FluentAssertions;
using IbanNet.CheckDigits.Calculators;
using Newtonsoft.Json;
using TestHelpers.Specs;
using Xunit;

namespace IbanNet.Registry.Patterns
{
    public class PatternExceptionTests : BaseExceptionTests<PatternException>
    {
#if NETFRAMEWORK || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0
        [Fact]
        public void Given_validation_result_it_should_serialize_and_deserialize_and_ignore_result()
        {
            var exception = new PatternException("some message");

            string jsonWithException = JsonConvert.SerializeObject(exception);

            // Act
            Exception actual = JsonConvert.DeserializeObject<InvalidTokenException>(jsonWithException);

            // Assert
            actual.Should().BeEquivalentTo(exception);
        }
#endif
    }
}
