using System.Collections.Generic;
using MediatR;

namespace ygo.application.Commands.AddCard
{
    public class AddCardCommand : IRequest<CommandResult>
    {
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CardLevel { get; set; }
        public int? CardRank { get; set; }
        public int? Atk { get; set; }
        public int? Def { get; set; }

        public List<string> SubCategories { get; set; }
        public List<string> Types { get; set; }
        public List<string> LinkArrows { get; set; }

        public List<string> Tips { get; set; }
        public List<string> Rulings { get; set; }
        public List<string> Trivia { get; set; }

    }
}