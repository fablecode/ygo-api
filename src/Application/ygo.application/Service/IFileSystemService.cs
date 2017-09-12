using System;
using System.Threading.Tasks;
using ygo.application.Dto;

namespace ygo.application.Service
{
    public interface IFileSystemService
    {
        Task<DownloadedFileDto> Download(string remoteFileUrl, string localFileFullPath);
        Task<DownloadedFileDto> Download(Uri remoteFileUrl, string localFileFullPath);
        void Delete(string localFileFullPath);
        void Rename(string oldNameFullPath, string newNameFullPath);
        string[] GetFiles(string path, string searchPattern);
        bool Exists(string localFileFullPath);
    }
}