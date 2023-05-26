using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Configuration
{
    public class DBContextConfig
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Models.Context.AppContext>(options =>
                          options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
