using System.Threading.Tasks;

namespace Stats.Fetcher.Modules
{
    public interface IJob
    {
        Task<bool> Start();
    }
}
