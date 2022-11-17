using Basket.Application.DI;
using Basket.Application.Middleware;
using Basket.Infastructure.DI;
using Basket.Infastructure.GRPC.GrpcServices;
using Basket.Infastructure.DI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddContext(configuration)
    .AddControllers()
    .AddServices()
    .AddApplication();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGrpc();

var app = await builder.Build()
    .MigrateDatabaseAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapGrpcService<CustomerGrpcService>();
app.MapControllers();

app.Run();
