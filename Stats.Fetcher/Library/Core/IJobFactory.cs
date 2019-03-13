using System.Collections.Generic;
using Stats.Common.Enums;

namespace Stats.Fetcher.Library.Core
{
    public interface IJobFactory
    {
        IJobBase CreateInstance(Competition competition, JobType type);
        List<JobInfo> Jobs { get; }
    }
}