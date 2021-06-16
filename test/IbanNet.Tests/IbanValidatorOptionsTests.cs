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
        public void Rules_should_default_to_empty_list()
        {
            _sut.Rules.Should().BeEmpty();
        }
    }
}
