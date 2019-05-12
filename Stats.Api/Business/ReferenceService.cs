using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Stats.Api.Business.Interfaces;
using Stats.Api.Models;
using Stats.Common.Dto;
using Stats.Common.Enums;
using Competition = Stats.Common.Enums.Competition;

namespace Stats.Api.Business
{
    public class ReferenceService : IReferenceService
    {
        private readonly StatsDbContext context;
        private readonly IMapper mapper;

        public ReferenceService(StatsDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<List<ReferenceDto>> GetAsync(Competition competition)
        {
            var result = await context.References.Where(x => x.Competition == competition).ToListAsync();
            return mapper.Map<List<ReferenceDto>>(result);
        }

        public async Task<List<ReferenceDto>> GetAsync(Competition competition, ReferenceType type)
        {
            var result = await context.References.Where(x => x.Competition == competition && x.Type == type).ToListAsync();
            return mapper.Map<List<ReferenceDto>>(result);
        }

        public async Task<ReferenceDto> TryGetAsync(Competition competition, ReferenceType type, string value)
        {
            var reference = await context.References.SingleAsync(x => x.Competition == competition && x.Type == type && x.LocalValue == value);

            if (reference != null) return mapper.Map<ReferenceDto>(reference);

            reference = new Reference
            {
                Type = type,
                Competition = competition,
                LocalValue = value,
                ReferenceValue = Guid.NewGuid().ToString(),
                Timestamp = DateTime.Now
            };

            await context.References.AddAsync(reference);
            await context.SaveChangesAsync();

            return mapper.Map<ReferenceDto>(reference);
        }
    }
}
