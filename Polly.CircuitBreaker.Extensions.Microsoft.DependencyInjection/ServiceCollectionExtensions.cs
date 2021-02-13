using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly.CircuitBreaker.Extensions.DependencyInjection.Abstractions;
using System;

namespace Polly.CircuitBreaker.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCircuitBreaker(this IServiceCollection services,
            IConfiguration configuration = null,
            string configurationSection = "CircuitBreaker",
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            var serviceDescriptor = new ServiceDescriptor(typeof(ICircuitBreakerFactory), typeof(CircuitBreakerFactory), serviceLifetime);

            services.Add(serviceDescriptor);

            services.AddTransient(typeof(ICircuitBreaker<>), typeof(CircuitBreaker<>));

            services.Configure<CircuitBreakerOption>(configuration.GetSection(configurationSection).Bind);

            return services;
        }
    }
}
