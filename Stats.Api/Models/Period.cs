using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stats.Api.Models
{
    public class Period : BaseModel
    {
        public int Id { get; set; }
        public Game Game { get; set; }
        [ForeignKey("Game")]
        public Guid GameId { get; set; }
        public short HomeScore { get; set; }
        public short VisitorScore { get; set; }
    }
}
