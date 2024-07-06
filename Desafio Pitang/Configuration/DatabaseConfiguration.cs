using DesafioPitang.Repository;
using Microsoft.EntityFrameworkCore;


namespace DesafioPitang.WebApi.Configuration
{
    public static class DatabaseConfiguration
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Context>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
