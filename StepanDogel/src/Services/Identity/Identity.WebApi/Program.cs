using Auth;
using Identity.Application.DI;
using Identity.WebApi.DI;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);

IdentityModelEventSource.ShowPII = true;
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddContext(configuration)
    .ConfigureCors()
    .ConfigureSqlServerContext(builder.Configuration)
    .ConfigureIdentity()
    .ConfigureIdentityServer(builder.Configuration);

builder.Services.ConfigureApi()
    .AddEndpointsApiExplorer();

builder.Services.AddControllers()
    .AddApplication();

builder.Services.AddBearerAuth("https://localhost:7003");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = await builder.Build()
    .MigrateDatabaseAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
app.UseCors("CorsPolicy");

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseIdentityServer();

app.Run();
