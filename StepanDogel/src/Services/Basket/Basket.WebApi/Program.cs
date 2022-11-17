using Basket.Application.IoC;
using Basket.Application.Middleware;
using Basket.Domain.Data;
using Basket.Infastructure.Ioc;
using Basket.Infastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigurationManager configuration = builder.Configuration;
builder.Services.AddDbContext<BasketDbContext>(option => option.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
    x => x.MigrationsAssembly(typeof(IBaseRepository<>).Assembly.FullName)
    ));
builder.Services.AddControllers()
    .AddServices()
    .AddApplication();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BasketDbContext>();
    await context.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();