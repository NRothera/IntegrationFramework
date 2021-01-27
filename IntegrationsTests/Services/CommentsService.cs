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
    public class CommentsService : ClientService
    {
        public GetCommentResponse CommentResponse { get; set; }

        public CommentsService(Request request, RestClient client, ITestConfiguration config)
          : base(request, client, config.BasePostUrl, config)
        {
        }

        public override IResponse GetResponse()
        {
            AssertThatServiceCallWasSuccessful();
            return CommentResponse;
        }

        protected override void CheckThatResponseBodyIsPopulated()
        {
            CheckThatResponseBodyIsPopulated(CommentResponse);
        }

        protected override void MapResponse()
        {
            CommentResponse = new GetCommentResponse();
            CommentResponse.Comments = JsonConvert.DeserializeObject<List<Comment>>(response.Content);
        }
    }
}
