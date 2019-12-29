using System;
using FluentAssertions;
using NUnit.Framework;

namespace IbanNet.Registry
{
	[TestFixture]
	public class IbanStructureTests
	{
		[Test]
		public void When_creating_with_null_structure_it_should_throw()
		{
			string structure = null;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			// ReSharper disable once ObjectCreationAsStatement
			Action act = () => new IbanStructure(structure);

			// Assert
			act.Should()
				.Throw<ArgumentNullException>()
				.Which.ParamName.Should()
				.Be(nameof(structure));
		}
	}
}
