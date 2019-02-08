using System.Threading.Tasks;

namespace StatsFetcher.modules
{
    public interface IWebBrowser
    {
        Task<string> LoadUrl(string url);
    }
}
