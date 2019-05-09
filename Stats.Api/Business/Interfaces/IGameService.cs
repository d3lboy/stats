using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stats.Common.Dto;

namespace Stats.Api.Business.Interfaces
{
    public interface IGameService
    {
        Task<GameDto> Get(Guid id);
        Task<Guid> Add(GameDto dto);
        Task<int> Add(List<GameDto> dtos);
        Task<int> Update(GameDto dto);
        Task<int> Delete(Guid id);
    }
}