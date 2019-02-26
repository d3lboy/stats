using System;

namespace Stats.Api.Models
{
    public class Game : BaseModel
    {
        public Guid Id { get; set; }
        public Team HomeTeam { get; set; }
        public short HomeScore { get; set; }
        public Team VisitorTeam { get; set; }
        public short VisitorScore { get; set; }
        public DateTime Schedule { get; set; }
    }
}
