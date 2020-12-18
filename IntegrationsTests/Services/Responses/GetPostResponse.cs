using IntegrationsTests.Models;
using IntegrationsTests.Services.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationsTests.Services.Responses
{
    public class GetPostResponse : Response
    {
        public Posts Posts { get; set; }
    }
}
