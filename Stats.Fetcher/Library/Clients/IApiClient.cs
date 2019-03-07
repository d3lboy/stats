using System;
using System.Threading.Tasks;
using Stats.Common.Dto;

namespace Stats.Fetcher.Library.Clients
{
    public interface IApiClient : IDisposable
    {
        Task<T> Get<T>(string action);
        Task<bool> Post(string action, BaseDto dto);
        Task<T> Post<T>(string action, BaseDto dto);
        Task<bool> Put(string action, BaseDto dto);
        Task<bool> Delete(string action);
    }
}
