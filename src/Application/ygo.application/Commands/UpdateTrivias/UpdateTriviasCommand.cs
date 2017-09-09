using System.Collections.Generic;

namespace ygo.application.Commands.UpdateTrivias
{
    public class UpdateTriviasCommand
    {
        public long CardId { get; set; }
        public List<string> Trivias { get; set; }
    }
}