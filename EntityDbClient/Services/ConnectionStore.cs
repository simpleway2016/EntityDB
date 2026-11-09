using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EntityDbClient.Services;

public sealed class SavedConnection
{
    public string Url { get; set; } = "";
    public string UserName { get; set; } = "";
    public string ProtectedPassword { get; set; } = "";
    public DateTimeOffset LastConnected { get; set; }
    [JsonIgnore] public string DisplayName => $"{Url}  ·  {UserName}";
    public override string ToString() => DisplayName;
}

public sealed class ConnectionStore
{
    public string DirectoryPath { get; }
    private string FilePath => Path.Combine(DirectoryPath, "connections.json");
    public ConnectionStore(string? directory = null) => DirectoryPath = directory ?? Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EntityDbClient");

    public List<SavedConnection> Load()
    {
        if (!File.Exists(FilePath)) return [];
        return (JsonSerializer.Deserialize<List<SavedConnection>>(File.ReadAllText(FilePath)) ?? [])
            .OrderByDescending(x => x.LastConnected).ToList();
    }

    public string GetPassword(SavedConnection connection) => Encoding.UTF8.GetString(Unprotect(Convert.FromBase64String(connection.ProtectedPassword)));

    public void Remember(string url, string userName, string password)
    {
        var entries = Load();
        url = ServerConnection.NormalizeUrl(url);
        entries.RemoveAll(x => x.Url == url && x.UserName == userName);
        entries.Insert(0, new SavedConnection { Url = url, UserName = userName,
            ProtectedPassword = Convert.ToBase64String(Protect(Encoding.UTF8.GetBytes(password))), LastConnected = DateTimeOffset.UtcNow });
        Save(entries);
    }

    public void Forget(SavedConnection connection)
    {
        var entries = Load();
        entries.RemoveAll(x => x.Url == connection.Url && x.UserName == connection.UserName);
        Save(entries);
    }

    private void Save(List<SavedConnection> entries)
    {
        Directory.CreateDirectory(DirectoryPath);
        var temporary = FilePath + ".tmp";
        File.WriteAllText(temporary, JsonSerializer.Serialize(entries, new JsonSerializerOptions { WriteIndented = true }));
        File.Move(temporary, FilePath, true);
        if (!OperatingSystem.IsWindows()) File.SetUnixFileMode(FilePath, UnixFileMode.UserRead | UnixFileMode.UserWrite);
    }

    private byte[] Protect(byte[] bytes) => OperatingSystem.IsWindows() ? WindowsProtect(bytes, false) : AesProtect(bytes, false);
    private byte[] Unprotect(byte[] bytes) => OperatingSystem.IsWindows() ? WindowsProtect(bytes, true) : AesProtect(bytes, true);

    private byte[] AesProtect(byte[] bytes, bool decrypt)
    {
        if (OperatingSystem.IsWindows()) throw new PlatformNotSupportedException();
        Directory.CreateDirectory(DirectoryPath);
        var path = Path.Combine(DirectoryPath, "credentials.key");
        if (!File.Exists(path))
        {
            if (decrypt) throw new CryptographicException("找不到凭据密钥，请重新输入密码。");
            using var file = new FileStream(path, new FileStreamOptions { Mode = FileMode.CreateNew,
                Access = FileAccess.Write, UnixCreateMode = UnixFileMode.UserRead | UnixFileMode.UserWrite });
            file.Write(RandomNumberGenerator.GetBytes(32));
        }
        using var aes = new AesGcm(File.ReadAllBytes(path), 16);
        if (decrypt)
        {
            if (bytes.Length < 28) throw new CryptographicException("保存的密码已损坏。");
            var clear = new byte[bytes.Length - 28];
            aes.Decrypt(bytes.AsSpan(0, 12), bytes.AsSpan(28), bytes.AsSpan(12, 16), clear);
            return clear;
        }
        var result = new byte[bytes.Length + 28];
        RandomNumberGenerator.Fill(result.AsSpan(0, 12));
        aes.Encrypt(result.AsSpan(0, 12), bytes, result.AsSpan(28), result.AsSpan(12, 16));
        return result;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct Blob { public int Length; public IntPtr Data; }
    [DllImport("crypt32.dll", SetLastError = true)]
    private static extern bool CryptProtectData(ref Blob input, IntPtr description, IntPtr entropy, IntPtr reserved, IntPtr prompt, int flags, out Blob output);
    [DllImport("crypt32.dll", SetLastError = true)]
    private static extern bool CryptUnprotectData(ref Blob input, IntPtr description, IntPtr entropy, IntPtr reserved, IntPtr prompt, int flags, out Blob output);
    [DllImport("kernel32.dll")] private static extern IntPtr LocalFree(IntPtr memory);
    private static byte[] WindowsProtect(byte[] bytes, bool decrypt)
    {
        var input = new Blob { Length = bytes.Length, Data = Marshal.AllocHGlobal(bytes.Length) };
        try
        {
            Marshal.Copy(bytes, 0, input.Data, bytes.Length);
            Blob output;
            var ok = decrypt ? CryptUnprotectData(ref input, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, 1, out output)
                : CryptProtectData(ref input, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, 1, out output);
            if (!ok) throw new CryptographicException(Marshal.GetLastWin32Error());
            try { var result = new byte[output.Length]; Marshal.Copy(output.Data, result, 0, result.Length); return result; }
            finally { LocalFree(output.Data); }
        }
        finally { Marshal.FreeHGlobal(input.Data); }
    }
}
