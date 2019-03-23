using MediatR;
using Microsoft.Extensions.Options;
using MimeTypes;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ygo.application.Configuration;
using ygo.core.Services;
using ygo.domain.Helpers;

namespace ygo.application.Queries.CardImageByName
{
    public class CardImageByNameQueryHandler : IRequestHandler<CardImageByNameQuery, ImageResult>
    {
        private readonly IFileSystemService _fileSystemService;
        private readonly IOptions<ApplicationSettings> _settings;

        public CardImageByNameQueryHandler(IFileSystemService fileSystemService, IOptions<ApplicationSettings> settings)
        {
            _fileSystemService = fileSystemService;
            _settings = settings;
        }

        public Task<ImageResult> Handle(CardImageByNameQuery request, CancellationToken cancellationToken)
        {
            var response = new ImageResult();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                var imageFilePath = GetImagePath(request.Name.MakeValidFileName(), _settings.Value.CardImageFolderPath);

                if (!string.IsNullOrWhiteSpace(imageFilePath) && _fileSystemService.Exists(imageFilePath))
                {
                    response.Name = request.Name;
                    response.FilePath = imageFilePath;
                    response.Extension = Path.GetExtension(imageFilePath);
                    response.ContentType = MimeTypeMap.GetMimeType(response.Extension);
                    response.IsSuccessful = true;
                }
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