using System;

namespace Polly.CircuitBreaker.DependencyInjection.Abstractions
{
    public interface ICircuitBreakerFactory
    {
        ICircuitBreaker CreateCircuitBreaker(string categoryName);
        ICircuitBreaker CreateCircuitBreaker(Type type);
        ICircuitBreaker CreateCircuitBreaker<T>();
    }
}