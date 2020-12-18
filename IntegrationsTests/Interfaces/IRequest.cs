using IntegrationsTests;
using IntegrationsTests.Services.PathParameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationsTests.Interfaces
{
    public interface IRequest
    {
        /// <summary>
        /// Get all headers for a request
        /// </summary>
        /// <returns></returns>
        IHeaders GetHeaders();

        /// <summary>
        /// Gets all query parameters for a request
        /// </summary>
        /// <returns></returns>
        IQueryParams GetQueryParameters();

        /// <summary>
        /// Gets the request body for a request
        /// </summary>
        /// <returns></returns>
        IRequestBody GetRequestBody();

        /// <summary>
        /// Sets the HTTP method for the request, eg GET, POST
        /// </summary>
        /// <returns></returns>
        RestSharp.Method GetHttpMethod();

        PathParams GetPathParameters();
    }
}
