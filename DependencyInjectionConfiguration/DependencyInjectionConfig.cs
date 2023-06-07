using Microsoft.Extensions.DependencyInjection;
using Interfaces.Map;
using Interfaces.Repository;
using Interfaces.Service;
using Maps;
using Repositories.Repositories;
using Services;

namespace DependencyInjectionConfiguration
{
    public class DependencyInjectionConfig
    {


        public static void AddScope(IServiceCollection services)
        {
            services.AddScoped<IUserMap, UserMap>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPharmacyMap, PharmacyMap>();
            services.AddScoped<IPharmacyService, PharmacyService>();
            services.AddScoped<IPharmacyRepository, PharmacyRepository>();

        }
    }
}
