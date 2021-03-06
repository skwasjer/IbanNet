﻿using System.Threading.Tasks;
using FluentAssertions;
using TestHelpers.Specs;
using Xunit;

namespace IbanNet.Registry
{
    public abstract class BaseRegistryProviderSpec<T> : AsyncSpec<T>
        where T : IIbanRegistryProvider
    {
        private readonly int _expectedCount;

        protected BaseRegistryProviderSpec(int expectedCount)
        {
            _expectedCount = expectedCount;
        }

        protected override Task GivenAsync()
        {
#if NET452
			var cts = new TaskCompletionSource<object>();
			cts.SetResult(null);
			return cts.Task;
#else
            return Task.CompletedTask;
#endif
        }

        [Fact]
        public void When_creating_registry_provider_it_should_have_n_items()
        {
            Subject.Should().NotBeEmpty();
            Subject.Count.Should().Be(_expectedCount);
        }
    }
}
