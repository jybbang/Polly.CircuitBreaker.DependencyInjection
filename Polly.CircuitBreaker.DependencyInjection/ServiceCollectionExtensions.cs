using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly.CircuitBreaker.DependencyInjection.Abstractions;
using System;
using System.Collections.Generic;

namespace Polly.CircuitBreaker.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCircuitBreaker(this IServiceCollection services,
            CircuitBreakerOptions circuitBreakerOption,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            if (circuitBreakerOption is null) throw new ArgumentNullException(nameof(circuitBreakerOption));

            services.Configure<CircuitBreakerOptions>(option => option = circuitBreakerOption);

            var serviceDescriptor = new ServiceDescriptor(typeof(ICircuitBreakerFactory), typeof(CircuitBreakerFactory), serviceLifetime);

            services.Add(serviceDescriptor);

            services.AddTransient(typeof(ICircuitBreaker<>), typeof(CircuitBreaker<>));

            return services;
        }

        public static IServiceCollection AddCircuitBreaker(this IServiceCollection services,
            CircuitBreakerOptionBuilder circuitBreakerOptionBuilder,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            if (circuitBreakerOptionBuilder is null) throw new ArgumentNullException(nameof(circuitBreakerOptionBuilder));

            services.AddCircuitBreaker(circuitBreakerOptionBuilder.Build(), serviceLifetime);

            return services;
        }

        public static IServiceCollection AddCircuitBreaker(this IServiceCollection services,
            Action<CircuitBreakerOptionBuilder> circuitBreakerOptionBuilder,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            var builder = new CircuitBreakerOptionBuilder();

            circuitBreakerOptionBuilder(builder);

            services.AddCircuitBreaker(builder, serviceLifetime);

            return services;
        }

        public static IServiceCollection AddCircuitBreaker(this IServiceCollection services,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            var builder = new CircuitBreakerOptionBuilder();

            services.AddCircuitBreaker(builder, serviceLifetime);

            return services;
        }

        public static IServiceCollection AddCircuitBreaker(this IServiceCollection services,
            IConfiguration configuration,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            var configurationSection = configuration?.GetSection(nameof(CircuitBreakerOptions));

            var options = configurationSection?.Get<CircuitBreakerOptions>() ?? new CircuitBreakerOptions();

            services.AddCircuitBreaker(options, serviceLifetime);

            return services;
        }
    }
}
