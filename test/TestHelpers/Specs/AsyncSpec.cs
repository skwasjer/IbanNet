namespace TestHelpers.Specs;

public abstract class AsyncSpec<TSubject> : IAsyncLifetime
{
    Task IAsyncLifetime.InitializeAsync()
    {
        return InitializeAsync();
    }

    protected virtual async Task InitializeAsync()
    {
        await GivenAsync();
        Subject = await CreateSubjectAsync();
    }

    protected TSubject Subject { get; private set; }

    Task IAsyncLifetime.DisposeAsync()
    {
        return DisposeAsync();
    }

    protected virtual Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    protected abstract Task GivenAsync();

    protected abstract Task<TSubject> CreateSubjectAsync();
}