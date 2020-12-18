using System;
using System.Collections.Generic;
using System.Text;
using IntegrationsTests.Interfaces;

namespace  IntegrationsTests.Services.Headers
{
    public class RESTHeaders : IHeaders
    {

        public List<IHeader> Headers { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public RESTHeaders()
        {
            Headers = new List<IHeader>();
        }

    }
}
