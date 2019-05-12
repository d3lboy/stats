using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stats.Common.Dto;

namespace Stats.Api.Business.Interfaces
{
    public interface IJobService
    {
        Task<List<JobDto>> GetAsync();
        Task<List<JobDto>> GetAsync(JobFilter filter);
        Task<JobDto> GetAsync(Guid id);
        Task<Guid> AddAsync(JobDto jobDto);
        Task<bool> AddAsync(List<JobDto> jobs);
        Task<int> UpdateAsync(JobDto jobDto);
        Task<int> DeleteAsync(Guid id);
    }
}