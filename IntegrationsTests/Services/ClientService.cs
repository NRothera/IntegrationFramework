using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using IntegrationsTests.Interfaces;
using IntegrationsTests.Services.Requests;

namespace IntegrationsTests.Services
{

    public abstract class ClientService : IService
    {
        protected readonly RestClient client;
        public IRequest request;
        protected RestResponse response;
        private bool successful;
        public string resource;
        private RestRequest _restRequest;
        private readonly CookieContainer _cookieJar = new CookieContainer();
        public ITestConfiguration config;

        public ClientService(IRequest request, RestClient client, string resource, ITestConfiguration config)
        {
            this.request = request;
            this.client = client;
            if (client.CookieContainer == null)
            {
                this.client.CookieContainer = _cookieJar;
            }
            this.resource = resource;
        }

        /// <summary>
        /// Invokes the request object to be sent to the service with specified patyh, query params etc
        /// </summary>
        public void Invoke(bool expectSuccess)
        {
            _restRequest = new RestRequest(request.GetHttpMethod());
            _restRequest.Resource = resource;

            if (request.GetQueryParameters() != null)
            {
                AddQueryParams();
            }
            if (request.GetPathParameters() != null)
            {
                AddPathParams();
            }
            try
            {
                this.response = AddHeadersAndExecuteRequest();
            }
            catch (Exception pe)
            {

                Console.WriteLine(pe.Message, pe);
            }
            SetSuccessState();
            if (successful)
            {
                MapResponse();
                CheckThatResponseBodyIsPopulated();
            }
           
        }

        protected abstract void MapResponse();

        /// <summary>
        /// Asserts errors returned, allows for fluent chaining
        /// </summary>
        /// <returns>IAssertion object</returns>
        public T AssertThatErrors<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }

        /// <summary>
        /// Asserts that the service returned 200 OK
        /// </summary>
        protected void AssertThatServiceCallWasSuccessful()
        {
            response.Should().NotBeNull();
            successful.Should().BeTrue("The service was not called successfully.  Response code was: " + response.StatusCode.ToString());
        }

        /// <summary>
        /// Checks the response was not empty
        /// </summary>
        protected abstract void CheckThatResponseBodyIsPopulated();

        //protected abstract void GetResponseHeaders();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expectedResponseContent"></param>
        protected void CheckThatResponseBodyIsPopulated(Object expectedResponseContent)
        {
            expectedResponseContent.Should().NotBeNull("The message body was not populated but the service reported a 200 OK");
        }

        /// <summary>
        /// Adds the provided headers and executes the request against the service
        /// </summary>
        /// <returns>RestResponse</returns>
        private RestResponse AddHeadersAndExecuteRequest()
        {

            IRestResponse response = null;
            var stopWatch = new Stopwatch();

            foreach (var header in request.GetHeaders().Headers)
            {
                _restRequest.AddHeader(header.Name, header.Value);
            }

            switch (request.GetHttpMethod())
            {
                case Method.GET:

                    break;
                case Method.POST:
                    _restRequest.RequestFormat = DataFormat.Json;
                    _restRequest.AddJsonBody(request.GetRequestBody());
                    break;
                case Method.PUT:
                    try
                    {
                        _restRequest.AddJsonBody(request.GetRequestBody());
                    }
                    catch
                    {
                        _restRequest.AddJsonBody(request.GetRequestBody());
                    }
                    break;
                case Method.DELETE:
                    break;
            }

            try
            {
                stopWatch.Start();
                response = client.Execute(_restRequest);
                stopWatch.Stop();

                return response as RestResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error executing request: " + ex);
            }
            finally
            {
                LogRequest(_restRequest, response, stopWatch.ElapsedMilliseconds);
            }

            return null;

        }

        private void SetSuccessState()
        {
            this.successful = (response.StatusCode == System.Net.HttpStatusCode.OK);
        }

        private void AssertThatServiceCallWasNotSuccessful()
        {
            successful.Should().BeFalse("The service returned a: " + response.StatusCode.ToString() + ":" + response.ErrorMessage);
        }

        /// <summary>
        /// Add the PathParams to the request
        /// </summary>
        private void AddPathParams()
        {
            _restRequest.Resource = _restRequest.Resource.ToString() + request.GetPathParameters().resource;
            if (!request.GetPathParameters().replaceableSegments.Any()) return;
            foreach (string segment in request.GetPathParameters().replaceableSegments)
            {
                //check that any specified as replacable are actually replaced
                request.GetPathParameters().urlSegments.Where(x => x.Key == segment).FirstOrDefault().Should().NotBeNull("URL Segement defined as pathParameter has not been replaced: " + segment);
                KeyValuePair<string, string> pathParam = request.GetPathParameters().urlSegments.Where(x => x.Key == segment).FirstOrDefault();
                _restRequest.AddParameter(pathParam.Key, pathParam.Value, ParameterType.UrlSegment);
            }
        }

        /// <summary>
        /// Add the QueryParams to the request
        /// </summary>
        private void AddQueryParams()
        {
            foreach (KeyValuePair<string, string> queryParam in request.GetQueryParameters().getParameters())
            {
                _restRequest.AddQueryParameter(queryParam.Key, queryParam.Value);
            }
        }

        private void LogRequest(IRestRequest request, IRestResponse response, long durationMs)
        {

            var requestToLog = new
            {
                resource = request.Resource,
                // Parameters are custom anonymous objects in order to have the parameter type as a nice string
                // otherwise it will just show the enum value
                parameters = request.Parameters.Select(parameter => new
                {
                    name = parameter.Name,
                    value = parameter.Value,
                    type = parameter.Type.ToString()
                }),
                // ToString() here to have the method as a nice string otherwise it will just show the enum value
                method = request.Method.ToString(),
                // This will generate the actual Uri used in the request
                uri = client.BuildUri(request),
            };

            var responseToLog = new
            {
                statusCode = response.StatusCode,
                headers = response.Headers,
                responseUri = response.ResponseUri,
                content = JsonConvert.DeserializeObject<dynamic>(response.Content),
                errorMessage = response.ErrorMessage,
            };

            Console.WriteLine(
                $"Request completed in {durationMs} ms \r\n Request: \r\n " +
                $"{JsonConvert.SerializeObject(requestToLog, Formatting.Indented, new JsonConverter[] {new StringEnumConverter()})} " +
                $"\r\n Response: \r\n {JsonConvert.SerializeObject(responseToLog, Formatting.Indented, new JsonConverter[] {new StringEnumConverter()})}");

        }

        public T Get<T>(Request request, string configKey) where T : ClientService
        {
            client.BaseUrl = new Uri(config.GetResource(configKey));

            return (T)Activator.CreateInstance(typeof(T), request, client, config);
        }

        public virtual IResponse GetResponse()
        {
            throw new NotImplementedException();
        }

        public virtual T AssertThat<T>()
        {
            throw new NotImplementedException();
        }
    }
}
