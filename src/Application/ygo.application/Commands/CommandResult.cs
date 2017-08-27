using System.Collections.Generic;

namespace ygo.application.Commands
{
    public class CommandResult
    {
        public bool IsSuccessful { get; set; }

        public List<string> Errors { get; set; }

        public object Data { get; set; }
    }
}