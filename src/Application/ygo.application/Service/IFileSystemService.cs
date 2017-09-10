using System;
using System.Threading.Tasks;
using ygo.application.Dto;

namespace ygo.application.Service
{
    public interface IFileSystemService
    {
        Task<DownloadedFileDto> Download(string remoteFileUrl, string localFileName);
        Task<DownloadedFileDto> Download(Uri remoteFileUrl, string localImageFileName);
        void Delete(string localFileName);
        void Rename(string sourceFileName, string destinationFileName);
        string[] GetFiles(string path, string searchPattern);
    }
}