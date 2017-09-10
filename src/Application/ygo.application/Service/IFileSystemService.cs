using System.Threading.Tasks;
using ygo.application.Dto;

namespace ygo.application.Service
{
    public interface IFileSystemService
    {
        Task<DownloadedFileDto> Download(string remoteFileUrl, string localFileName);
        void Delete(string localFileName);
        void Rename(string sourceFileName, string destinationFileName);
    }
}