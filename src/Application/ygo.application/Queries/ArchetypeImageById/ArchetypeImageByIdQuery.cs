using MediatR;

namespace ygo.application.Queries.ArchetypeImageById
{
    public class ArchetypeImageByIdQuery : IRequest<ImageResult>
    {
        public long Id { get; set; }
    }
}