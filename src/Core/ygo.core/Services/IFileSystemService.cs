using System;
using System.Threading.Tasks;
using ygo.core.Models;

namespace ygo.core.Services
{
    public interface IFileSystemService
    {
        Task<DownloadedFile> Download(string remoteFileUrl, string localFileFullPath);
        Task<DownloadedFile> Download(Uri remoteFileUrl, string localFileFullPath);
        void Delete(string localFileFullPath);
        void Rename(string oldNameFullPath, string newNameFullPath);
        string[] GetFiles(string path, string searchPattern);
        bool Exists(string localFileFullPath);
    }
}