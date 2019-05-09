using System;

namespace Stats.Common.Dto
{
    public class GameDto : BaseDto
    {
        public Guid Id { get; set; }
        public Guid Round { get; set; }
        public Guid? SeasonId { get; set; }
        public int? RoundNumber { get; set; }
        public Guid HomeTeam { get; set; }
        public short HomeScore { get; set; }
        public Guid VisitorTeam { get; set; }
        public short VisitorScore { get; set; }
        public DateTime Schedule { get; set; }
    }
}