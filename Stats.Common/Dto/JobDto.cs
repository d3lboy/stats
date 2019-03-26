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
        public JobType Type { get; set; }
        public Guid? Parent { get; set; }

        public JobDto()
        {
            State = JobState.New;
        }

        public override string ToString()
        {
            return $"Competition: {Enum.GetName(typeof(Competition), Competition)}, Type: {Type}, Id:{Id}, " +
                   $"State: {Enum.GetName(typeof(JobState), State)}, Scheduled: {ScheduledDate}";
        }
    }
}
