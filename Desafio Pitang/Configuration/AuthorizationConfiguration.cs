using DesafioPitang.Utils.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using tusdotnet.Helpers;

namespace DesafioPitang.WebApi.Configuration
{
    public static class AuthorizationConfiguration
    {
        public static void AddAuthorizationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var autenticacaoConfig = new AuthConfiguration
            {
                Issuer = configuration["Authorization:Issuer"],
                Audience = configuration["Authorization:Audience"],
                SecretKey = configuration["Authorization:SecretKey"],
                AccessTokenExpiration = int.Parse(configuration["Authorization:AccessTokenExpiration"]),
                RefreshTokenExpiration = int.Parse(configuration["Authorization:RefreshTokenExpiration"]),
            };


            services.AddCors(o => o.AddPolicy("CORS_POLICY", builder =>
            {
                builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowAnyOrigin()
                       .WithExposedHeaders(CorsHelper.GetExposedHeaders());
            }));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = autenticacaoConfig.Issuer,

                            ValidateAudience = true,
                            ValidAudience = autenticacaoConfig.Audience,

                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(autenticacaoConfig.SecretKey)),

                            RequireExpirationTime = true,
                            ValidateLifetime = true,

                            ClockSkew = TimeSpan.Zero
                        };
                    });
        }
    }
}
