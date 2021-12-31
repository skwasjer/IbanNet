using Newtonsoft.Json;
using TestHelpers.Specs;

namespace IbanNet.CheckDigits.Calculators
{
    public class InvalidTokenExceptionTests : BaseExceptionTests<InvalidTokenException>
    {
#if !NETSTD_LEGACY
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
