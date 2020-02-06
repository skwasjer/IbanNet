using System;
using FluentAssertions;
using IbanNet.Validation;
using NUnit.Framework;

namespace IbanNet.Registry
{
	[TestFixture]
	public class StructureTests
	{
		private class TestStructureSection : StructureSection
		{
			internal TestStructureSection()
			{
			}

			public TestStructureSection(string structure) : base(structure, new NullStructureValidationFactory())
			{
			}
		}

		private StructureSection _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new TestStructureSection();
		}

		[Test]
		public void When_creating_structureSection_it_should_initialize_properties()
		{
			// Act
			StructureSection structure = new TestStructureSection();

			// Assert
			structure.Structure.Should().BeEmpty();
			structure.Example.Should().BeEmpty();
			structure.Length.Should().Be(0);
			structure.Position.Should().Be(0);
		}

		[Test]
		public void When_creating_structureSection_with_structure_it_should_set_property()
		{
			const string myStructure = nameof(myStructure);

			// Act
			StructureSection structure = new TestStructureSection(myStructure);

			// Assert
			structure.Structure.Should().Be(myStructure);
		}

		[Test]
		public void When_creating_structureSection_with_null_structure_it_should_throw()
		{
			string structure = null;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			// ReSharper disable once ObjectCreationAsStatement
			Action act = () => new TestStructureSection(structure);

			// Assert
			act.Should()
				.Throw<ArgumentNullException>()
				.Which.ParamName.Should()
				.Be(nameof(structure));
		}

		[Test]
		public void When_setting_structure_to_null_it_should_throw()
		{
			string value = null;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			Action act = () => _sut.Structure = value;

			// Assert
			act.Should()
				.Throw<ArgumentNullException>()
				.Which.ParamName.Should()
				.Be(nameof(value));
		}

		[Test]
		public void When_creating_with_null_structure_validation_factory_it_should_throw()
		{
			IStructureValidationFactory value = null;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			Action act = () => _sut.ValidationFactory = value;

			// Assert
			act.Should()
				.Throw<ArgumentNullException>()
				.Which.ParamName.Should()
				.Be(nameof(value));
		}

		[Test]
		public void When_setting_example_to_null_it_should_set_to_empty_string()
		{
			// Act
			_sut.Example = null;

			// Assert
			_sut.Example.Should().BeEmpty();
		}

		[Test]
		public void When_setting_position_to_negative_value_it_should_throw()
		{
			const int value = -1;

			// Act
			Action act = () => _sut.Position = value;

			// Assert
			act.Should()
				.Throw<ArgumentOutOfRangeException>()
				.Which.ParamName.Should()
				.Be(nameof(value));
		}

		[Test]
		public void When_setting_length_to_negative_value_it_should_throw()
		{
			const int value = -1;

			// Act
			Action act = () => _sut.Length = value;

			// Assert
			act.Should()
				.Throw<ArgumentOutOfRangeException>()
				.Which.ParamName.Should()
				.Be(nameof(value));
		}
	}
}
