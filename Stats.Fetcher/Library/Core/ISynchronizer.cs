using System.Threading.Tasks;

namespace Stats.Fetcher.Library.Core
{
    public interface ISynchronizer
    {
        Task FetchNewJobs();
    }
}