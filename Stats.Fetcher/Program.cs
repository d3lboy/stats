using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using Stats.Fetcher.Jobs.ABA;
using Stats.Fetcher.Library;
using Stats.Fetcher.Library.Clients;
using Stats.Fetcher.Library.Core;
using Stats.Fetcher.Library.DiInjection;

namespace Stats.Fetcher
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Verbose)
                .Enrich.FromLogContext()
                .WriteTo.Console(theme: SystemConsoleTheme.Colored)
                .CreateLogger();

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

                    Scanner.Scan(services);

                })
                .UseSerilog();

            await builder.RunConsoleAsync();
        }
    }
}
