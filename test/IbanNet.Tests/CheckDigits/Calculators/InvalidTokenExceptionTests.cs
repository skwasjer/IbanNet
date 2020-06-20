using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using FluentAssertions;
using IbanNet.Registry;
using IbanNet.Validation.Results;
using TestHelpers.Specs;
using Xunit;

namespace IbanNet.CheckDigits.Calculators
{
	public class InvalidTokenExceptionTests : BaseExceptionTests<InvalidTokenException>
	{
#if NETFRAMEWORK || NETCOREAPP3_0 || NETCOREAPP3_1
		[Fact]
		public void Given_validation_result_it_should_serialize_and_deserialize_and_ignore_result()
		{
			var exception = new InvalidTokenException(23, 'c');

			using var ms = new MemoryStream();
			var formatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.CrossMachine));

			// Act
			formatter.Serialize(ms, exception);
			ms.Position = 0;
			var actual = formatter.Deserialize(ms) as Exception;

			// Assert
			actual.Should()
				.BeOfType<InvalidTokenException>()
				.Which
				.Should()
				.BeEquivalentTo(exception);
		}
#endif
	}
}
