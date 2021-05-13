using FluentMigrator.Runner;
using MetricsAgent.DataBase;
using MetricsAgent.DataBase.Interfaces;
using MetricsAgent.DataBase.Repositories;
using MetricsAgent.Jobs;
using MetricsAgent.Jobs.MetricsJobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace MetricsAgent
{
    public class Startup
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new() {Title = "MetricsAgent", Version = "v1"}));

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString(ConnectionString)
                    .ScanIn(typeof(Startup).Assembly).For.Migrations())
                .AddLogging(logging => logging.AddFluentMigratorConsole());

            services.AddAutoMapper(typeof(MapperProfile));
            services
                .AddSingleton(new SQLiteContainer(ConnectionString))
                .AddTransient<SQLiteInitializer>();

            services
                .AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>()
                .AddSingleton<IDotnetMetricsRepository, DotnetMetricsRepository>()
                .AddSingleton<IHddMetricsRepository, HddMetricsRepository>()
                .AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>()
                .AddSingleton<IRamMetricsRepository, RamMetricsRepository>()
                ;

            services
                .AddSingleton<IJobFactory, JobFactory>()
                .AddSingleton<ISchedulerFactory, StdSchedulerFactory>()
                .AddHostedService<QuartzHostedService>();
            services
                .AddJob<CpuMetricJob>("0/5 * * * * ?")
                .AddJob<DotnetMetricJob>("0/5 * * * * ?")
                .AddJob<HddMetricJob>("0/5 * * * * ?")
                .AddJob<NetworkMetricJob>("0/5 * * * * ?")
                .AddJob<RamMetricJob>("0/5 * * * * ?")
                ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SQLiteInitializer initializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MetricsAgent v1"));
            }

            initializer.Init();
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}