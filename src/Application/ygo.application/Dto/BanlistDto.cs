using System;

namespace ygo.application.Dto
{
    public class BanlistDto
    {
        public long Id { get; set; }
        public FormatDto Format { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public BanlistCardDto Cards { get; set; }
    }
}