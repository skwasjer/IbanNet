#if NETCOREAPP3_1
using System;
using FluentAssertions;
using IbanNet.DependencyInjection.ServiceProvider.Fixtures;
using Microsoft.Extensions.Options;
using TestHelpers.Specs;
using Xunit;

namespace IbanNet.DependencyInjection.ServiceProvider.Specs
{
	public class ShouldValidateOptionsRegistrySpec : DiSpec
	{
		public ShouldValidateOptionsRegistrySpec() : base(new ServiceProviderDependencyInjectionFixture(true))
		{
		}

		protected override void Given()
		{
			Fixture.Configure(builder =>
			{
                // ReSharper disable once AssignNullToNotNullAttribute
                IIbanNetOptionsBuilder returnedBuilder = builder.Configure(options => options.Registry = null);

				returnedBuilder.Should().BeSameAs(builder);
			});
		}

		[Fact]
		public void Given_registry_is_null_when_getting_options_it_should_ensure_not_null_through_validation()
		{
			Func<IbanValidatorOptions> act = () => Subject.GetService<IOptions<IbanValidatorOptions>>()?.Value;

			// Assert
			act.Should()
				.Throw<OptionsValidationException>()
				.Which.Message.Should()
				.Be("The 'Registry' is required.");
		}
	}
}
#endif
