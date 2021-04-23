using Microsoft.Extensions.DependencyInjection;

namespace MetricsManager.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddSingleton<WeatherStore>()
        ;
    }
}