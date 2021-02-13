using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Polly.CircuitBreaker.DependencyInjection.Tests
{
    [TestClass]
    public class CircuitBreakerTests
    {
        [TestMethod]
        public void ShouldOccurredArgumentExceptionWhenDurationZero()
        {
            FluentActions.Invoking(() => new CircuitBreaker(1, TimeSpan.Zero))
               .Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void ShouldReturnSamePolicyWhenGetMultiple()
        {
            // given
            var cb = new CircuitBreaker(1, TimeSpan.FromMinutes(1));

            // when
            var policy = cb.GetPolicy<Exception>();
            var policy2 = cb.GetPolicy<Exception>();

            // then
            policy.Should().BeEquivalentTo(policy2);
        }
    }
}
