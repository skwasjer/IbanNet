using IbanNet;
using IbanNet.Registry;
using TestHelpers.Fixtures;

namespace TestHelpers.Specs
{
    /// <summary>
    /// It should resolve custom service if registered before registering IbanNet with extension.
    /// </summary>
    public abstract class ShouldResolveCustomService : DiSpec
    {
        protected ShouldResolveCustomService(IDependencyInjectionFixture fixture) : base(fixture)
        {
        }

        protected override void Given()
        {
            Fixture.Configure(_ => { });
        }

        [Theory]
        [InlineData(typeof(IIbanRegistry))]
        [InlineData(typeof(IIbanParser))]
        [InlineData(typeof(IIbanValidator))]
        [InlineData(typeof(IIbanGenerator))]
        public void When_resolving_registry_it_should_not_throw(Type serviceType)
        {
            Type expectedMockType = typeof(IMocked<>).MakeGenericType(serviceType);

            // Assert
            Func<object> act = () => Subject.GetService(serviceType);

            // Act
            act.Should()
                .NotThrow()
                .Which
                .Should()
                .BeAssignableTo(expectedMockType);
        }
    }
}
