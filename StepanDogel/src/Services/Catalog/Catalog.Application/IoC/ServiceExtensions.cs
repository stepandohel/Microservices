using Catalog.Application.Mapping;
using Catalog.Domain.Interfaces;
using Catalog.Domain;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Application.IoC
{
    public static class ServiceExtensions
    {
        public static IMvcBuilder AddApplication(this IMvcBuilder services)
        {
            services.Services.AddScoped(typeof(IBaseRepository<,,,>), typeof(BaseRepository<,,,>));
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
