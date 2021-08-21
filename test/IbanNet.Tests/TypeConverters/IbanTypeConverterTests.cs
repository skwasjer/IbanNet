using System;
using System.ComponentModel;
using System.Globalization;
using FluentAssertions;
using IbanNet.Registry;
using Moq;
using Newtonsoft.Json;
using TestHelpers;
using Xunit;

namespace IbanNet.TypeConverters
{
    [Collection(nameof(SetsStaticValidator))]
    public abstract class IbanTypeConverterTests
    {
        private readonly IbanTypeConverter _sut;

        protected IbanTypeConverterTests()
        {
            _sut = new IbanTypeConverter();
        }

        public class When_converting_from_string : IbanTypeConverterTests
        {
            [Fact]
            public void Should_be_able()
            {
                _sut.CanConvertFrom(typeof(string)).Should().BeTrue();
            }

            [Fact]
            public void From_valid_iban_string_should_return_parsed_iban()
            {
                // Act
                object resultObj = _sut.ConvertFrom(TestValues.ValidIban);

                // Assert
                resultObj.Should()
                    .NotBeNull()
                    .And.BeOfType<Iban>()
                    .Which.ToString()
                    .Should()
                    .Be(TestValues.ValidIban);
            }

            [Fact]
            public void From_invalid_iban_string_should_throw()
            {
                // Act
                Action act = () => _sut.ConvertFrom(TestValues.InvalidIban);

                // Assert
                act.Should().Throw<NotSupportedException>();
            }

            [Fact]
            public void From_null_iban_string_should_return_null()
            {
                string nullValue = null;

                // Act
                // ReSharper disable once ExpressionIsAlwaysNull
                object resultObj = _sut.ConvertFrom(nullValue);

                // Assert
                resultObj.Should().BeNull();
            }

            [Fact]
            public void Given_type_descriptor_context_when_converting_from_string_it_should_request_validator()
            {
                var typeDescriptorContextMock = new Mock<ITypeDescriptorContext>();
                typeDescriptorContextMock
                    .Setup(m => m.GetService(It.Is<Type>(t => t == typeof(IIbanValidator))))
                    .Returns(new IbanValidator())
                    .Verifiable();

                // Act
                object resultObj = _sut.ConvertFrom(typeDescriptorContextMock.Object, CultureInfo.InvariantCulture, TestValues.ValidIban);

                // Assert
                typeDescriptorContextMock.Verify();
                resultObj.Should()
                    .NotBeNull()
                    .And.BeOfType<Iban>()
                    .Which.ToString()
                    .Should()
                    .Be(TestValues.ValidIban);
            }
        }

        public class When_converting_to_string : IbanTypeConverterTests
        {
            private readonly Iban _iban;

            public When_converting_to_string()
            {
                _iban = new IbanParser(IbanRegistry.Default).Parse(TestValues.ValidIban);
            }

            [Fact]
            public void Should_be_able()
            {
                _sut.CanConvertTo(typeof(string)).Should().BeTrue();
            }

            [Fact]
            public void To_string_should_return_flat_formatted_iban()
            {
                // Act
                object resultObj = _sut.ConvertTo(_iban, typeof(string));

                // Assert
                resultObj.Should()
                    .NotBeNull()
                    .And.BeOfType<string>()
                    .Which.Should()
                    .Be(TestValues.ValidIban);
            }
        }

        public class When_querying_for_converter_via_typeDescriptor : IbanTypeConverterTests
        {
            [Fact]
            public void Should_return_custom_typeConverter()
            {
                // Act
                TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(Iban));

                // Assert
                typeConverter.Should().BeOfType<IbanTypeConverter>();
            }
        }

        public class When_json_converting
        {
            [Fact]
            public void It_should_succeed()
            {
                Iban bankAccountNumber1 = new IbanParser(IbanRegistry.Default).Parse(TestValues.ValidIban);
                string json = JsonConvert.SerializeObject(bankAccountNumber1);

                json.Should().Be($"\"{TestValues.ValidIban}\"");

                Iban bankAccountNumber2 = JsonConvert.DeserializeObject<Iban>(json);
                bankAccountNumber1.Should().Be(bankAccountNumber2);
            }
        }
    }
}
