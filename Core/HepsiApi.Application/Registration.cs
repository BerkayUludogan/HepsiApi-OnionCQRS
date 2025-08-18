using FluentValidation;
using HepsiApi.Application.Beheviors;
using HepsiApi.Application.Exceptions;
using HepsiApi.Application.Features.Auth.Rules;
using HepsiApi.Application.Features.Products.Rules;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Reflection;

namespace HepsiApi.Application
{
    public static class Registration
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddTransient<ExceptionMiddleware>();
            services.AddTransient<ProductRules>();
            services.AddTransient<AuthRules>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
            services.AddValidatorsFromAssembly(assembly);
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr-TR");

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehevarior<,>));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RedisCacheBehevior<,>));
        }
        public static IServiceCollection AddRulesFromAssemblyContaining(
            this IServiceCollection services,
            Assembly assembly,
            Type type
            )
        {
            var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
            foreach (var item in types)
                services.AddTransient(item);
            return services;  
        }
    }
}
