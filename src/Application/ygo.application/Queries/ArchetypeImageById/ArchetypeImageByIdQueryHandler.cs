using MediatR;
using Microsoft.Extensions.Options;
using MimeTypes;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ygo.core.Services;

namespace ygo.application.Queries.ArchetypeImageById
{
    public class ArchetypeImageByIdQueryHandler : IRequestHandler<ArchetypeImageByIdQuery, ImageResult>
    {
        private readonly IFileSystemService _fileSystemService;
        private readonly IOptions<ApplicationSettings> _settings;

        public ArchetypeImageByIdQueryHandler(IFileSystemService fileSystemService, IOptions<ApplicationSettings> settings)
        {
            _fileSystemService = fileSystemService;
            _settings = settings;
        }

        public Task<ImageResult> Handle(ArchetypeImageByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new ImageResult();

            var imageFilePath = GetImagePath(request.Id.ToString(), _settings.Value.ArchetypeImageFolderPath);

            if (!string.IsNullOrWhiteSpace(imageFilePath) && _fileSystemService.Exists(imageFilePath))
            {
                response.Name = request.Id.ToString();
                response.FilePath = imageFilePath;
                response.Extension = Path.GetExtension(imageFilePath);
                response.ContentType = MimeTypeMap.GetMimeType(response.Extension);
                response.IsSuccessful = true;
            }

            return Task.FromResult(response);
        }

        #region private helpers

        private string GetImagePath(string imageName, string directoryPath)
        {
            var filePaths = GetFilePaths(imageName, directoryPath);

            return filePaths.FirstOrDefault();
        }

        private IEnumerable<string> GetFilePaths(string fileName, string directoryPath)
        {
            var searchPattern = fileName + ".*";

            var imageFiles = _fileSystemService.GetFiles(directoryPath, searchPattern);

            return imageFiles;
        }

        #endregion

    }
}