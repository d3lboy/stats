using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;
using Stats.Common.Enums;

namespace Stats.Api.Models
{
    public class Job : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Stats.Common.Enums.Competition Competition { get; set; }
        public Url Url { get; set; }
        public string Args { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime ExecutedDate { get; set; }
        public JobState State { get; set; }
    }
}
