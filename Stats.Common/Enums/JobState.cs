using System;

namespace Stats.Common.Enums
{
    [Flags]
    public enum JobState
    {
        New = 1,
        InProgress = 2,
        Finished = 3,
        Error = 4
    }
}
