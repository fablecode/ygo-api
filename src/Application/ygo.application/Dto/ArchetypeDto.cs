using System.Collections.Generic;

namespace ygo.application.Dto
{
    public class ArchetypeDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }

        public List<ArchetypeCardDto> Cards { get; set; } = new List<ArchetypeCardDto>();
    }
}