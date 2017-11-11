using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ygo.core.Models;
using ygo.domain.Service;

namespace ygo.infrastructure.Service
{
    public class FileSystemService : IFileSystemService
    {
        private static readonly object RenameLocker = new object();
        private static readonly object GetFilesLocker = new object();
        private static readonly object ExistsLocker = new object();

        public Task<DownloadedFile> Download(string remoteFileUrl, string localFileFullPath)
        {
            return Download(new Uri(remoteFileUrl), localFileFullPath);
        }

        public async Task<DownloadedFile> Download(Uri remoteFileUrl, string localFileFullPath)
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");
                await webClient.DownloadFileTaskAsync(remoteFileUrl, localFileFullPath);

                var contentType = webClient.ResponseHeaders["Content-Type"];

                return new DownloadedFile
                {
                    Source = remoteFileUrl,
                    Destination = localFileFullPath,
                    ContentType = contentType,
                };
            }
        }

        public void Delete(string localFileFullPath)
        {
            int timeout = 30000;

            using (var fw = new FileSystemWatcher(Path.GetDirectoryName(localFileFullPath), Path.GetFileName(localFileFullPath)))
            using (var mre = new ManualResetEventSlim())
            {
                fw.EnableRaisingEvents = true;
                fw.Deleted += (sender, e) =>
                {
                    mre.Set();
                };

                File.Delete(localFileFullPath);
                mre.Wait(timeout);
            }

        }

        public void Rename(string oldNameFullPath, string newNameFullPath)
        {
            lock (RenameLocker)
            {
                File.Move(oldNameFullPath, newNameFullPath);
            }
        }

        public string[] GetFiles(string path, string searchPattern)
        {
            lock (GetFilesLocker)
            {
                return Directory.GetFiles(path, searchPattern);
            }
        }

        public bool Exists(string localFileFullPath)
        {
            lock (ExistsLocker)
            {
                return File.Exists(localFileFullPath);
            }
        }
    }
}