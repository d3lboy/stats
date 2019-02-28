using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stats.Api.Models
{
    public class Season : BaseModel
    {
        [Key]
        [MaxLength(20)]
        public string Id { get; set; }
        public Competition Competition { get; set; }
        [ForeignKey("Competition")]
        public Stats.Common.Enums.Competition CompetitionId { get; set; }
    }
}
