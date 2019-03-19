using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stats.Common.Dto;

namespace Stats.Api.Business
{
    public interface IJobManager
    {
        Task<List<JobDto>> GetJobs();
        Task<List<JobDto>> GetJobs(JobFilter filter);
        Task<JobDto> GetJob(Guid id);
        Task<Guid> SaveNewJob(JobDto jobDto);
        Task<bool> BulkInsert(List<JobDto> jobs);
        Task<int> UpdateJob(JobDto jobDto);
        Task<int> DeleteJob(Guid id);
    }
}