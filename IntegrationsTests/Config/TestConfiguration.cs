using IntegrationsTests.Interfaces;
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
                             reloadOnChange: true)
                .AddEnvironmentVariables();

            builder.AddUserSecrets<TestConfiguration>(true);

            Configuration = builder.Build();

        }

        public IConfiguration Configuration { get; }
        public string BasePostUrl => Configuration["BasePostUrl"];
        public string BaseCommentUrl => Configuration["BaseCommentUrl"];
        public string ExampleName => Configuration["ExampleName"];
        public string ExampleTwo => Configuration["Example-Two"];
        public string GetResource(string key)
        {
            return Configuration[key];
        }

    }
}
