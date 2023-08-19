﻿#if NET6_0_OR_GREATER
using System.Text;
using System.Text.Json;
using IbanNet.Registry;
using TestHelpers;

namespace IbanNet.Json;

[Collection(nameof(SetsStaticValidator))]
public class IbanJsonConverterTests
{
    [Fact]
    public void When_creating_default_instance_it_should_use_static_validator()
    {
        const string ibanStr = "NL91ABNA0417164300";
        IIbanValidator validatorStub = IbanValidatorStub.Create();

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
            validatorStub.Received(1).Validate(ibanStr);
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

        IIbanParser parserMock = Substitute.For<IIbanParser>();
        parserMock
            .TryParse(Arg.Any<string>(), out Arg.Any<Iban?>())
            .Returns(true);

        ReadOnlySpan<byte> buffer = Encoding.UTF8.GetBytes($"\"{ibanStr}\"");
        var reader = new Utf8JsonReader(buffer);
        reader.Read();

        var sut = new IbanJsonConverter(parserMock);

        // Act
        sut.Read(ref reader, typeof(Iban), new JsonSerializerOptions());

        // Assert
        parserMock.Received(1).TryParse(ibanStr, out Arg.Any<Iban?>());
    }

    [Fact]
    public void Given_that_a_complex_record_has_an_iban_when_deserializing_it_should_equal_expected()
    {
        var parser = new IbanParser(IbanRegistry.Default);

        var payment1 = new Payment(parser.Parse("NL91 ABNA 0417 1643 00"), 100M);
        const string expectedJson = "{\"BankAccountNumber\":\"NL91ABNA0417164300\",\"Amount\":100}";

        // Act
        string json = JsonSerializer.Serialize(payment1);
        Payment? payment2 = JsonSerializer.Deserialize<Payment>(json);

        // Assert
        json.Should().Be(expectedJson);
        payment2.Should().Be(payment1);
    }

    [Fact]
    public void Given_that_a_complex_record_has_a_null_iban_when_deserializing_it_should_equal_expected()
    {
        var payment1 = new Payment(null!, 100M);
        const string expectedJson = "{\"BankAccountNumber\":null,\"Amount\":100}";

        // Act
        string json = JsonSerializer.Serialize(payment1);
        Payment? payment2 = JsonSerializer.Deserialize<Payment>(json);

        // Assert
        json.Should().Be(expectedJson);
        payment2.Should().Be(payment1);
    }

    [Theory]
    [MemberData(nameof(WriterNullArgTestCases))]
    public void Given_that_write_argument_is_null_when_writing_it_should_throw(Utf8JsonWriter writer, Iban value, string expectedParamName)
    {
        var sut = new IbanJsonConverter(Substitute.For<IIbanParser>());

        // Act
        Action act = () => sut.Write(writer, value, new JsonSerializerOptions());

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName(expectedParamName);
    }

    public static IEnumerable<object?[]> WriterNullArgTestCases()
    {
        var parser = new IbanParser(IbanRegistry.Default);
        var writer = new Utf8JsonWriter(Stream.Null);
        Iban value = parser.Parse("NL91 ABNA 0417 1643 00");
        yield return new object?[] { null, value, nameof(writer) };
        yield return new object?[] { writer, null, nameof(value) };
    }

    public record Payment(Iban BankAccountNumber, decimal Amount);
}
#endif
