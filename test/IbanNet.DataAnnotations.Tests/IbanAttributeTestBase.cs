using System;
using System.ComponentModel.DataAnnotations;
using Moq;

namespace IbanNet.DataAnnotations
{
	internal abstract class IbanAttributeTestBase<TAttribute>
		: IbanTestFixture
		where TAttribute : Attribute
	{
		protected Mock<IServiceProvider> ServiceProviderMock;
		protected ValidationContext ValidationContext;
		protected TAttribute Sut;

		public override void SetUp()
		{
			base.SetUp();

			ServiceProviderMock = new Mock<IServiceProvider>();
			ServiceProviderMock
				.Setup(m => m.GetService(typeof(IIbanValidator)))
				.Returns(IbanValidatorMock.Object)
				.Verifiable();

			ValidationContext = new ValidationContext(new object(), ServiceProviderMock.Object, null);

			Sut = CreateSubject();
		}

		protected abstract TAttribute CreateSubject();
	}
}