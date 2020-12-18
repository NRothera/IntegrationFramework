using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationsTests.Services.PathParameters
{
   
    public class GetPostPathParams : PathParams
    {
        public GetPostPathParams(PostPath path)
        {
            resource = path.resource;
        }
    }

    public static class PostPaths
    {
        /// <summary>
        /// <para>Resource used to get an existing post</para>
        /// <para>Method: GET</para>
        /// <para>URLSegements to be replaced: name</para>
        /// </summary>
        public static PostPath GetPostById = new PostPath() { Method = "GET", resource = "/{id}" };

    }

    public class PostPath
    {
        public string Method { get; set; }
        public string resource { get; set; }
    }
    
}
