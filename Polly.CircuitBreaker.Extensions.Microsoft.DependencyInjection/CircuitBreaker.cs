using Polly;
using Polly.CircuitBreaker.Extensions.DependencyInjection.Abstractions;
using System;

namespace Polly.CircuitBreaker.Extensions.DependencyInjection
{
    public class CircuitBreaker : ICircuitBreaker
    {
        private AsyncCircuitBreakerPolicy _polices;

        public int ExceptionsAllowedBeforeBreaking { get; } = 1;

        public TimeSpan DurationOfBreak { get; } = TimeSpan.FromMinutes(1);

        internal CircuitBreaker(int exceptionsAllowedBeforeBreaking, TimeSpan durationOfBreak)
        {
            ExceptionsAllowedBeforeBreaking = exceptionsAllowedBeforeBreaking;
            DurationOfBreak = durationOfBreak;

            if (durationOfBreak == TimeSpan.Zero)
                throw new ArgumentException($"Required input {nameof(durationOfBreak)} was zero.", nameof(durationOfBreak));
        }

        public AsyncCircuitBreakerPolicy GetPolicy<TException>() where TException : Exception
        {
            return _polices = _polices ?? Policy
                .Handle<TException>()
                .CircuitBreakerAsync(ExceptionsAllowedBeforeBreaking, DurationOfBreak);
        }

        public AsyncCircuitBreakerPolicy GetPolicy<TException>(Action<Exception, TimeSpan> onBreak, Action onReset) where TException : Exception
        {
            return _polices = _polices ?? Policy
                .Handle<TException>()
                .CircuitBreakerAsync(ExceptionsAllowedBeforeBreaking, DurationOfBreak, onBreak, onReset);
        }

        public AsyncCircuitBreakerPolicy GetPolicy<TException>(Action<Exception, TimeSpan, Context> onBreak, Action<Context> onReset) where TException : Exception
        {
            return _polices = _polices ?? Policy
                .Handle<TException>()
                .CircuitBreakerAsync(ExceptionsAllowedBeforeBreaking, DurationOfBreak, onBreak, onReset);
        }

        public AsyncCircuitBreakerPolicy GetPolicy<TException>(Action<Exception, TimeSpan> onBreak, Action onReset, Action onHalfOpen) where TException : Exception
        {
            return _polices = _polices ?? Policy
                .Handle<TException>()
                .CircuitBreakerAsync(ExceptionsAllowedBeforeBreaking, DurationOfBreak, onBreak, onReset, onHalfOpen);
        }

        public AsyncCircuitBreakerPolicy GetPolicy<TException>(Action<Exception, TimeSpan, Context> onBreak, Action<Context> onReset, Action onHalfOpen) where TException : Exception
        {
            return _polices = _polices ?? Policy
                .Handle<TException>()
                .CircuitBreakerAsync(ExceptionsAllowedBeforeBreaking, DurationOfBreak, onBreak, onReset, onHalfOpen);
        }

        public AsyncCircuitBreakerPolicy GetPolicy<TException>(Action<Exception, CircuitState, TimeSpan, Context> onBreak, Action<Context> onReset, Action onHalfOpen) where TException : Exception
        {
            return _polices = _polices ?? Policy
                .Handle<TException>()
                .CircuitBreakerAsync(ExceptionsAllowedBeforeBreaking, DurationOfBreak, onBreak, onReset, onHalfOpen);
        }
    }

    public class CircuitBreaker<TCategoryName> : ICircuitBreaker<TCategoryName>
    {
        private readonly ICircuitBreaker _cb;

        public int ExceptionsAllowedBeforeBreaking => _cb.ExceptionsAllowedBeforeBreaking;

        public TimeSpan DurationOfBreak => _cb.DurationOfBreak;

        internal CircuitBreaker(ICircuitBreakerFactory circuitBreakerFactory)
        {
            _cb = circuitBreakerFactory.CreateCircuitBreaker<TCategoryName>();
        }

        public AsyncCircuitBreakerPolicy GetPolicy<TException>() where TException : Exception
        {
            return _cb.GetPolicy<TException>();
        }

        public AsyncCircuitBreakerPolicy GetPolicy<TException>(Action<Exception, TimeSpan> onBreak, Action onReset) where TException : Exception
        {
            return _cb.GetPolicy<TException>(onBreak, onReset);
        }

        public AsyncCircuitBreakerPolicy GetPolicy<TException>(Action<Exception, TimeSpan, Context> onBreak, Action<Context> onReset) where TException : Exception
        {
            return _cb.GetPolicy<TException>(onBreak, onReset);
        }

        public AsyncCircuitBreakerPolicy GetPolicy<TException>(Action<Exception, TimeSpan> onBreak, Action onReset, Action onHalfOpen) where TException : Exception
        {
            return _cb.GetPolicy<TException>(onBreak, onReset, onHalfOpen);
        }

        public AsyncCircuitBreakerPolicy GetPolicy<TException>(Action<Exception, TimeSpan, Context> onBreak, Action<Context> onReset, Action onHalfOpen) where TException : Exception
        {
            return _cb.GetPolicy<TException>(onBreak, onReset, onHalfOpen);
        }

        public AsyncCircuitBreakerPolicy GetPolicy<TException>(Action<Exception, CircuitState, TimeSpan, Context> onBreak, Action<Context> onReset, Action onHalfOpen) where TException : Exception
        {
            return _cb.GetPolicy<TException>(onBreak, onReset, onHalfOpen);
        }
    }
}
