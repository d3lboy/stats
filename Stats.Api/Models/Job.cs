using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Stats.Common.Enums;

namespace Stats.Api.Models
{
    public class Job : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Common.Enums.Competition Competition { get; set; }
        [MaxLength(255)]
        public string Url { get; set; }
        [MaxLength(1000)]
        public string Args { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime ExecutedDate { get; set; }
        public JobState State { get; set; }
    }
}
