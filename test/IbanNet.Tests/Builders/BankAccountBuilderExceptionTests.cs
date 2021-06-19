using System;
using FluentAssertions;
using Newtonsoft.Json;
using TestHelpers.Specs;
using Xunit;

namespace IbanNet.Builders
{
    public class BankAccountBuilderExceptionTests : BaseExceptionTests<BankAccountBuilderException>
    {
#if NETFRAMEWORK || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0
        [Fact]
        public void Given_exception_with_parameters_it_should_serialize_and_deserialize()
        {
            var exception = new BankAccountBuilderException("some error");

            string jsonWithException = JsonConvert.SerializeObject(exception);

            // Act
            Exception actual = JsonConvert.DeserializeObject<BankAccountBuilderException>(jsonWithException);

            // Assert
            actual.Should().BeEquivalentTo(exception);
        }
#endif
    }
}
