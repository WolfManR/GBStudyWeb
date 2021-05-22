using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace MetricsManager.Jobs
{
    public static class JobRegistrationExtensions
    {
        public static IServiceCollection AddJob<TJob>(this IServiceCollection services, string cronExpression) where TJob: class, IJob
        {
            return services
                .AddSingleton<TJob>()
                .AddSingleton(new JobSchedule(typeof(TJob), cronExpression));
        }
    }
}