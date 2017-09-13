using System;
using MediatR;

namespace ygo.application.Commands.DownloadImage
{
    public class DownloadImageCommand : IRequest<CommandResult>
    {
        public Uri RemoteImageUrl { get; set; }
        public string ImageFileName { get; set; }
    }
}