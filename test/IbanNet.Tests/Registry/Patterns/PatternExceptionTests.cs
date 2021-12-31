using Newtonsoft.Json;
using TestHelpers.Specs;

namespace IbanNet.Registry.Patterns
{
    public class PatternExceptionTests : BaseExceptionTests<PatternException>
    {
#if !NETSTD_LEGACY
        [Fact]
        public void Given_validation_result_it_should_serialize_and_deserialize_and_ignore_result()
        {
            var exception = new PatternException("some message");

            string jsonWithException = JsonConvert.SerializeObject(exception);

            // Act
            Exception actual = JsonConvert.DeserializeObject<PatternException>(jsonWithException);

            // Assert
            actual.Should().BeEquivalentTo(exception);
        }
#endif
    }
}
