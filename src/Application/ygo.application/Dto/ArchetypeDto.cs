using System.Collections.Generic;
using MediatR;

namespace ygo.application.Dto
{
    public class ArchetypeDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public List<ArchetypeCardDto> Cards { get; set; } = new List<ArchetypeCardDto>();
    }
}