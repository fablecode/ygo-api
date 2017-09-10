using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Dto;
using ygo.application.Manager;

namespace ygo.infrastructure.Manager
{
    public class FileManager : IFileManager
    {
        private static ReaderWriterLock locker = new ReaderWriterLock();

        public async Task<DownloadedFileDto> Download(string remoteFileUrl, string localFileName)
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
    }
}