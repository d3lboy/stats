using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stats.Common.Dto;

namespace Stats.Api.Business.Interfaces
{
    public interface IRoundService
    {
        Task<List<RoundDto>> GetAsync(Guid season);
        Task<RoundDto> GetRoundAsync(Guid id);
        Task<Guid> AddAsync(RoundDto dto);
        Task<int> AddAsync(List<RoundDto> dtos);
        Task<int> UpdateAsync(RoundDto dto);
        Task<int> DeleteAsync(Guid id);
    }
}