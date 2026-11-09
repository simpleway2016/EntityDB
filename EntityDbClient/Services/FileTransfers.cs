using System.Text;
using System.Text.Json;

namespace EntityDbClient.Services;

public static class FileTransfers
{
    public static async Task<byte[]> GenerateCodeAsync(ServerConnection connection, int databaseId, string kind, string filePath, string? nameSpace = null)
    {
        var page = kind switch { "model" => "DownloadDatabaseCode.aspx", "simple" => "DownLoadSimpleCodeHandler.aspx", "minimal" => "DownLoadVerySimpleCodeHandler.aspx", _ => throw new ArgumentException("kind 必须是 model、simple 或 minimal。") };
        return await connection.TransferAsync($"{page}?databaseid={databaseId}&filepath={Uri.EscapeDataString(filePath)}" +
            (nameSpace == null ? "" : "&namespace=" + Uri.EscapeDataString(nameSpace)), readResponse: ReadCodeAsync);
    }

    private static async Task<byte[]> ReadCodeAsync(Stream stream, CancellationToken cancellation, Action<string, double?> report)
    {
        async Task<int> ReadIntAsync()
        {
            var bytes = new byte[4]; await stream.ReadExactlyAsync(bytes, cancellation);
            return System.Buffers.Binary.BinaryPrimitives.ReadInt32LittleEndian(bytes);
        }
        async Task<string> ReadStringAsync()
        {
            uint length = 0;
            var one = new byte[1];
            for (int shift = 0; shift < 35; shift += 7)
            {
                await stream.ReadExactlyAsync(one, cancellation);
                if (shift == 28 && one[0] > 7) throw new InvalidDataException("代码包字符串长度无效。");
                length |= (uint)(one[0] & 127) << shift;
                if ((one[0] & 128) != 0) continue;
                if (length > 1024 * 1024) throw new InvalidDataException("代码包字符串过长。");
                var bytes = new byte[(int)length]; await stream.ReadExactlyAsync(bytes, cancellation);
                return Encoding.UTF8.GetString(bytes);
            }
            throw new InvalidDataException("代码包字符串长度无效。");
        }
        var start = await ReadStringAsync();
        if (start != "start") throw new InvalidDataException(start);
        int count = await ReadIntAsync();
        if (count < 0) throw new InvalidDataException("代码包文件数量无效。");
        using var output = new MemoryStream();
        int read = 0;
        report("正在生成代码，等待文件…", null);
        string fileName;
        while ((fileName = await ReadStringAsync()) != ":end")
        {
            if (read >= count) throw new InvalidDataException("代码包文件数量无效。");
            int length = await ReadIntAsync();
            if (length < 0) throw new InvalidDataException("代码包长度无效。");
            var buffer = new byte[Math.Min(length, 81920)];
            void Report(int received)
            {
                double percent = (read + (length == 0 ? 1 : (double)received / length)) / count * 100;
                report($"正在下载 {fileName} · 文件 {read + 1}/{count} · {received:N0}/{length:N0} 字节 · {percent:F1}%", percent);
            }
            Report(0);
            for (int remaining = length; remaining > 0;)
            {
                int size = Math.Min(remaining, buffer.Length);
                int received = await stream.ReadAsync(buffer.AsMemory(0, size), cancellation);
                if (received == 0) throw new EndOfStreamException();
                output.Write(buffer, 0, received); remaining -= received;
                Report(length - remaining);
            }
            if (++read > count) throw new InvalidDataException("代码包文件数量无效。");
        }
        if (read != count) throw new InvalidDataException("代码包不完整。");
        return output.ToArray();
    }

    public static Task<byte[]> ExportDataAsync(ServerConnection connection, int[] tableIds)
    {
        if (tableIds.Length == 0) throw new ArgumentException("请选择数据表。");
        return connection.TransferAsync("DownloadTableData.aspx?tableids=" + Uri.EscapeDataString(JsonSerializer.Serialize(tableIds)));
    }

    public static async Task ImportDataAsync(ServerConnection connection, int databaseId, byte[] backup, bool clearDataFirst)
    {
        using var reader = new BinaryReader(new MemoryStream(backup), Encoding.UTF8);
        var names = reader.ReadString();
        _ = JsonSerializer.Deserialize<string[]>(names) ?? throw new InvalidDataException("备份表名无效。");
        _ = JsonSerializer.Deserialize<int[]>(reader.ReadString()) ?? throw new InvalidDataException("备份行数无效。");
        using var buffer = new MemoryStream();
        using (var writer = new BinaryWriter(buffer, Encoding.UTF8, true))
        {
            writer.Write(names);
            while (true)
            {
                var table = reader.ReadString(); writer.Write(table);
                if (table == ":end") break;
                writer.Write(reader.ReadString());
            }
        }
        var response = await connection.TransferAsync($"ImportTableData.aspx?dbid={databaseId}&clearDataFirst={(clearDataFirst ? 1 : 0)}", buffer.ToArray());
        CheckResult(response);
    }
    public static async Task ImportDesignAsync(ServerConnection connection, int projectId, byte[] content) =>
        CheckResult(await connection.TransferAsync($"ImportCSFileHandler.aspx?projectid={projectId}", content));
    private static void CheckResult(byte[] response)
    {
        var text = Encoding.UTF8.GetString(response).Trim();
        if (text != "ok") throw new InvalidOperationException(string.IsNullOrWhiteSpace(text) ? "服务器未确认导入成功。" : text);
    }
}
