using Microsoft.Extensions.DependencyInjection;
using SmartHome.Entities.Lights;

namespace SmartHome.Entities
{
    public static class RegisterEntitiesExtension
    {
        public static void AddEntities(this IServiceCollection services)
        {
            services.AddSingleton<AllLights>();
        }
    }
}
