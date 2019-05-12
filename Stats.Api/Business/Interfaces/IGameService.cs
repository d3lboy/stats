using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stats.Common.Dto;

namespace Stats.Api.Business.Interfaces
{
    public interface IGameService
    {
        Task<GameDto> GetAsync(Guid id);
        Task<Guid> AddAsync(GameDto dto);
        Task<int> AddAsync(List<GameDto> dtos);
        Task<int> UpdateAsync(GameDto dto);
        Task<int> DeleteAsync(Guid id);
    }
}