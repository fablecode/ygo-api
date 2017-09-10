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

        public Task<DownloadedFileDto> Download(string remoteFileUrl, string localFileName)
        {
            return Download(new Uri(remoteFileUrl), localFileName);
        }

        public async Task<DownloadedFileDto> Download(Uri remoteFileUrl, string localFileName)
        {
            try
            {
                locker.AcquireWriterLock(int.MaxValue);

                using (var webClient = new WebClient())
                {
                    webClient.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");
                    await webClient.DownloadFileTaskAsync(remoteFileUrl, localFileName);

                    var contentType = webClient.ResponseHeaders["Content-Type"];

                    return new DownloadedFileDto
                    {
                        Source = remoteFileUrl,
                        Destination = localFileName,
                        ContentType = contentType
                    };
                }
            }
            finally
            {
                locker.ReleaseWriterLock();
            }
        }

        public void Delete(string localFileName)
        {
            try
            {
                locker.AcquireWriterLock(int.MaxValue);

                File.Delete(localFileName);
            }
            finally
            {
                locker.ReleaseWriterLock();
            }
        }

        public void Rename(string sourceFileName, string destinationFileName)
        {
            try
            {
                locker.AcquireWriterLock(int.MaxValue);

                File.Move(sourceFileName, destinationFileName);
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