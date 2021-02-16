# Polly CircuitBreaker extensions for Microsoft.Extensions.DependencyInjection

[![NuGet](https://img.shields.io/nuget/v/Jyb.Polly.CircuitBreaker.DependencyInjection.svg)](https://www.nuget.org/packages/Jyb.Polly.CircuitBreaker.DependencyInjection/)
![Build And Deploy](https://github.com/jybbang/Polly.CircuitBreaker.DependencyInjection/workflows/Build%20And%20Deploy/badge.svg)
[![License: MIT](https://img.shields.io/badge/License-BSD3-yellow.svg)](https://github.com/jybbang/Polly.CircuitBreaker.DependencyInjection/blob/master/LICENSE)

**Polly.CircuitBreaker.DependencyInjection
 is a Polly CircuitBreaker extensions for Microsoft.Extensions.DependencyInjection.**

You can install [Polly.CircuitBreaker.DependencyInjection with NuGet](https://www.nuget.org/packages/Jyb.Polly.CircuitBreaker.DependencyInjection/):

```
Install-Package Jyb.Polly.CircuitBreaker.DependencyInjection
```

To use, with an `IServiceCollection` instance :

```c#
services.AddCircuitBreaker(Configuration);
```
Or, you don't use `IConfiguration`

```c#
services.AddCircuitBreaker();
```

CircuitBreaker Factory is registered as scope. You can configure this with the `serviceLifetime` parameter.

### ICircuitBreaker usage

To use sustainable CircuitBreaker, add a dependency on `ICircuitBreaker` (similar ILogger):

```c#
public class CircuitBreakerBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;
    private readonly ICircuitBreaker<TRequest> _cb;

    public CircuitBreakerBehaviour(ILogger<TRequest> logger, ICircuitBreaker<TRequest> cb)
    {
        _logger = logger;
        _cb = cb;
    }

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var policy = _cb.GetPolicy<FallbackHandledException>((ex, t) => _logger.LogWarning("Circuit broken."), () => _logger.LogInformation("Circuit reset."));
        return policy.ExecuteAsync(() => next());
    }
}
```

`ICircuitBreaker<CategoryName>` always return same object when CategoryName is same. (like a ILogger).

That means you can not change configuration after circuit breaker initialized. (for the first time calling the GetPolicy function with parameters.)

### Configuration usage

Starting config with your `appsettings.json` :

```json
"CircuitBreakerOptions" : {
  "CircuitBreakers" : {
    "category1" : { "ExceptionsAllowedBeforeBreaking" : 1, "DurationOfBreak" : "00:01:00" },
    "category2" : { "ExceptionsAllowedBeforeBreaking" : 2, "DurationOfBreak" : "00:02:00" },
    "category3" : { "ExceptionsAllowedBeforeBreaking" : 3, "DurationOfBreak" : "00:03:00" }
  }
}
```

## Copyright

Copyright (c) Jungyoung bang.

### POLLY

[Polly](https://github.com/App-vNext/Polly)


