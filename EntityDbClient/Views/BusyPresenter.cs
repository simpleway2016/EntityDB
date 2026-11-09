using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Threading;
using EntityDbClient.Services;

namespace EntityDbClient.Views;

// Observe the actual transport, so UI buttons, lazy loading, file transfers and Agent calls all report progress.
public sealed class BusyPresenter : UserControl, IDisposable
{
    private readonly ServerConnection connection;
    private readonly Control body;
    private readonly Border overlay;
    private readonly TextBlock description = Ui.Label("");
    private readonly ProgressBar progress = new() { IsIndeterminate = true, Height = 4, Width = 200, Margin = new Thickness(0, 0, 0, 14) };
    private long version = -1;
    private bool disposed;
    public bool IsBusy => overlay.IsVisible;
    public string Description => description.Text ?? "";

    public BusyPresenter(Control body, ServerConnection connection)
    {
        this.body = body; this.connection = connection;
        var root = new Grid(); root.Children.Add(body);
        overlay = new Border { Name = "NetworkLoadingOverlay", Background = Brush.Parse("#CFFFFFFF"), IsVisible = false, Child =
            new Border { Background = Brushes.White, BorderBrush = Ui.Line, BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(6),
                Padding = new Thickness(28, 24), HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center,
                Child = new StackPanel { Children = { progress, description } } } };
        root.Children.Add(overlay); Content = root;
        connection.ActivityChanged += OnActivityChanged;
        Apply(connection.Activity);
    }
    private void OnActivityChanged(ConnectionActivity activity)
    {
        if (Dispatcher.UIThread.CheckAccess()) Apply(activity);
        else Dispatcher.UIThread.Post(() => Apply(activity));
    }
    private void Apply(ConnectionActivity activity)
    {
        if (disposed || activity.Version < version) return;
        version = activity.Version; overlay.IsVisible = activity.IsBusy;
        progress.IsIndeterminate = activity.IsBusy && activity.Progress == null;
        progress.Value = activity.Progress ?? 0;
        body.IsEnabled = !activity.IsBusy; description.Text = activity.Description;
    }
    public void Dispose() { disposed = true; connection.ActivityChanged -= OnActivityChanged; }
}
