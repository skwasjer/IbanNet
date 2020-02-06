using System;
using FluentAssertions;
using IbanNet.Validation;
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
			Action act = () => new IbanStructure(structure, new NullStructureValidationFactory());

			// Assert
			act.Should()
				.Throw<ArgumentNullException>()
				.Which.ParamName.Should()
				.Be(nameof(structure));
		}

		[Test]
		public void When_creating_with_null_structure_validation_factory_it_should_throw()
		{
			IStructureValidationFactory structureValidationFactory = null;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			// ReSharper disable once ObjectCreationAsStatement
			Action act = () => new IbanStructure(string.Empty, structureValidationFactory);

			// Assert
			act.Should()
				.Throw<ArgumentNullException>()
				.Which.ParamName.Should()
				.Be(nameof(structureValidationFactory));
		}
	}
}
