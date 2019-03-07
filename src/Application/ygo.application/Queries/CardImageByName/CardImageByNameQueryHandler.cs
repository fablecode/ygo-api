﻿using MediatR;
using Microsoft.Extensions.Options;
using MimeTypes;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ygo.domain.Helpers;
using ygo.domain.SystemIO;

namespace ygo.application.Queries.CardImageByName
{
    public class CardImageByNameQueryHandler : IRequestHandler<CardImageByNameQuery, CardImageByNameResult>
    {
        private readonly IFileSystem _fileSystem;
        private readonly IOptions<ApplicationSettings> _settings;

        public CardImageByNameQueryHandler(IFileSystem fileSystem, IOptions<ApplicationSettings> settings)
        {
            _fileSystem = fileSystem;
            _settings = settings;
        }

        public Task<CardImageByNameResult> Handle(CardImageByNameQuery request, CancellationToken cancellationToken)
        {
            var response = new CardImageByNameResult();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                var imageFilePath = GetImagePath(request.Name.MakeValidFileName(), _settings.Value.CardImageFolderPath);

                if (!string.IsNullOrWhiteSpace(imageFilePath) && _fileSystem.Exists(imageFilePath))
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

            var imageFiles = _fileSystem.GetFiles(directoryPath, searchPattern);

            return imageFiles;
        }

        #endregion

    }
}