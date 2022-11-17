using Identity.WebApi.Data;
using Identity.WebApi.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.WebApi.DI
{
    public static class ServerExtension
    {
        public static IServiceCollection ConfigureApi(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }

        public static IServiceCollection ConfigureControllers(this IServiceCollection services)
        {
            services
                .AddControllers(config =>
                {
                    config.ReturnHttpNotAcceptable = true;
                    config.RespectBrowserAcceptHeader = true;
                })
                .AddXmlDataContractSerializerFormatters();

            return services;
        }

        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });

            return services;
        }

        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(config =>
                {
                    config.User.RequireUniqueEmail = true;
                    config.Password.RequiredLength = 6;
                    config.Password.RequireDigit = false;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<EfDbContext>()
                .AddDefaultTokenProviders()
                .AddClaimsPrincipalFactory<Claims>();

            return services;
        }

        public static IServiceCollection ConfigureIdentityServer(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddIdentityServer()
                    .AddAspNetIdentity<IdentityUser>()
                    .AddConfigurationStore(options =>
                    {
                        options.ConfigureDbContext = b =>
                            b.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                                sql => sql.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));
                    })
                    .AddOperationalStore(options =>
                    {
                        options.ConfigureDbContext = b =>
                            b.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sql =>
                                sql.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));
                    })
                    .AddDeveloperSigningCredential();

            return services;
        }

        public static IServiceCollection ConfigureSqlServerContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<EfDbContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b =>
                    b.MigrationsAssembly(typeof(Program).Assembly.GetName().Name)));

            return services;
        }
    }
}

