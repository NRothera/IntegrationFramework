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
    [Binding]
    public class PostsSteps
    {
        private RestPostsService _restPostService;
        private RestClientFactory _restClientFactory;
        private string _id;

        public PostsSteps()
        {
            var client = new RestClient();
            var config = new TestConfiguration();
            _restClientFactory = new RestClientFactory(client, config);
        }

        [Given(@"I request a post with id (.*)")]
        public void GivenIRequestAPostWithId(int i)
        {
            _id = i.ToString();
            var path = new GetPostPathParams(PostPaths.GetPostById);
            var segments = new Dictionary<string, string> { { path.replaceableSegments.First(), _id.ToString() } };
            path.urlSegments = segments;

            _restPostService = _restClientFactory.Get<RestPostsService>(new GetPostRequest(new RESTHeaders(), path), "BasePostUrl");
            _restPostService.Invoke(true);
        }

        [Then(@"I ensure the server set response headers are correct")]
        public void ThenIEnsureTheServerSetResponseHeadersAreCorrect()
        {
            var cacheControl = GetResponseHeader("Cache-Control");
            var xRateLimit = GetResponseHeader("X-Ratelimit-Limit");
            var contentType = GetResponseHeader("Content-Type").Split(";")[0];
            var accessControl = GetResponseHeader("Access-Control-Allow-Credentials");

            cacheControl.Should().BeEquivalentTo("max-age=43200");
            xRateLimit.Should().BeEquivalentTo("1000");
            contentType.Should().BeEquivalentTo("application/json");
            accessControl.Should().BeEquivalentTo("true");
        }

        [Given(@"I get a (.*) response")]
        public void GivenIGetAResponse(int p0)
        {
            Console.WriteLine(_restPostService.GetResponse());
        }

        [Then(@"the ""(.*)"" response contains all the required properties")]
        public void ThenTheResponseContainsAllTheRequiredProperties(string responseType)
        {
            switch (responseType)
            {
                case ("Post"):
                    var response = _restPostService.PostResponse.Posts;

                    response.Id.Should().Equals(_id.ToString());
                    response.Title.Should().BeOfType(typeof(string));
                    response.Body.Should().BeOfType(typeof(string));
                    response.UserId.Should().BeOfType(typeof(int));
                    break;
            }
        }

        [Then(@"I can validate that the ""(.*)"" response is empty")]
        public void ThenICanValidateThatTheResponseIsEmpty(string responseType)
        {
            

            switch (responseType)
            {
                case ("Post"):
                    var response = _restPostService.PostResponse.Posts;

                    response.Id.Should().Be(0);
                    response.Title.Should().BeNullOrEmpty();
                    response.Body.Should().BeNullOrEmpty();
                    response.UserId.Should().Be(0);
                    break;

                default:
                    break;
            }
        }

        [Then(@"I can check the response time is under (.*) milliseconds")]
        public void ThenICanCheckTheResponseTimeIsUnderMilliseconds(int p0)
        {
            Console.WriteLine("");
        }

    }
}
