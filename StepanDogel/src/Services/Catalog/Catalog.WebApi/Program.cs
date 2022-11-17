using Catalog.Application.DI;
using Catalog.Application.DI;
using Catalog.Application.Middleware;
using Catalog.Mongo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.AddElasticSearch();

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddContext(configuration)
    .AddControllers()
    .AddApplication();

builder.Services.AddTransient<ProductService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = await builder.Build()
    .MigrateDatabaseAsync();


//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseDeveloperExceptionPage();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();
