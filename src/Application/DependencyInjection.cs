using Application.Mapping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Add Automapper Configuration
            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}
