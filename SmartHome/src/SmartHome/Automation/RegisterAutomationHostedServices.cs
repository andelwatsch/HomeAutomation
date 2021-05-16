using Microsoft.Extensions.DependencyInjection;

namespace SmartHome.Automation
{
    public static class RegisterAutomationHostedServices
    {
        public static void AddAutomationHostedServices(this IServiceCollection services)
        {
            services.AddHostedService<Office.Office>();
        }
    }
}
