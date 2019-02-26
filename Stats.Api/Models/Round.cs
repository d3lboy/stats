using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stats.Api.Models
{
    public class Round : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public Season Season { get; set; }
    }
}
