using System;
using FluentAssertions;
using IbanNet.Validation;
using Xunit;

namespace IbanNet.Registry
{
    public class BranchStructureTests
    {
        [Fact]
        public void When_creating_with_null_structure_it_should_throw()
        {
            string structure = null;

            // Act
            // ReSharper disable once ObjectCreationAsStatement
            // ReSharper disable once AssignNullToNotNullAttribute
            Action act = () => new BranchStructure(structure, new NullStructureValidationFactory());

            // Assert
            act.Should()
                .Throw<ArgumentNullException>()
                .Which.ParamName.Should()
                .Be(nameof(structure));
        }

        [Fact]
        public void When_creating_with_null_structure_validation_factory_it_should_throw()
        {
            IStructureValidationFactory structureValidationFactory = null;

            // Act
            // ReSharper disable once ObjectCreationAsStatement
            // ReSharper disable once AssignNullToNotNullAttribute
            Action act = () => new BranchStructure(string.Empty, structureValidationFactory);

            // Assert
            act.Should()
                .Throw<ArgumentNullException>()
                .Which.ParamName.Should()
                .Be(nameof(structureValidationFactory));
        }
    }
}
