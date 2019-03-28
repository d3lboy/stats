using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stats.Common.Dto;

namespace Stats.Api.Business.Interfaces
{
    public interface IJobs
    {
        Task<List<JobDto>> Get();
        Task<List<JobDto>> Get(JobFilter filter);
        Task<JobDto> Get(Guid id);
        Task<Guid> Add(JobDto jobDto);
        Task<bool> Add(List<JobDto> jobs);
        Task<int> Update(JobDto jobDto);
        Task<int> Delete(Guid id);
    }
}