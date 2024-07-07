using DesafioPitang.Business.Businesses;
using DesafioPitang.Business.Interface.IBusinesses;
using DesafioPitang.Repository.Repositories;
using DesafioPitang.Repository;
using DesafioPitang.Repository.Interface;
using DesafioPitang.Repository.Interface.IRepositories;
using DesafioPitang.WebApi.Middlewares;

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
        }

        private static void InjectMiddleware(IServiceCollection services)
        {
            services.AddTransient<ApiMiddleware>();
        }

        private static void InjectBusiness(IServiceCollection services)
        {
            services.AddScoped<IAppointmentBusiness, AppointmentBusiness>();
            services.AddScoped<ISchedulingBusiness, SchedulingBusiness>();
        }

        private static void InjectRepository(IServiceCollection services)
        {
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
        }
    }
}
