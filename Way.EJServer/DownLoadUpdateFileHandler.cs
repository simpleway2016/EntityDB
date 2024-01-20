using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Way.EntityDB.Design.Services;
using Way.Lib.ScriptRemoting;

namespace Way.EJServer
{
    class DownLoadUpdateFileHandler : Way.Lib.ScriptRemoting.ICustomHttpHandler
    {
        public void Handle(string originalUrl, HttpConnectInformation connectInfo, ref bool handled)
        {
            if (originalUrl.Contains("/DownLoadUpdateFile.aspx") == false)
                return;
            handled = true;
            var filepath = connectInfo.Request.Query["name"];

            byte[] compressedData;

            var data = File.ReadAllBytes($"./updates/{filepath}");
            // First, compress the string using GZip
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionLevel.Optimal, true))
                {
                    gzipStream.Write(data);
                }
                compressedData = memoryStream.ToArray();
            }

            connectInfo.Response.ContentLength = compressedData.Length;

            connectInfo.Response.Write(compressedData);
            connectInfo.Response.End();
        }
    }
}
