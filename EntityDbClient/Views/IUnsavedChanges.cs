namespace EntityDbClient.Views;

public interface IUnsavedChanges
{
    bool HasUnsavedChanges { get; }
}
