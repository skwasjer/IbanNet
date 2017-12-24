using System;
using System.Linq;
using Moq;
using NUnit.Framework;

namespace IbanNet.Tests
{
	[TestFixture]
	internal class IbanTests
	{
		private Mock<IIbanValidator> _ibanValidatorMock;

		[SetUp]
		public void SetUp()
		{
			_ibanValidatorMock = new Mock<IIbanValidator>();
			_ibanValidatorMock
				.Setup(m => m.Validate(It.IsAny<string>()))
				.Returns(IbanValidationResult.Valid);

			Iban.Validator = _ibanValidatorMock.Object;
		}

		[Test]
		public void When()
		{
			var defs = new IbanDefinitions();

			Console.WriteLine(string.Join("\r\n", defs.Values.Select(ibanDef => Iban.Parse(ibanDef.Example).ToString("F") + " | " + Iban.Parse(ibanDef.Example).ToString("S"))));
		}
	}
}
