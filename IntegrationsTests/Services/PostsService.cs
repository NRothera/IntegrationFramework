using IntegrationsTests.Interfaces;
using IntegrationsTests.Services.Responses;
using IntegrationsTests.Services.Requests;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using IntegrationsTests.Models;

namespace IntegrationsTests.Services
{
    public class PostsService : ClientService
    {
        public GetPostResponse PostResponse { get; set; }

        public PostsService(Request request, RestClient client, ITestConfiguration config)
            : base(request, client, config.BasePostUrl, config)
        {
        }

        public override IResponse GetResponse()
        {
            AssertThatServiceCallWasSuccessful();
            return PostResponse;
        }

        protected override void CheckThatResponseBodyIsPopulated()
        {
            CheckThatResponseBodyIsPopulated(PostResponse);
        }

        protected override void MapResponse()
        {
            PostResponse = new GetPostResponse();
            PostResponse.Posts = JsonConvert.DeserializeObject<Posts>(response.Content);
        }

    }
}
