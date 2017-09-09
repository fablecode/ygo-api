using System.Collections.Generic;

namespace ygo.application.Commands.UpdateRulings
{
    public class UpdateRulingsCommand
    {
        public long CardId { get; set; }
        public List<string> Rulings { get; set; }
    }
}