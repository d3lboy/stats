using System.Threading.Tasks;

namespace Stats.Fetcher.Modules
{
    public interface IWebBrowser
    {
        Task<string> LoadUrl(string url);
    }
}
