using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using ygo.application.Queries.CategoryById;

namespace ygo.application.Ioc
{
    public static class ApplicationInstaller
    {
        public static IServiceCollection AddCqrs(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ApplicationInstaller).GetTypeInfo().Assembly);

            services.AddTransient<IValidator<CategoryByIdQuery>, CategoryByIdQueryValidator>();

            return services;
        }
    }
}