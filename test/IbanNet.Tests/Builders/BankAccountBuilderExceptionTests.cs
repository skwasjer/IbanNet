using Newtonsoft.Json;
using TestHelpers.Specs;

namespace IbanNet.Builders;

public class BankAccountBuilderExceptionTests : BaseExceptionTests<BankAccountBuilderException>
{
#if !NETSTD_LEGACY
    [Fact]
    public void Given_exception_with_parameters_it_should_serialize_and_deserialize()
    {
        var exception = new BankAccountBuilderException("some error");

        string jsonWithException = JsonConvert.SerializeObject(exception);

        // Act
        Exception? actual = JsonConvert.DeserializeObject<BankAccountBuilderException>(jsonWithException);

        // Assert
        actual.Should().BeEquivalentTo(exception);
    }
#endif
}
