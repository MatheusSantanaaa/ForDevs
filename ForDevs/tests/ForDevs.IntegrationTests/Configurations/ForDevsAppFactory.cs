﻿using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ForDevs.IntegrationTests.Configurations
{
    public class ForDevsAppFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        private readonly string _environment;

        public ForDevsAppFactory(string environment = "Development")
        {
            _environment = environment;
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment(_environment);

            builder.ConfigureServices(services =>
            {
                services.AddMemoryCache();
            });

            return base.CreateHost(builder);
        }
    }
}
