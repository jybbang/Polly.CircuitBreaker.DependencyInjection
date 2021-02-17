using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Polly.CircuitBreaker.DependencyInjection.Tests
{
    [TestClass]
    public abstract class TestBase
    {
        public ServiceProvider _services;

        [TestInitialize]
        public void TestSetUp()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            var services = new ServiceCollection();

            ConfigureServices(services, configuration);

            _services = services.BuildServiceProvider();
        }

        public abstract void ConfigureServices(ServiceCollection service, IConfiguration configuration);
    }
}
