﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Stats.Common.Dto;
using Stats.Common.Enums;

namespace Stats.Api.Business.Interfaces
{
    public interface IReferenceService
    {
        Task<List<ReferenceDto>> GetAsync(Competition competition);
        Task<List<ReferenceDto>> GetAsync(Competition competition, ReferenceType type);
        Task<ReferenceDto> TryGetAsync(Competition competition, ReferenceType type, string value);
    }
}