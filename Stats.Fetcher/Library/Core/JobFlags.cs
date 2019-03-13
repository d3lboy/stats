using System;
using Stats.Common.Enums;

namespace Stats.Fetcher.Library.Core
{
    [AttributeUsage(AttributeTargets.Class)]
    public class JobFlags : Attribute
    {
        public JobFlags(Competition competition, JobType type)
        {
            Competition = competition;
            Type = type;
        }

        public Competition Competition { get; }
        public JobType Type { get; }
    }
}
