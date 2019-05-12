using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Stats.Api.Business.Exceptions;
using Stats.Api.Business.Interfaces;
using Stats.Api.Models;
using Stats.Common.Dto;

namespace Stats.Api.Business
{
    public class RoundService : IRoundService
    {
        private readonly StatsDbContext context;
        private readonly IMapper mapper;

        public RoundService(StatsDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<RoundDto>> GetAsync(Guid season)
        {
            var rounds = await context.Rounds.Where(x => x.SeasonId == season).ToListAsync();

            return rounds.Any() ? rounds.Select(mapper.Map<RoundDto>).ToList() : default;
        }

        public async Task<RoundDto> GetRoundAsync(Guid id)
        {
            var round = await context.Rounds.SingleOrDefaultAsync(x => x.Id == id);

            if (round == null)
            {
                throw new ItemNotFoundException();
            }

            return mapper.Map<RoundDto>(round);
        }

        public async Task<Guid> AddAsync(RoundDto dto)
        {
            var round = await context.Rounds.SingleOrDefaultAsync(x => x.Id == dto.Id);
            if (round != null)
            {
                throw new ItemAlreadyExistException($"Round {round.Id} not found!");
            }

            round = mapper.Map<Round>(dto);
            round.Id = Guid.NewGuid();
            context.Rounds.Add(round);

            await context.SaveChangesAsync();

            return round.Id;
        }

        public async Task<int> AddAsync(List<RoundDto> dtos)
        {
            Guid? seasonId = dtos.FirstOrDefault()?.Season;

            if (!seasonId.HasValue)
                return default;

            try
            {
                var rounds = await context.Rounds.Where(x => x.SeasonId == seasonId).ToListAsync();

                dtos.ForEach(dto =>
                {
                    if (rounds.All(x => x.RoundNumber != dto.RoundNumber))
                    {
                        var round = mapper.Map<Round>(dto);
                        context.Rounds.Add(round);
                    }
                });

                return await context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return default;
            }
        }

        public async Task<int> UpdateAsync(RoundDto dto)
        {
            var round = await context.Rounds.SingleOrDefaultAsync(x => x.Id == dto.Id);
            if (round == null)
            {
                throw new ItemNotFoundException();
            }

            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var round = await context.Rounds.SingleOrDefaultAsync(x => x.Id == id);
            if (round == null)
            {
                throw new ItemNotFoundException();
            }

            context.Rounds.Remove(round);
            return await context.SaveChangesAsync();
        }
    }
}
