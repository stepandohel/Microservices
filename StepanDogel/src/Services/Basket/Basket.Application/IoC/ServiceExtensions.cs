using Basket.Application.Mapping;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Basket.Application.IoC
{
    public static class ServiceExtensions
    {
        public static IMvcBuilder AddApplication(this IMvcBuilder services)
        {
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

            return services;
        }
    }
}
