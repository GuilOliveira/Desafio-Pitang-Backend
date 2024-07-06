using DesafioPitang.Repository;
using DesafioPitang.Repository.Interface;
using DesafioPitang.WebApi.Middlewares;

namespace DesafioPitang.WebApi.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            InjectMiddleware(services);

            services.AddScoped<ITransactionManager, TransactionManager> ();
        }

        private static void InjectMiddleware(IServiceCollection services)
        {
            services.AddTransient<ApiMiddleware>();
        }
    }
}
