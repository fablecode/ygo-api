using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using MimeTypes;
using ygo.domain.Helpers;
using ygo.domain.Service;

namespace ygo.application.Queries.CardImageByName
{
    public class CardImageByNameQueryHandler : IAsyncRequestHandler<CardImageByNameQuery, CardImageByNameResult>
    {
        private readonly IFileSystemService _fileSystemService;
        private readonly IOptions<ApplicationSettings> _settings;

        public CardImageByNameQueryHandler(IFileSystemService fileSystemService, IOptions<ApplicationSettings> settings)
        {
            _fileSystemService = fileSystemService;
            _settings = settings;
        }

        public Task<CardImageByNameResult> Handle(CardImageByNameQuery message)
        {
            var response = new CardImageByNameResult();

            if (!string.IsNullOrWhiteSpace(message.Name))
            {
                var imageFilePath = GetImagePath(message.Name.MakeValidFileName(), _settings.Value.CardImageFolderPath);

                if (!string.IsNullOrWhiteSpace(imageFilePath) && _fileSystemService.Exists(imageFilePath))
                {
                    response.Name = message.Name;
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