using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Stats.Common.Enums;

namespace Stats.Api.Models
{
    public class Round : BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        public int RoundNumber { get; set; }
        public RoundType RoundType { get; set; }
        private Season Season { get; set; }
        [ForeignKey("Season")]
        public Guid SeasonId { get; set; }
    }
}
