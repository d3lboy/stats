using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Stats.Api.Models;
using Stats.Common.Dto;

namespace Stats.Api.Business.Mappings
{
    public class JobProfile : Profile
    {
        public JobProfile()
        {
            CreateMap<JobDto, Job>();
            CreateMap<Job, JobDto>();
        }
    }
}
