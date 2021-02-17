using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Polly.CircuitBreaker.DependencyInjection.Abstractions;
using System;
using System.Collections.Generic;

namespace Polly.CircuitBreaker.DependencyInjection.Tests
{
    [TestClass]
    public class CircuitBreakerFactoryTests : TestBase
    {
        public override void ConfigureServices(ServiceCollection service, IConfiguration configuration)
        {
            service.AddCircuitBreaker(configuration);
            service.AddTransient<Di>();
            service.AddTransient<DiFactory>();
        }

        [TestMethod]
        public void ShouldReturnSameBreakerWhenCreateMultiple()
        {
            // given
            var optionMock = new Mock<IOptions<CircuitBreakerOptions>>();

            var opt = new CircuitBreakerOptions();

            optionMock.Setup(x => x.Value).Returns(opt);

            var factory = new CircuitBreakerFactory(optionMock.Object);

            // when
            var cb = factory.CreateCircuitBreaker("test");
            var cb2 = factory.CreateCircuitBreaker("test");

            // then
            cb.Should().BeEquivalentTo(cb2);
        }

        [TestMethod]
        public void ShouldReturnPredefinedOption()
        {
            // given
            var factory = _services.GetRequiredService<ICircuitBreakerFactory>();

            // when
            var cb = factory.CreateCircuitBreaker("category1");
            var cb2 = factory.CreateCircuitBreaker("category1");

            // then
            cb.Should().BeEquivalentTo(cb2);
            cb.ExceptionsAllowedBeforeBreaking.Should().Be(cb2.ExceptionsAllowedBeforeBreaking);
        }

        [TestMethod]
        public void ShouldReturnSameCbUsingDi()
        {
            // given
            var cb = _services.GetRequiredService<Di>().Cb;
            var cb2 = _services.GetRequiredService<Di>().Cb;

            // then
            cb.Should().BeEquivalentTo(cb2);
        }

        [TestMethod]
        public void ShouldReturnSameCbUsingFactory()
        {
            // given
            var cb = _services.GetRequiredService<DiFactory>().Cb;
            var cb2 = _services.GetRequiredService<DiFactory>().Cb;

            // then
            cb.Should().BeEquivalentTo(cb2);
        }

        public class Di
        {
            public ICircuitBreaker Cb { get; }

            public Di(ICircuitBreaker<Di> cb)
            {
                Cb = cb;
            }
        }

        public class DiFactory
        {
            public ICircuitBreaker Cb { get; }

            public DiFactory(ICircuitBreakerFactory factory)
            {
                Cb = factory.CreateCircuitBreaker<DiFactory>();
            }
        }
    }
}
