using System;

namespace ygo.application.Dto
{
    public class DownloadedFileDto
    {
        public Uri Source { get; set; }
        public string Destination { get; set; }
        public string ContentType { get; set; }
    }
}