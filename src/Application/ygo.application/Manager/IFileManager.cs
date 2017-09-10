using System.IO;
using System.Threading.Tasks;
using ygo.application.Dto;

namespace ygo.application.Manager
{
    public interface IFileManager
    {
        Task<DownloadedFileDto> Download(string remoteFileUrl, string localFileName);
        void Delete(string localFileName);
        void Rename(string sourceFileName, string destinationFileName);
    }
}