using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stats.Common.Dto;

namespace Stats.Api.Business.Interfaces
{
    public interface ITeamService
    {
        Task<TeamDto> GetAsync(Guid id);
        Task<List<TeamDto>> GetTeamsInSeasonAsync(Guid season);
        Task<Guid> AddAsync(TeamDto dto);
        Task<int> UpdateAsync(TeamDto dto);
    }
}
