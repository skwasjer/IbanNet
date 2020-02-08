using System;
using FluentAssertions;
using IbanNet.Validation.Results;
using IbanNet.Validation.Rules;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IbanNet.DependencyInjection.ServiceProvider
{
	public class ServiceProviderDependencyResolverAdapterTests
	{
		private class Dependency
		{
		}

		private class TestRuleWithDefaultCtor : IIbanValidationRule
		{
			public ValidationRuleResult Validate(ValidationRuleContext context)
			{
				throw new NotImplementedException();
			}
		}

		private class TestRuleWithDependency : IIbanValidationRule
		{
			// ReSharper disable once UnusedParameter.Local - justification: intentional.
#pragma warning disable IDE0060 // Remove unused parameter
			public TestRuleWithDependency(Dependency dependency)
#pragma warning restore IDE0060 // Remove unused parameter
			{
			}

			public ValidationRuleResult Validate(ValidationRuleContext context)
			{
				throw new NotImplementedException();
			}
		}

		[Fact]
		public void Given_service_is_not_registered_when_getting_service_it_should_return_null()
		{
			var serviceCollection = new ServiceCollection();
			var sut = new ServiceProviderDependencyResolverAdapter(serviceCollection.BuildServiceProvider());

			// Act
			Func<object> act = () => sut.GetService(typeof(object));

			// Assert
			act.Should().NotThrow().Which.Should().BeNull();
		}

		[Fact]
		public void Given_rule_type_with_default_ctor_is_not_registered_when_getting_service_it_should_return_instance()
		{
			var serviceCollection = new ServiceCollection();
			var sut = new ServiceProviderDependencyResolverAdapter(serviceCollection.BuildServiceProvider());

			// Act
			Func<object> act = () => sut.GetService(typeof(TestRuleWithDefaultCtor));

			// Assert
			act.Should().NotThrow().Which.Should().BeOfType<TestRuleWithDefaultCtor>();
		}

		[Fact]
		public void Given_rule_type_with_dependency_are_both_not_registered_when_getting_service_it_should_throw()
		{
			var serviceCollection = new ServiceCollection();
			var sut = new ServiceProviderDependencyResolverAdapter(serviceCollection.BuildServiceProvider());

			// Act
			Func<object> act = () => sut.GetService(typeof(TestRuleWithDependency));

			// Assert
			act.Should().Throw<InvalidOperationException>();
		}

		[Fact]
		public void Given_rule_type_with_dependency_and_dependency_is_registered_when_getting_service_it_should_not_throw()
		{
			IServiceCollection serviceCollection = new ServiceCollection()
				.AddTransient<Dependency>();
			var sut = new ServiceProviderDependencyResolverAdapter(serviceCollection.BuildServiceProvider());

			// Act
			Func<object> act = () => sut.GetService(typeof(TestRuleWithDependency));

			// Assert
			act.Should().NotThrow().Which.Should().BeOfType<TestRuleWithDependency>();
		}

		[Fact]
		public void Given_rule_type_with_dependency_are_registered_when_getting_service_it_should_not_throw()
		{
			IServiceCollection serviceCollection = new ServiceCollection()
				.AddTransient<TestRuleWithDependency>()
				.AddTransient<Dependency>();
			var sut = new ServiceProviderDependencyResolverAdapter(serviceCollection.BuildServiceProvider());

			// Act
			Func<object> act = () => sut.GetService(typeof(TestRuleWithDependency));

			// Assert
			act.Should().NotThrow().Which.Should().BeOfType<TestRuleWithDependency>();
		}

		[Fact]
		public void Given_null_argument_when_creating_instance_it_should_throw()
		{
			IServiceProvider serviceProvider = null;

			// ReSharper disable once ExpressionIsAlwaysNull
			// ReSharper disable once ObjectCreationAsStatement
			Action act = () => new ServiceProviderDependencyResolverAdapter(serviceProvider);

			act.Should().Throw<ArgumentNullException>()
				.Which.ParamName.Should().Be(nameof(serviceProvider));
		}
	}
}
