using System;
using IntegrationsTests.Services.Headers;
using IntegrationsTests.Services.PathParameters;
using RestSharp;

namespace IntegrationsTests.Services.Requests
{
    public class GetCommentRequest : Request
    {
        public GetCommentRequest(RESTHeaders headers, PathParams pathParameters)
        : base(headers, Method.GET, pathParameters, null, null)
        {
        }
    }
}
