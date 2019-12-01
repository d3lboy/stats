using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Stats.Api.Business.Interfaces;
using Stats.Api.Models;
using Stats.Common.Dto;

namespace Stats.Api.Business
{
    public class PlayerService:IPlayerService
    {
        private readonly StatsDbContext context;
        private readonly IMapper mapper;

        public PlayerService(StatsDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<PlayerDto> GetAsync(Guid id)
        {
            var result = await context.Players.SingleOrDefaultAsync(x => x.Id == id);
            return mapper.Map<PlayerDto>(result);
        }

        public async Task<List<PlayerDto>> GetFromTeamAsync(Guid season)
        {
            //var result = await context.Players.Where(x => x == id);
            //return mapper.Map<PlayerDto>(result);
            return  null;
        }

        public async Task<Guid> AddAsync(PlayerDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateAsync(PlayerDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
