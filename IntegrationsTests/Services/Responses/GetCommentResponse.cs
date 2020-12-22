using System;
using System.Collections.Generic;
using IntegrationsTests.Models;

namespace IntegrationsTests.Services.Responses
{
    public class GetCommentResponse : Response
    {
        
        public List<Comment> Comments { get; set; }

        public GetCommentResponse()
        {
            Comments = new List<Comment>();
        }
        
    }
}
