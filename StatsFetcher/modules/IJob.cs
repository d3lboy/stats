using System.Threading.Tasks;

namespace StatsFetcher.modules
{
    public interface IJob
    {
        Task<bool> Start();
    }
}
