using FluentAssertions;
using IbanNet;
using TestHelpers.Fixtures;
using Xunit;

namespace TestHelpers.Specs
{
	public abstract class ShouldResolveSameInstanceSpec : DiSpec
	{
		protected ShouldResolveSameInstanceSpec(IDependencyInjectionFixture fixture) : base(fixture)
		{
		}

		protected override void Given()
		{
			Fixture.Configure(builder => {});
		}

		[Fact]
		public void When_resolving_default_validator_multiple_times_it_should_return_same_instance()
		{
			// Act
			IIbanValidator validator1 = Subject.GetRequiredService<IIbanValidator>();
			IIbanValidator validator2 = Subject.GetRequiredService<IIbanValidator>();

			// Assert
			validator1.Should().BeSameAs(validator2);
		}
	}
}
