using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using ygo.application.Commands.AddCard;
using ygo.application.Commands.AddCategory;
using ygo.application.Commands.AddMonsterCard;
using ygo.application.Commands.AddSpellCard;
using ygo.application.Commands.AddTrapCard;
using ygo.application.Queries.CategoryById;

namespace ygo.application.Ioc
{
    public static class ApplicationInstaller
    {
        public static IServiceCollection AddCqrs(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ApplicationInstaller).GetTypeInfo().Assembly);

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CategoryByIdQuery>, CategoryByIdQueryValidator>();
            services.AddTransient<IValidator<AddCategoryCommand>, AddCategoryCommandValidator>();
            services.AddTransient<IValidator<AddCardCommand>, AddCardCommandValidator>();
            services.AddTransient<IValidator<AddMonsterCardCommand>, AddMonsterCardCommandValidator>();
            services.AddTransient<IValidator<AddSpellCardCommand>, AddSpellCardCommandValidator>();
            services.AddTransient<IValidator<AddTrapCardCommand>, AddTrapCardCommandValidator>();

            return services;
        }

    }
}