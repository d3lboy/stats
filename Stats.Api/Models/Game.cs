using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stats.Api.Models
{
    public class Game : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        private Round Round { get; set; }
        [ForeignKey("Round")]
        public Guid RoundId { get; set; }
        private Team HomeTeam { get; set; }
        [ForeignKey("HomeTeam")]
        public Guid HomeTeamId { get; set; }
        public short HomeScore { get; set; }
        private Team VisitorTeam { get; set; }
        [ForeignKey("VisitorTeam")]
        public Guid VisitorTeamId { get; set; }
        public short VisitorScore { get; set; }
        public DateTime Schedule { get; set; }
    }
}
