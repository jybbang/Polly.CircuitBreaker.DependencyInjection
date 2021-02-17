using Microsoft.Extensions.Options;
using Polly.CircuitBreaker.DependencyInjection.Abstractions;
using System;
using System.Collections.Concurrent;

namespace Polly.CircuitBreaker.DependencyInjection
{
    public class CircuitBreakerFactory : ICircuitBreakerFactory
    {
        private readonly ConcurrentDictionary<string, ICircuitBreaker> _circuitBreakers = new ConcurrentDictionary<string, ICircuitBreaker>();
        private readonly CircuitBreakerOptions _opt;

        public CircuitBreakerFactory(IOptions<CircuitBreakerOptions> opt)
        {
            _opt = opt.Value;
        }

        public ICircuitBreaker CreateCircuitBreaker<T>()
        {
            return CreateCircuitBreaker(typeof(T).Name);
        }

        public ICircuitBreaker CreateCircuitBreaker(Type type)
        {
            return CreateCircuitBreaker(type.Name);
        }

        public ICircuitBreaker CreateCircuitBreaker(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
                throw new ArgumentException($"Required input {nameof(categoryName)} was null or empty.", nameof(categoryName));

            CircuitBreaker cb = null;
            _opt?.CircuitBreakers?.TryGetValue(categoryName, out cb);

            return _circuitBreakers.GetOrAdd(
                categoryName,
                cb ?? new CircuitBreaker(_opt.ExceptionsAllowedBeforeBreaking, _opt.DurationOfBreak));
        }
    }
}
