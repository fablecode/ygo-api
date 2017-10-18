using System;

namespace ygo.application.CommandsAddBanlist
{
    public class AddBanlistCommand
    {
        public long? Id { get; set; }
        public long? FormatId { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}