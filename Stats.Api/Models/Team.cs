using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Stats.Common.Enums;

namespace Stats.Api.Models
{
    public class Team : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Url { get; set; }
        [MaxLength(255)]
        public string Address { get; set; }
        public Country? Country { get; set; }
    }
}
