using EmployeeForum.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace EmployeeForum.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCompatablityWithJsonDataContractResolver(this IServiceCollection services)
        {
            services.AddMvc(
                 config =>
                 {
                     var policy = new AuthorizationPolicyBuilder()
                                      .RequireAuthenticatedUser()
                                      .Build();
                     config.Filters.Add(new AuthorizeFilter(policy));
                     config.RespectBrowserAcceptHeader = true;
                 }).AddJsonOptions(options =>
                            {
                                var jsonCaseResolver = options.SerializerSettings.ContractResolver;
                                if (jsonCaseResolver != null)
                                    (jsonCaseResolver as DefaultContractResolver).NamingStrategy = null;
                            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<EmployeeForumContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DevEnvConnection")));

        }

        public static void ConfigureJwtAutentication(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,                       

                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Jwt:SecurityKey"])),

                    };
                });
        }
    }
}
