using System.Collections.Generic;

namespace ygo.application.Dto
{
    public class LatestBanlistDto
    {
        public string Format { get; set; }

        public List<LatestBanlistCardDto> Forbidden { get; set; }
        public List<LatestBanlistCardDto> Limited { get; set; }
        public List<LatestBanlistCardDto> SemiLimited { get; set; }
        public List<LatestBanlistCardDto> Unlimited { get; set; }
        public string ReleaseDate { get; set; }
    }
}