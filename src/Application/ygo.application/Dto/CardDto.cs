using System;
using System.Collections.Generic;
using ygo.application.Ioc;

namespace ygo.application.Dto
{
    public class CardDto
    {
        public long Id { get; set; }
        public string CardNumber { get; set; }
        public string ImageUrl { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int? CardLevel { get; set; }
        public int? CardRank { get; set; }
        public int? Atk { get; set; }
        public int? Def { get; set; }
        public int? Link { get; set; }
        public List<SubCategoryDto> SubCategories { get; set; } = new List<SubCategoryDto>();
        public List<TypeDto> Types { get; set; } = new List<TypeDto>();
        public AttributeDto Attribute { get; set; }
        public List<LinkArrowDto> LinkArrows { get; set; } = new List<LinkArrowDto>();
    }
}