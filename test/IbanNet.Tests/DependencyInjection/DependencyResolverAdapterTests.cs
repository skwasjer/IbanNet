using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using IbanNet.FluentAssertions;
using IbanNet.TestCases;
using Moq;
using NUnit.Framework;

namespace IbanNet.DependencyInjection
{
	public class DependencyResolverAdapterTests
	{
		private class TestService
		{
		}

		private DependencyResolverAdapter _sut;
		private Mock<DependencyResolverAdapter> _adapterStub;

		[SetUp]
		public void SetUp()
		{
			_sut = CreateAdapterStub();
			_adapterStub = Mock.Get(_sut);
		}

		private static DependencyResolverAdapter CreateAdapterStub()
		{
			var adapterStub = new Mock<DependencyResolverAdapter>
			{
				CallBase = true
			};
			adapterStub
				.Setup(m => m.GetService(It.IsAny<Type>()))
				.Returns<Type>(Activator.CreateInstance);
			return adapterStub.Object;
		}

		[Test]
		public void Given_service_is_not_registered_when_getting_generic_required_it_should_throw()
		{
			_adapterStub
				.Setup(m => m.GetService(It.IsAny<Type>()))
				.Returns(null)
				.Verifiable();

			// Act
			Action act = () => _sut.GetRequiredService<TestService>();

			// Assert
			act.Should().Throw<InvalidOperationException>();
			_adapterStub.Verify();
		}

		[Test]
		public void Given_service_is_not_registered_when_getting_required_it_should_throw()
		{
			_adapterStub
				.Setup(m => m.GetService(It.IsAny<Type>()))
				.Returns(null)
				.Verifiable();

			// Act
			Action act = () => _sut.GetRequiredService(typeof(TestService));

			// Assert
			act.Should().Throw<InvalidOperationException>();
			_adapterStub.Verify();
		}

		[TestCaseSource(nameof(ResolvesSuccessfullyTestCases))]
		public void Given_service_is_registered_when_resolving_it_should_return_service(params object[] args)
		{
			Delegate act = (Delegate)args[0];
			act.Should().NotThrow(args.Skip(1).ToArray()).Subject.Should().BeOfType<TestService>();
		}

		public static IEnumerable<object[]> ResolvesSuccessfullyTestCases()
		{
			DependencyResolverAdapter instance = CreateAdapterStub();
			yield return DelegateTestCase.Create(instance.GetService, typeof(TestService)).WithoutName();
			yield return DelegateTestCase.Create(instance.GetRequiredService, typeof(TestService)).WithoutName();
			yield return DelegateTestCase.Create(instance.GetService<TestService>).WithoutName();
			yield return DelegateTestCase.Create(instance.GetRequiredService<TestService>).WithoutName();
		}
	}
}
