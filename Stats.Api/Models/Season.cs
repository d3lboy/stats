using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stats.Api.Models
{
    public class Season : BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        private Competition Competition { get; set; }
        [ForeignKey("Competition")]
        public Stats.Common.Enums.Competition CompetitionId { get; set; }
    }
}
