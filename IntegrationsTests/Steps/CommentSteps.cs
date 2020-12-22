using System;
using IntegrationsTests.Config;
using IntegrationsTests.Services;
using IntegrationsTests.Services.Headers;
using IntegrationsTests.Services.Requests;
using IntegrationsTests.Services.PathParameters;
using IntegrationsTests.Factories;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using IntegrationsTests.Interfaces;
using FluentAssertions;

namespace IntegrationsTests.Steps
{
    [Binding]
    public class CommentSteps
    {
        private CommentsService _restPostService;
        private RestClientFactory _restClientFactory;
        private string _id;

        public CommentSteps()
        {
            var client = new RestClient();
            var config = new TestConfiguration();
            _restClientFactory = new RestClientFactory(client, config);
        }

        [Given(@"I request a comment with id (.*)")]
        public void GivenIRequestACommentWithId(int i)
        {
            _id = i.ToString();
            var path = new GetCommentPathParams(CommentPaths.GetCommentId);
            var segments = new Dictionary<string, string> { { path.replaceableSegments.First(), _id.ToString() } };
            path.urlSegments = segments;

            _restPostService = _restClientFactory.Get<CommentsService>(new GetCommentRequest(new RESTHeaders(), path), "BaseCommentUrl");
            _restPostService.Invoke(true);
        }

        [Then(@"the ""(.*)"" response contains all the required properties")]
        public void ThenTheResponseContainsAllTheRequiredProperties(string p0)
        {
            var response = _restPostService.CommentResponse.Comments;

            foreach (var comment in response)
            {
                comment.Name.Should().BeOfType(typeof(string));
                //IsValidEmail(comment.Email).Should().BeTrue();
                comment.PostId.Should().BeOfType(typeof(string));
            }
        }


    }
}
