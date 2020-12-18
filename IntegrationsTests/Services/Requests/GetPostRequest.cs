using IntegrationsTests.Services.Headers;
using IntegrationsTests.Services.PathParameters;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationsTests.Services.Requests
{
    public class GetPostRequest : Request
    {
        public GetPostRequest(RESTHeaders headers, PathParams pathParameters)
        : base(headers, Method.GET, pathParameters, null, null)
        {
        }
    }
}
