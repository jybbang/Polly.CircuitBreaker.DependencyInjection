using System;
using System.Collections.Generic;
using System.Text;

namespace Polly.CircuitBreaker.DependencyInjection
{
    public class CircuitBreakerOptionBuilder
    {
        private CircuitBreakerOptions _option = new CircuitBreakerOptions();

        public CircuitBreakerOptionBuilder WithExceptionsAllowedBeforeBreaking(int value)
        {
            _option.ExceptionsAllowedBeforeBreaking = value;
            return this;
        }

        public CircuitBreakerOptionBuilder WithDurationOfBreak(TimeSpan value)
        {
            _option.DurationOfBreak = value;
            return this;
        }

        public CircuitBreakerOptionBuilder WithCircuitBreakers(IDictionary<string, CircuitBreaker> value)
        {
            _option.CircuitBreakers = value;
            return this;
        }

        internal CircuitBreakerOptions Build()
        {
            return _option;
        }
    }
}
