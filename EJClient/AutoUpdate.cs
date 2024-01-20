using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace EJClient
{
    class AutoUpdate
    {
        class FileInfo
        {
            public string SavePath;
            public string FileName;
            public long LastWriteTime;
            public string MD5;
        }
        public AutoUpdate()
        {

            new Thread(check) { IsBackground = true}.Start();

        }

        async void check()
        {
            try
            {
                while (Helper.Client == null)
                    Thread.Sleep(1000);
                bool hasUpdated = false;

                var fileinfos = Helper.Client.InvokeSync<FileInfo[]>("GetUpdateFileListV2" , FileVersionInfo.GetVersionInfo("./EJClient.exe").FileVersion);

                foreach (var item in fileinfos)
                {
                    if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + item.SavePath) == false)
                    {
                        await downloadFile(item.SavePath);
                        hasUpdated = true;
                    }
                    else
                    {
                        if ((AppDomain.CurrentDomain.BaseDirectory + item.SavePath).FileMD5() != item.MD5)
                        {
                            await downloadFile(item.SavePath);
                            hasUpdated = true;
                        }
                    }
                }

                if (hasUpdated)
                {
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        if (Application.Current.MainWindow != null)
                        {
                            if (MessageBox.Show(Application.Current.MainWindow, "发现新版本，是否现在更新？", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            {
                                var json = (new Copy.DstSource
                                {
                                    dst = AppDomain.CurrentDomain.BaseDirectory ,
                                    src = AppDomain.CurrentDomain.BaseDirectory + "updates",
                                }).ToJsonString();
                                System.Diagnostics.Process.Start("copy.exe",Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes( json)));
                            }
                        }
                    });
                    
                }
            }
            catch
            {
                try
                {
                    var files = System.IO.Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "updates");
                    foreach( var f in files )
                    System.IO.File.Delete( f );
                }
                catch(Exception ex)
                {
                }
            }
        }

        System.Net.Http.HttpClient _httpClient = new System.Net.Http.HttpClient();
        async Task downloadFile(string savepath)
        {
            //DownLoadUpdateFile.aspx?name=ejclient.exe
            var filecontent = await _httpClient.GetByteArrayAsync($"{Helper.Client.BaseUrl}/DownLoadUpdateFile.aspx?name={savepath}");
            filecontent = GZipDecompressToByteArray(filecontent);
            if (System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "updates") == false)
                System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "updates");
            if (savepath.ToLower() == "copy.exe")
            {
                System.IO.File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + savepath, filecontent);
            }
            else
            System.IO.File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "updates" + "\\" + savepath, filecontent);
        }

        public static byte[] GZipDecompressToByteArray(byte[] gzipData)
        {
            // Create a GZipStream to decompress the data
            using (MemoryStream memStream = new MemoryStream(gzipData))
            using (GZipStream gzipStream = new GZipStream(memStream, CompressionMode.Decompress))
            using (MemoryStream resultStream = new MemoryStream())
            {
                // Read the decompressed data to a byte array
                byte[] buffer = new byte[8192];
                int bytesRead;
                while ((bytesRead = gzipStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    resultStream.Write(buffer, 0, bytesRead);
                }
                return resultStream.ToArray();
            }
        }

        internal static void Update()
        {

           
        }
        /// <summary>
        /// 拷贝文件夹
        /// </summary>
        /// <param name="srcdir"></param>
        /// <param name="desdir"></param>
        internal static void CopyDirectory(string srcdir, string desdir)
        {
            string folderName = srcdir.Substring(srcdir.LastIndexOf("\\") + 1);

            string desfolderdir = desdir + "\\" + folderName;

            if (desdir.LastIndexOf("\\") == (desdir.Length - 1))
            {
                desfolderdir = desdir + folderName;
            }
            string[] filenames = Directory.GetFileSystemEntries(srcdir);

            foreach (string file in filenames)// 遍历所有的文件和目录
            {
                if (Directory.Exists(file))// 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                {

                    string currentdir = desfolderdir + "\\" + file.Substring(file.LastIndexOf("\\") + 1);
                    if (!Directory.Exists(currentdir))
                    {
                        Directory.CreateDirectory(currentdir);
                    }

                    CopyDirectory(file, desfolderdir);
                }

                else // 否则直接copy文件
                {
                    string srcfileName = file.Substring(file.LastIndexOf("\\") + 1);

                    srcfileName = desfolderdir + "\\" + srcfileName;


                    if (!Directory.Exists(desfolderdir))
                    {
                        Directory.CreateDirectory(desfolderdir);
                    }


                    File.Copy(file, srcfileName);
                }
            }//foreach 
        }//function end
    }
}
