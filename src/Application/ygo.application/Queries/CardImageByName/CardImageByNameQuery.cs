using MediatR;

namespace ygo.application.Queries.CardImageByName
{
    public class CardImageByNameQuery : IRequest<ImageResult>
    {
        public string Name { get; set; }
    }
}