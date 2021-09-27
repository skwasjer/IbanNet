#if NET5_0_OR_GREATER
using System;
using System.Text;
using System.Text.Json;
using IbanNet.Registry;
using Moq;
using TestHelpers;
using Xunit;

namespace IbanNet.Json
{
    [Collection(nameof(SetsStaticValidator))]
    public class IbanJsonConverterTests
    {
        [Fact]
        public void When_creating_default_instance_it_should_use_static_validator()
        {
            const string ibanStr = "NL91ABNA0417164300";
            var validatorStub = new IbanValidatorStub();

            ReadOnlySpan<byte> buffer = Encoding.UTF8.GetBytes($"\"{ibanStr}\"");
            var reader = new Utf8JsonReader(buffer);
            reader.Read();

            Iban.Validator = validatorStub;
            try
            {
                var sut = new IbanJsonConverter();

                // Act
                sut.Read(ref reader, typeof(Iban), new JsonSerializerOptions());

                // Assert
                validatorStub.Verify(m => m.Validate(ibanStr), Times.Once);
            }
            finally
            {
                Iban.Validator = null!;
            }
        }

        [Fact]
        public void Given_that_parser_is_used_when_creating_instance_it_should_use_parser()
        {
            const string ibanStr = "NL91ABNA0417164300";

            Iban outIban = null;
            var parserMock = new Mock<IIbanParser>();
            parserMock
                .Setup(m => m.TryParse(It.IsAny<string>(), out outIban))
                .Returns(true)
                .Verifiable();

            ReadOnlySpan<byte> buffer = Encoding.UTF8.GetBytes($"\"{ibanStr}\"");
            var reader = new Utf8JsonReader(buffer);
            reader.Read();

            var sut = new IbanJsonConverter(parserMock.Object);

            // Act
            sut.Read(ref reader, typeof(Iban), new JsonSerializerOptions());

            // Assert
            parserMock.Verify();
        }
    }
}
#endif
