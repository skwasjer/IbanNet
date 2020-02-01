using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace IbanNet.TypeConverters
{
	[TestFixture]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	internal class IbanTypeConverterTests
	{
		private IbanTypeConverter _sut;

		[SetUp]
		public virtual void SetUp()
		{
			_sut = new IbanTypeConverter();
		}

		public class When_converting_from_string : IbanTypeConverterTests
		{
			[Test]
			public void Should_be_able()
			{
				_sut.CanConvertFrom(typeof(string)).Should().BeTrue();
			}

			[Test]
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

			[Test]
			public void From_invalid_iban_string_should_throw()
			{
				// Act
				Action act = () => _sut.ConvertFrom(TestValues.InvalidIban);

				// Assert
				act.Should().Throw<NotSupportedException>();
			}

			[Test]
			public void From_null_iban_string_should_return_null()
			{
				string nullValue = null;

				// Act
				// ReSharper disable once ExpressionIsAlwaysNull
				object resultObj = _sut.ConvertFrom(nullValue);

				// Assert
				resultObj.Should().BeNull();
			}

			[Test]
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
			private Iban _iban;

			public override void SetUp()
			{
				base.SetUp();

				_iban = new Iban(TestValues.ValidIban);
			}

			[Test]
			public void Should_be_able()
			{
				_sut.CanConvertTo(typeof(string)).Should().BeTrue();
			}

			[Test]
			public void To_string_should_return_flat_formatted_iban()
			{
				// Act
				object resultObj = _sut.ConvertTo(_iban, typeof(string));

				// Assert
				resultObj.Should()
					.NotBeNull()
					.And.BeOfType<string>()
					.Which.Should().Be(TestValues.ValidIban);
			}
		}

		public class When_quering_for_converter_via_typeDescriptor : IbanTypeConverterTests
		{
			[Test]
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
			[Test]
			public void It_should_succeed()
			{
				var bankAccountNumber1 = new Iban(TestValues.ValidIban);
				string json = JsonConvert.SerializeObject(bankAccountNumber1);

				json.Should().Be($"\"{TestValues.ValidIban}\"");

				Iban bankAccountNumber2 = JsonConvert.DeserializeObject<Iban>(json);
				bankAccountNumber1.Should().Be(bankAccountNumber2);
			}
		}
	}
}
