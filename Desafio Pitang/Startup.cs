using DesafioPitang.WebApi.Configuration;
using DesafioPitang.WebApi.Middlewares;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace DesafioPitang.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDependencyInjectionConfiguration(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.MapType(typeof(TimeSpan), () => new() { Type = "string", Example = new OpenApiString("00:00:00") });
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Desafio Pitang",
                    Version = "v1",
                    Description = "API para o desafio da Pitang",
                    Contact = new() { Name = "Guilherme de Oliveira", Url = new Uri("https://github.com/GuilOliveira") },
                    License = new() { Name = "Private", Url = new Uri("https://github.com/GuilOliveira") },
                    TermsOfService = new Uri("http://google.com.br")
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio Pitang v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ApiMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

}
