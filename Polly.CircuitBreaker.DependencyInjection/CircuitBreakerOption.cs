using System;
using System.Collections.Generic;

namespace Polly.CircuitBreaker.Extensions.DependencyInjection
{
    public class CircuitBreakerOption
    {
        public int ExceptionsAllowed { get; set; } = 1;
        public TimeSpan DurationOfBreak { get; set; } = TimeSpan.FromMinutes(1);
        public IDictionary<string, CircuitBreaker> CircuitBreakers { get; set; }
    }
}
