using CleanArchEcommerce.Application.Common.Behaviours;
using CleanArchEcommerce.Application.Common.Services.Tokens;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchEcommerce.Application
{
    public static class ConfigurationServicesApplication
    {
        public static IServiceCollection AddApplicationServices (this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(ctg =>
            {
                ctg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                ctg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            });
            services.AddHttpContextAccessor();
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}
