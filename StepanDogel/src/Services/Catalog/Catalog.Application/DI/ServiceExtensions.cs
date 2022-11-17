using Catalog.Application.Mapping;
using Catalog.Domain.Repository.Interfaces;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Catalog.Application.DI
{
    public static class ServiceExtensions
    {
        public static IMvcBuilder AddApplication(this IMvcBuilder services)
        {
            services.Services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.Services.AddAutoMapper(typeof(AppMappingProfile));

            services.AddFluentValidation(fv =>
                 {
                     fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                     fv.ImplicitlyValidateChildProperties = true;
                     fv.ImplicitlyValidateRootCollectionElements = true;
                 }).ConfigureApiBehaviorOptions(options => options.InvalidModelStateResponseFactory = context =>
                 {
                     return new BadRequestObjectResult(new
                     {
                         message = "One or more validators are corrupted",
                         errors = context.ModelState.SelectMany(pair => pair.Value.Errors.Select(error => error.ErrorMessage))
                     });
                 });
            services.Services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
