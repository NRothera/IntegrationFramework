﻿using IntegrationsTests.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace IntegrationsTests.Config
{
    public class TestConfiguration : ITestConfiguration
    {
            public TestConfiguration()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var builder = new ConfigurationBuilder()
                .AddJsonFile($"{path}/appsettings.json",
                             optional: true,
                             reloadOnChange: true);

            Configuration = builder.Build();

        }

        public IConfiguration Configuration { get; }
        public string BasePostUrl => Configuration["BasePostUrl"];
        public string BaseCountryUrl => Configuration["BaseCountryUrl"];
        
        public string GetResource(string key)
        {
            return Configuration[key];
        }

    }
}
