using Microsoft.Extensions.DependencyInjection;
using SmartHome.Services.IoBroker;

namespace SmartHome.Services
{
    public static class RegisterStartupHostedServices
    {
        public static void AddHostedStartupServices(this IServiceCollection services)
        {
            services.AddHostedService<StartupConnectionService>();
        }
    }
}
