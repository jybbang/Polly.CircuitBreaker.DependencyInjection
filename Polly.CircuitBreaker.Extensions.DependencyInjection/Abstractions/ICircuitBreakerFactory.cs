using System;

namespace Polly.CircuitBreaker.Extensions.DependencyInjection.Abstractions
{
    public interface ICircuitBreakerFactory
    {
        ICircuitBreaker CreateCircuitBreaker(string categoryName);
        ICircuitBreaker CreateCircuitBreaker(Type type);
        ICircuitBreaker CreateCircuitBreaker<T>();
    }
}