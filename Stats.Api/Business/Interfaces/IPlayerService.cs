using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stats.Common.Dto;

namespace Stats.Api.Business.Interfaces
{
    public interface IPlayerService
    {
        Task<PlayerDto> GetAsync(Guid id);
        Task<List<PlayerDto>> GetFromTeamAsync(Guid season);
        Task<Guid> AddAsync(PlayerDto dto);
        Task<int> UpdateAsync(PlayerDto dto);
    }
}
