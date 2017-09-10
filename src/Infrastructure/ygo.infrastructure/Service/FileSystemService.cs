using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.application.Service;

namespace ygo.infrastructure.Service
{
    public class FileSystemService : IFileSystemService
    {
        private static ReaderWriterLock locker = new ReaderWriterLock();

        public Task<DownloadedFileDto> Download(string remoteFileUrl, string localFileFullPath)
        {
            return Download(new Uri(remoteFileUrl), localFileFullPath);
        }

        public async Task<DownloadedFileDto> Download(Uri remoteFileUrl, string localFileFullPath)
        {
            try
            {
                locker.AcquireWriterLock(int.MaxValue);

                using (var webClient = new WebClient())
                {
                    webClient.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");
                    await webClient.DownloadFileTaskAsync(remoteFileUrl, localFileFullPath);

                    var contentType = webClient.ResponseHeaders["Content-Type"];

                    return new DownloadedFileDto
                    {
                        Source = remoteFileUrl,
                        Destination = localFileFullPath,
                        ContentType = contentType
                    };
                }
            }
            finally
            {
                locker.ReleaseWriterLock();
            }
        }

        public void Delete(string localFileFullPath)
        {
            int timeout = 30000;

            try
            {
                locker.AcquireWriterLock(int.MaxValue);

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
            finally
            {
                locker.ReleaseWriterLock();
            }
        }

        public void Rename(string oldNameFullPath, string newNameFullPath)
        {
            try
            {
                locker.AcquireWriterLock(int.MaxValue);

                File.Move(oldNameFullPath, newNameFullPath);
            }
            finally
            {
                locker.ReleaseWriterLock();
            }
        }

        public string[] GetFiles(string path, string searchPattern)
        {
            try
            {
                locker.AcquireReaderLock(int.MaxValue);

                return Directory.GetFiles(path, searchPattern);
            }
            finally
            {
                locker.ReleaseReaderLock();
            }
        }
    }
}