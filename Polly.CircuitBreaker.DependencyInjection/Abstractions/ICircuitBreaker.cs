using System;

namespace Polly.CircuitBreaker.DependencyInjection.Abstractions
{
    public interface ICircuitBreaker
    {
        int ExceptionsAllowedBeforeBreaking { get; }

        TimeSpan DurationOfBreak { get; }

        AsyncCircuitBreakerPolicy GetPolicy<TException>() where TException : Exception;

        AsyncCircuitBreakerPolicy GetPolicy<TException>(Action<Exception, TimeSpan> onBreak, Action onReset) where TException : Exception;

        AsyncCircuitBreakerPolicy GetPolicy<TException>(Action<Exception, TimeSpan, Context> onBreak, Action<Context> onReset) where TException : Exception;

        AsyncCircuitBreakerPolicy GetPolicy<TException>(Action<Exception, TimeSpan> onBreak, Action onReset, Action onHalfOpen) where TException : Exception;

        AsyncCircuitBreakerPolicy GetPolicy<TException>(Action<Exception, TimeSpan, Context> onBreak, Action<Context> onReset, Action onHalfOpen) where TException : Exception;

        AsyncCircuitBreakerPolicy GetPolicy<TException>(Action<Exception, CircuitState, TimeSpan, Context> onBreak, Action<Context> onReset, Action onHalfOpen) where TException : Exception;
    }

    public interface ICircuitBreaker<TCategoryName> : ICircuitBreaker { }
}
