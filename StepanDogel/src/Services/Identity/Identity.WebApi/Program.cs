using AutoMapper.Configuration;
using Identity.WebApi.Data;
using Identity.WebApi.IdentityServer;
using IdentityModel.Client;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;

var builder = WebApplication.CreateBuilder(args);

IdentityModelEventSource.ShowPII = true;
ConfigurationManager configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");
var assembly = typeof(Program).Assembly.GetName().Name;

builder.Services.AddDbContext<EfDbContext>(option => option.UseSqlServer(connectionString,
    x => x.MigrationsAssembly(typeof(EfDbContext).Assembly.FullName)
));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts =>
{
    opts.Password.RequiredLength = 5;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireDigit = false;
})
    .AddEntityFrameworkStores<EfDbContext>();

builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddTestUsers(IdentityConfiguration.GetTestUsers())
    .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
    .AddInMemoryClients(IdentityConfiguration.Clients)
    .AddAspNetIdentity<IdentityUser>()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
            sql => sql.MigrationsAssembly(assembly));
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = x => x.UseSqlServer(connectionString,
             sql => sql.MigrationsAssembly(assembly));
    })
    ;

builder.Services.AddControllers();

builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://localhost:7003";

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EfDbContext>();
    context.Database.Migrate();
    scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
    var configurationDbContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

    configurationDbContext.Database.Migrate();
    if (!configurationDbContext.Clients.Any())
    {
        foreach (var client in IdentityConfiguration.Clients)
        {
            configurationDbContext.Clients.Add(client.ToEntity());
        }
        configurationDbContext.SaveChanges();
    }

    if (!configurationDbContext.IdentityResources.Any())
    {
        foreach (var resource in IdentityConfiguration.IdentityResources)
        {
            configurationDbContext.IdentityResources.Add(resource.ToEntity());
        }
        configurationDbContext.SaveChanges();
    }

    if (!configurationDbContext.ApiResources.Any())
    {
        foreach (var resource in IdentityConfiguration.ApiResources)
        {
            configurationDbContext.ApiResources.Add(resource.ToEntity());
        }
        configurationDbContext.SaveChanges();
    }

    if (!configurationDbContext.ApiScopes.Any())
    {
        foreach (var resource in IdentityConfiguration.ApiScopes)
        {
            configurationDbContext.ApiScopes.Add(resource.ToEntity());
        }
        configurationDbContext.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseIdentityServer();

app.MapControllers();

app.Run();
