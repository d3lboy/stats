using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Stats.Api.Business.Exceptions;
using Stats.Api.Business.Interfaces;
using Stats.Api.Models;
using Stats.Common.Dto;
using Stats.Common.Enums;

namespace Stats.Api.Business
{
    public class TeamService : ITeamService
    {
        private readonly StatsDbContext context;
        private readonly IMapper mapper;

        public TeamService(StatsDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<TeamDto> GetAsync(Guid id)
        {
            var entity = await context.Teams.SingleOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                throw new Exception($"Team with id {id} does'n exist.");
            }

            return mapper.Map<TeamDto>(entity);
        }

        public async Task<List<TeamDto>> GetTeamsInSeasonAsync(Guid season)
        {
            var seasonEntity = await context.Seasons.SingleOrDefaultAsync(x => x.Id == season);

            if(seasonEntity == null)
                throw new ItemNotFoundException($"Season with id {season} doesn't exist.");

            var teamIds = JsonConvert.DeserializeObject<List<Guid>>(seasonEntity.Teams);

            var teams = await context.Teams.Where(x => teamIds.Contains(x.Id)).ToListAsync();

            return mapper.Map<List<TeamDto>>(teams);
        }

        public async Task<Guid> AddAsync(TeamDto dto)
        {
            var reference = await context.References.FirstOrDefaultAsync(x =>
                x.Competition == dto.Competition
                && x.Type == ReferenceType.Team
                && x.ReferenceValue == dto.ReferenceId);

            if(reference != null)
                throw new ItemAlreadyExistException($"Team with id {dto.ReferenceId} already exsits.");
            
            if(dto.Id == Guid.Empty)
                dto.Id = Guid.NewGuid();

            await context.References.AddAsync(new Reference
            {
                Competition = dto.Competition,
                Type = ReferenceType.Team,
                LocalValue = dto.Id.ToString().ToLowerInvariant(),
                ReferenceValue = dto.ReferenceId,
                Timestamp = DateTime.Now
            });

            await context.Teams.AddAsync(mapper.Map<Team>(dto));

            await context.SaveChangesAsync();

            return dto.Id;
        }

        public async Task<int> UpdateAsync(TeamDto dto)
        {
            var entity = await context.Teams.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if(entity == null)
                throw new ItemNotFoundException($"Team with id {dto.Id} doesn't exist.");

            entity.Address = dto.Address;
            entity.Name = dto.Name;
            entity.Url = dto.Url;
            entity.Timestamp = DateTime.Now;

            return await context.SaveChangesAsync();
        }
    }
}
