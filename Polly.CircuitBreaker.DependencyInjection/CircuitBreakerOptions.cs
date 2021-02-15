using System;
using System.Collections.Generic;

namespace Polly.CircuitBreaker.DependencyInjection
{
    public class CircuitBreakerOptions
    {
        public int ExceptionsAllowedBeforeBreaking { get; set; } = 1;
        public TimeSpan DurationOfBreak { get; set; } = TimeSpan.FromMinutes(1);
        public IDictionary<string, CircuitBreaker> CircuitBreakers { get; set; }
    }
}
