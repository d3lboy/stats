using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stats.Common.Dto;

namespace Stats.Api.Business
{
    public interface IRoundManager
    {
        Task<List<RoundDto>> GetRounds(Guid season);
        Task<RoundDto> GetRound(Guid id);
        Task<Guid> SaveNewRound(RoundDto dto);
        Task<int> SaveNewRounds(List<RoundDto> dtos);
        Task<int> UpdateRound(RoundDto dto);
        Task<int> DeleteRound(Guid id);
    }
}