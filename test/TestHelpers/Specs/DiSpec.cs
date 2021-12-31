using IbanNet.DependencyInjection;
using TestHelpers.Fixtures;

namespace TestHelpers.Specs
{
    [Collection(nameof(SetsStaticValidator))]
    public abstract class DiSpec : SyncSpec<DependencyResolverAdapter>
    {
        protected DiSpec(IDependencyInjectionFixture fixture)
        {
            Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        }

        protected IDependencyInjectionFixture Fixture { get; }

        protected override DependencyResolverAdapter CreateSubject()
        {
            return Fixture.Build();
        }
    }
}
