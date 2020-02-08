using System;
using IbanNet.DependencyInjection;
using TestHelpers.Fixtures;

namespace TestHelpers.Specs
{
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
