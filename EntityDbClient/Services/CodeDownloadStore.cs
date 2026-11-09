using System.Text.Json;

namespace EntityDbClient.Services;

public sealed class CodeDownloadStore(string? directory = null)
{
    private static readonly object Sync = new();
    private string FilePath => Path.Combine(directory ?? Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EntityDbClient"), "code-downloads.json");

    private static string Key(string server, int databaseId, string kind, string? nameSpace) =>
        JsonSerializer.Serialize(new object[] { ServerConnection.NormalizeUrl(server), databaseId, kind, nameSpace ?? "" });

    private Dictionary<string, string> Load() => File.Exists(FilePath)
        ? JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(FilePath)) ?? [] : [];

    public string? Find(string server, int databaseId, string kind, string? nameSpace)
    {
        lock (Sync)
        {
            var paths = Load();
            return paths.GetValueOrDefault(Key(server, databaseId, kind, nameSpace))
                ?? paths.GetValueOrDefault(Key(server, databaseId, kind, null));
        }
    }

    public void Remember(string server, int databaseId, string kind, string? nameSpace, string path)
    {
        lock (Sync)
        {
            var paths = Load(); paths[Key(server, databaseId, kind, nameSpace)] = path;
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
            var temporary = FilePath + ".tmp";
            File.WriteAllText(temporary, JsonSerializer.Serialize(paths));
            File.Move(temporary, FilePath, true);
        }
    }

    public static string[] Namespaces(string? value) => (value ?? "").Replace('，', ',').Split(',')
        .Select(x => x.Trim()).Where(x => x.Length > 0 && x != "$not lower$").Distinct().ToArray();
}
