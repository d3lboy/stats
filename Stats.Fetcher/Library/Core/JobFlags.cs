using System;
using System.IO.Compression;
using Stats.Common.Enums;

namespace Stats.Fetcher.Library.Core
{
    [AttributeUsage(AttributeTargets.Class)]
    public class JobFlags : Attribute
    {
        public JobFlags(Common.Enums.Competition competition, JobType type)
        {
            Competition = competition;
            Type = type;
        }

        public Common.Enums.Competition Competition { get; }
        public JobType Type { get; }
    }
}
