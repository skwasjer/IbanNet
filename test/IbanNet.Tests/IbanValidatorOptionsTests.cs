using System;
using FluentAssertions;
using IbanNet.Registry;
using Xunit;

namespace IbanNet
{
	public class IbanValidatorOptionsTests
	{
		private readonly IbanValidatorOptions _sut;

		public IbanValidatorOptionsTests()
		{
			_sut = new IbanValidatorOptions();
		}

		[Fact]
		public void Registry_should_default_to_default_registry()
		{
			_sut.Registry.Should().BeSameAs(IbanRegistry.Default);
		}

		[Fact]
		public void Validation_method_should_default_to_strict()
		{
			_sut.Method.Should().Be(ValidationMethod.Strict);
		}

		[Fact]
		public void Rules_should_default_to_empty_list()
		{
			_sut.Rules.Should().BeEmpty();
		}

		[Fact]
		public void When_setting_invalid_validation_method_it_should_throw()
		{
			Action act = () => _sut.Method = (ValidationMethod)int.MaxValue;

			act.Should().Throw<ArgumentException>();
		}
	}
}
