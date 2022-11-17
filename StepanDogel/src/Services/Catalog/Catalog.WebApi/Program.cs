using Confluent.Kafka;
using IdentityModel.Client;
using Microsoft.IdentityModel.Tokens;
using Serilog.Sinks.Elasticsearch;
using Serilog;
using System.Reflection;
using Serilog.Exceptions;
using Kafka;
using Microsoft.EntityFrameworkCore;
using MediatR;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Catalog.Application.IoC;
using Identity.WebApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigureLogs();
builder.Host.UseSerilog();
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddDbContext<Catalog.Domain.Data.EfDbContext>(option => option.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
    x => x.MigrationsAssembly(typeof(Catalog.Domain.Data.EfDbContext).Assembly.FullName)
    ));

builder.Services.AddControllers()
    .AddApplication();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://localhost:7003";

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<Catalog.Domain.Data.EfDbContext>();
    await context.Database.MigrateAsync();
}

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();


app.Run();

void ConfigureLogs()
{
    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()
        .WriteTo.Debug()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(ConfigureELS(configuration, env))
        .CreateLogger();
}

ElasticsearchSinkOptions ConfigureELS(IConfigurationRoot configuration, string env)
{
    return new ElasticsearchSinkOptions(new Uri(configuration["ELKConfiguration:Uri"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower()}-{env.ToLower().Replace(".", ",")}-{DateTime.UtcNow:yyyy-MM}"
    };
}
