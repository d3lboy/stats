using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Stats.Fetcher.Jobs.ABA;
using Stats.Fetcher.Library;
using Stats.Fetcher.Library.Clients;
using Stats.Fetcher.Library.Core;

namespace Stats.Fetcher
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true);
                    config.AddEnvironmentVariables();

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();
                    services.Configure<AppConfig>(hostContext.Configuration.GetSection("AppConfig"));

                    services.AddSingleton<IHostedService, JobManager>();                   
                    services.AddSingleton<ICache, Cache>();
                    services.AddTransient<Browser, Browser>();
                    services.AddTransient<IApiClient, ApiClient>();
                    services.AddTransient<IJobFactory, JobFactory>();
                    services.AddTransient<ICompetition, Competition>();
                    services.AddTransient<ICompetitionFactory, CompetitionFactory>();
                    services.AddTransient<ISynchronizer, Synchronizer>();
                    services.AddTransient<Rounds, Rounds>();

                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                });

            await builder.RunConsoleAsync();
        }
    }
}
