using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Stats.Common.Enums;

namespace Stats.Api.Models
{
    public class Round : BaseModel
    {
        [Key]
        public int Id { get; set; }

        public RoundType RoundType { get; set; }
        public Season Season { get; set; }
        [ForeignKey("Season")]
        public string SeasonId { get; set; }
    }
}
