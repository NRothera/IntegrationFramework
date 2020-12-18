using System;
using System.Collections.Generic;
using System.Text;
using IntegrationsTests.Interfaces;
using IntegrationsTests.Services.Headers;
using IntegrationsTests.Services.PathParameters;

namespace  IntegrationsTests.Services.Requests
{
    public class Request : IRequest
    {

        private readonly RESTHeaders headers;
        private readonly RestSharp.Method httpMethod;
        private readonly IQueryParams queryParameters;
        private readonly IRequestBody requestBody;
        private readonly PathParams pathParameters;

        /// <summary>
        /// this class models a request that can be sent to a service
        /// </summary>
        /// <param name="headers">the headers that should be sent as part of the request</param>
        /// <param name="httpMethod">the httpMethod of the request</param>
        /// <param name="pathParameters">any path parameters required</param>
        /// <param name="queryParameters">any query parameters required</param>
        /// <param name="requestBody">a request body if required</param>
        public Request(RESTHeaders headers, RestSharp.Method httpMethod, PathParams pathParameters, IQueryParams queryParameters, IRequestBody requestBody)
        {
            this.headers = headers;
            this.httpMethod = httpMethod;
            this.pathParameters = pathParameters;
            this.queryParameters = queryParameters;
            this.requestBody = requestBody;
        }

        public RestSharp.Method GetHttpMethod()
        {
            return httpMethod;
        }

        public IHeaders GetHeaders()
        {
            return headers;
        }

        public IQueryParams GetQueryParameters()
        {
            return queryParameters;
        }

        public IRequestBody GetRequestBody()
        {
            return requestBody;
        }

        public PathParams GetPathParameters()
        {
            return pathParameters;
        }
    }
}
