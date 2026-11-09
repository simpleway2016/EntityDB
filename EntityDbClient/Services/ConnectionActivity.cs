namespace EntityDbClient.Services;

public sealed record ConnectionActivity(long Version, int ActiveRequests, string Description, double? Progress = null)
{
    public bool IsBusy => ActiveRequests > 0;
}

internal sealed class ActivityScope(Action end, Action<string, double?>? report = null) : IDisposable
{
    private Action? end = end;
    public void Report(string description, double? progress) => report?.Invoke(description, progress);
    public void Dispose() => Interlocked.Exchange(ref end, null)?.Invoke();
}
