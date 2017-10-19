using MediatR;

namespace ygo.application.Queries.BanlistExists
{
    public class BanlistExistsQuery : IRequest<bool>
    {
        public long Id { get; set; }
    }
}