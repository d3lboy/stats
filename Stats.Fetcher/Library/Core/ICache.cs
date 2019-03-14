using System;
using System.Collections.Generic;
using Stats.Common.Dto;
using Stats.Common.Enums;

namespace Stats.Fetcher.Library.Core
{
    public interface ICache
    {
        Action<List<JobDto>> JobsAdded { get; set; }
        Action<JobDto> JobUpdated { get; set; }
        Action<JobDto> JobFinished { get; set; }
        List<KeyValuePair<Common.Enums.Competition, int>> JobsPerCompetition { get; }
        JobDto GetJobCandidate(Common.Enums.Competition competition);
        int Count { get; }
        void Add(List<JobDto> newJobs);
        void Update(JobDto dto);
    }
}