using System.Collections.Generic;

namespace ygo.application.Commands.UpdateTips
{
    public class UpdateTipsCommand
    {
        public long CardId { get; set; }
        public List<string> Tips { get; set; }
    }
}