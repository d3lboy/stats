using System;
using System.Threading.Tasks;
using Stats.Common.Dto;

namespace Stats.Fetcher.Library.Core
{
    public interface IJobBase : IDisposable
    {
        Task<bool> ProcessJob(JobDto dto);
        int? RescheduleInterval { get; set; }
    }
}