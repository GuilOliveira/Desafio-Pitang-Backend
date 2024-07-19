using DesafioPitang.Business.Businesses;
using DesafioPitang.Business.Interface.IBusinesses;
using DesafioPitang.Repository.Repositories;
using DesafioPitang.Repository;
using DesafioPitang.Repository.Interface;
using DesafioPitang.Repository.Interface.IRepositories;
using DesafioPitang.WebApi.Middlewares;
using DesafioPitang.Utils.UserContext;
using DesafioPitang.Utils.Configuration;

namespace DesafioPitang.WebApi.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            InjectMiddleware(services);
            InjectBusiness(services);
            InjectRepository(services);

            services.AddScoped<ITransactionManager, TransactionManager> ();
            services.AddScoped<IUserContext, UserContext> ();
            services.AddOptions<AuthConfiguration>().Bind(configuration.GetSection("Authorization"));
        }

        private static void InjectMiddleware(IServiceCollection services)
        {
            services.AddTransient<ApiMiddleware>();
            services.AddTransient<UserContextMiddleware>();
        }

        private static void InjectBusiness(IServiceCollection services)
        {
            services.AddScoped<IAppointmentBusiness, AppointmentBusiness>();
            services.AddScoped<ISchedulingBusiness, SchedulingBusiness>();
            services.AddScoped<IUserBusiness, UserBusiness>();
            services.AddScoped<IAuthenticationBusiness, AuthenticationBusiness>();
        }

        private static void InjectRepository(IServiceCollection services)
        {
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
