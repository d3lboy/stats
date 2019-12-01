using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NpgsqlTypes;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;
using Stats.Api.Models;

namespace Stats.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConfigureLogging();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddEntityFrameworkNpgsql()
                .AddDbContext<StatsDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("StatsDb")))
                .BuildServiceProvider();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            DiRegistry.Init(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, StatsDbContext context)
        {
            context.Seed();

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void ConfigureLogging()
        {
            string connectionstring = Configuration.GetConnectionString("StatsDb");

            string tableName = "Logs";

            IDictionary<string, ColumnWriterBase> columnWriters = new Dictionary<string, ColumnWriterBase>
            {
                {"Message", new RenderedMessageColumnWriter() },
                {"Level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
                {"Exception", new ExceptionColumnWriter() },
                {"Timestamp", new TimestampColumnWriter() }
            };

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Verbose)
                .Enrich.FromLogContext()
                .WriteTo.PostgreSQL(connectionstring, tableName, columnWriters, needAutoCreateTable: true, respectCase: true)
                .CreateLogger();
        }
    }
}
