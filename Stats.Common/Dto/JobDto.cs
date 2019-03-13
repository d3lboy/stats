using System;
using Stats.Common.Enums;

namespace Stats.Common.Dto
{
    public class JobDto: BaseDto
    {
        public Guid Id { get; set; }
        public Competition Competition { get; set; }
        public string Url { get; set; }
        public string Args { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime ExecutedDate { get; set; }
        public JobState State { get; set; }
        
        

        public JobDto()
        {
            State = JobState.New;
        }

        public override string ToString()
        {
            return $"Competition: {Competition}, Url: {Url}";
        }
    }
}
