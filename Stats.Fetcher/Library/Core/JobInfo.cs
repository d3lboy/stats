using System;
using Stats.Common.Enums;

namespace Stats.Fetcher.Library.Core
{
    public class JobInfo
    {
        public Competition Competition { get; set; }
        public JobType Type { get; set; }
        public Type ObjectType { get; set; }
    }
}
