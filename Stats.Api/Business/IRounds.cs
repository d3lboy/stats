using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stats.Common.Dto;

namespace Stats.Api.Business
{
    public interface IRounds
    {
        Task<List<RoundDto>> Get(Guid season);
        Task<RoundDto> GetRound(Guid id);
        Task<Guid> Add(RoundDto dto);
        Task<int> Add(List<RoundDto> dtos);
        Task<int> Update(RoundDto dto);
        Task<int> Delete(Guid id);
    }
}