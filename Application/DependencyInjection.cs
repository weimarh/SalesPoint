using MediatR;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Application.Behaviors;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>());

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddValidatorsFromAssembly(ApplicationAssemblyReference.Assembly);

            return services;
        }
    }
}
