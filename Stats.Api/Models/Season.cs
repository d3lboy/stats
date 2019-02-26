using System;
using System.ComponentModel.DataAnnotations;

namespace Stats.Api.Models
{
    public class Season : BaseModel
    {
        [Key]
        [MaxLength(20)]
        public string Id { get; set; }
        public Guid Competition { get; set; }
    }
}
