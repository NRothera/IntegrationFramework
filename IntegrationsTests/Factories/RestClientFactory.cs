using IntegrationsTests.Interfaces;
using IntegrationsTests.Services;
using IntegrationsTests.Services.Requests;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace IntegrationsTests.Factories
{
    public class RestClientFactory : IServiceFactory
    {
        private RestClient client { get; set; }
        private ITestConfiguration config;

        public RestClientFactory(IRestClient client, ITestConfiguration config)
        {
            this.client = client as RestClient;
            
            this.config = config;
        }

        public void resetCookies()
        {
            client.CookieContainer = new CookieContainer();
        }

        public RestClient GetClient()
        {
            return client;
        }

        public T Get<T>(Request request, string configKey) where T : ClientService
        {
            client.BaseUrl = new Uri(config.GetResource(configKey));

            return (T)Activator.CreateInstance(typeof(T), request, client, config);
        }

        /*public RestCountriesService get(GetCountryRequest countryRequest)
        {
            return new RestCountriesService(countryRequest, client);
        }*/
    }
}

