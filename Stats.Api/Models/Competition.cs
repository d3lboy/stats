using System.ComponentModel.DataAnnotations;
using Stats.Common.Enums;

namespace Stats.Api.Models
{
    public class Competition : BaseModel
    {
        [Key]
        public Common.Enums.Competition Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }

        public Country Country { get; set; }
    }
}
