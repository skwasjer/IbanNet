using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using IbanNet.DependencyInjection.FluentAssertions;
using IbanNet.FakeRules;
using IbanNet.Registry;
using IbanNet.TestCases;
using IbanNet.Validation.Methods;
using Moq;
using NUnit.Framework;

namespace IbanNet.DependencyInjection
{
	public class IbanNetOptionsBuilderTests
	{
		private Mock<IIbanNetOptionsBuilder> _builderStub;
		private IIbanNetOptionsBuilder _builder;

		protected IbanNetOptionsBuilderTests()
		{
		}

		[SetUp]
		public void SetUp()
		{
			_builder = GetBuilderStub();
			_builderStub = Mock.Get(_builder);
		}

		private static IIbanNetOptionsBuilder GetBuilderStub()
		{
			var builderMock = new Mock<IIbanNetOptionsBuilder>();
			builderMock
				.Setup(m => m.Configure(It.IsAny<Action<DependencyResolverAdapter, IbanValidatorOptions>>()))
				.Returns(builderMock.Object);

			return builderMock.Object;
		}

		public class ExtensionTests : IbanNetOptionsBuilderTests
		{
			[Test]
			public void Given_registry_is_configured_it_should_set_registry()
			{
				IEnumerable<IbanCountry> limitedCountries = IbanRegistry.Default
					.Where((country, i) => i % 2 == 0)
					.ToList();

				// Act
				IIbanNetOptionsBuilder returnedBuilder = _builder.UseRegistry(new IbanRegistry
				{
					Providers =
					{
						new IbanRegistryListProvider(limitedCountries)
					}
				});

				// Assert
				_builderStub.Should().HaveConfiguredRegistry(limitedCountries);
				returnedBuilder.Should().BeSameAs(_builderStub.Object);
			}

			[Test]
			public void Given_strict_validation_is_configured_it_should_set_validation_method()
			{
				// Act
				IIbanNetOptionsBuilder returnedBuilder = _builder.UseStrictValidation();

				// Assert
				_builderStub.Should().HaveConfiguredValidationMethod<StrictValidation>();
				returnedBuilder.Should().BeSameAs(_builderStub.Object);
			}

			[Test]
			public void Given_loose_validation_is_configured_it_should_set_validation_method()
			{
				// Act
				IIbanNetOptionsBuilder returnedBuilder = _builder.UseLooseValidation();

				// Assert
				_builderStub.Should().HaveConfiguredValidationMethod<LooseValidation>();
				returnedBuilder.Should().BeSameAs(_builderStub.Object);
			}

			[Test]
			public void Given_rule_is_configured_via_factory_it_should_add_instance_to_rule_collection()
			{
				var configuredRule = new TestValidationRule();

				// Act
				IIbanNetOptionsBuilder returnedBuilder = _builder.WithRule(() => configuredRule);

				// Assert
				_builderStub.Should()
					.HaveConfiguredRule<TestValidationRule>()
					.And.Contain(configuredRule);
				returnedBuilder.Should().BeSameAs(_builderStub.Object);
			}
		}

		public class NullArgumentTests : IbanNetOptionsBuilderTests
		{
			[TestCaseSource(nameof(NullArgumentTestCases))]
			public void Given_null_instance_when_calling_method_it_should_throw(params object[] args)
			{
				NullArgumentTest.Execute(args);
			}

			public static IEnumerable<object[]> NullArgumentTestCases()
			{
				IIbanNetOptionsBuilder instance = GetBuilderStub();

				return new NullArgumentTestCases
				{
					DelegateTestCase.Create<IIbanNetOptionsBuilder, Action<IbanValidatorOptions>, IIbanNetOptionsBuilder>(
						IbanNetOptionsBuilderExtensions.Configure,
						instance,
						_ => { }),
					DelegateTestCase.Create(
						IbanNetOptionsBuilderExtensions.WithRule,
						instance,
						typeof(TestValidationRule)),
					DelegateTestCase.Create(
						IbanNetOptionsBuilderExtensions.UseRegistry,
						instance,
						Mock.Of<IIbanRegistry>()),
					DelegateTestCase.Create(
						IbanNetOptionsBuilderExtensions.WithRule<TestValidationRule>,
						instance),
					DelegateTestCase.Create(
						IbanNetOptionsBuilderExtensions.UseStrictValidation,
						instance),
					DelegateTestCase.Create(
						IbanNetOptionsBuilderExtensions.UseLooseValidation,
						instance),
					DelegateTestCase.Create<IIbanNetOptionsBuilder, Func<TestValidationRule>, IIbanNetOptionsBuilder>(
						IbanNetOptionsBuilderExtensions.WithRule,
						instance,
						() => new TestValidationRule())
				}.Flatten();
			}
		}
	}
}
