using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using NUnit.Framework;

namespace IbanNet
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
				var resultObj = _sut.ConvertFrom(TestValues.ValidIban);

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
				act.ShouldThrow<NotSupportedException>();
			}

			[Test]
			public void From_null_iban_string_should_return_null()
			{
				string nullValue = null;

				// Act
				// ReSharper disable once ExpressionIsAlwaysNull
				var resultObj = _sut.ConvertFrom(nullValue);

				// Assert
				resultObj.Should().BeNull();
			}
		}

		public class When_converting_to_string : IbanTypeConverterTests
		{
			private Iban _iban;

			public override void SetUp()
			{
				base.SetUp();

				_iban = Iban.Parse(TestValues.ValidIban);
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
				var resultObj = _sut.ConvertTo(_iban, typeof(string));

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
				var typeConverter = TypeDescriptor.GetConverter(typeof(Iban));

				// Assert
				typeConverter.Should().BeOfType<IbanTypeConverter>();
			}
		}
	}
}