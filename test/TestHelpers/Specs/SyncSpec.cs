using System.Threading.Tasks;

namespace TestHelpers.Specs
{
    public abstract class SyncSpec<TSubject> : AsyncSpec<TSubject>
    {
        protected sealed override Task InitializeAsync()
        {
            Initialize();
            return base.InitializeAsync();
        }

        protected virtual void Initialize()
        {
        }

        protected sealed override Task GivenAsync()
        {
            Given();
#if NET452
			var cts = new TaskCompletionSource<object>();
			cts.SetResult(null);
			return cts.Task;
#else
            return Task.CompletedTask;
#endif
        }

        protected sealed override Task<TSubject> CreateSubjectAsync()
        {
            return Task.FromResult(CreateSubject());
        }

        protected sealed override Task DisposeAsync()
        {
            Dispose();
            return base.DisposeAsync();
        }

        protected virtual void Dispose()
        {
        }

        protected abstract void Given();

        protected abstract TSubject CreateSubject();
    }
}
