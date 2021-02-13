using FluentAssertions;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Polly.CircuitBreaker.DependencyInjection.Tests
{
    [TestClass]
    public class CircuitBreakerFactoryTests
    {
        private readonly Mock<IOptionsMonitor<CircuitBreakerOption>> _opt;

        public CircuitBreakerFactoryTests()
        {
            _opt = new Mock<IOptionsMonitor<CircuitBreakerOption>>();
        }

        [TestMethod]
        public void ShouldReturnSameBreakerWhenCreateMultiple()
        {
            // given
            var opt = new CircuitBreakerOption();
            _opt.Setup(x => x.CurrentValue).Returns(opt);

            var factory = new CircuitBreakerFactory(_opt.Object);

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
            var expect = new CircuitBreaker(2, TimeSpan.FromMinutes(1));

            var cbs = new Dictionary<string, CircuitBreaker> { { "test", expect } };

            var opt = new CircuitBreakerOption { CircuitBreakers = cbs };

            _opt.Setup(x => x.CurrentValue).Returns(opt);

            var factory = new CircuitBreakerFactory(_opt.Object);

            // when
            var cb = factory.CreateCircuitBreaker("test");

            // then
            cb.Should().BeEquivalentTo(expect);
            cb.ExceptionsAllowedBeforeBreaking.Should().Be(expect.ExceptionsAllowedBeforeBreaking);
        }
    }
}
