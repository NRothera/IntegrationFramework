using System;
using IntegrationsTests.Config;
using IntegrationsTests.Services;
using IntegrationsTests.Services.Headers;
using IntegrationsTests.Services.Requests;
using IntegrationsTests.Services.PathParameters;
using IntegrationsTests.Factories;
using System.Threading.Tasks;
using System;
using TechTalk.SpecFlow;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using IntegrationsTests.Interfaces;
using FluentAssertions;

namespace IntegrationsTests.Steps
{
    public class CommentSteps
    {
        private RestPostsService _restPostService;
        private RestClientFactory _restClientFactory;
        private string _id;

        public CommentSteps()
        {
            var client = new RestClient();
            var config = new TestConfiguration();
            _restClientFactory = new RestClientFactory(client, config);
        }
    }
}
