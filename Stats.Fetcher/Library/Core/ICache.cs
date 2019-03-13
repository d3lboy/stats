using System;
using System.Collections.Generic;
using Stats.Common.Dto;
using Stats.Common.Enums;

namespace Stats.Fetcher.Library.Core
{
    public interface ICache
    {
        Action<JobDto> Updated { get; set; }
        List<KeyValuePair<Competition, int>> JobsPerCompetition { get; }
        JobDto GetJobCandidate(Competition competition);
        void Add(List<JobDto> newJobs);
        void Update(JobDto dto);
    }
}