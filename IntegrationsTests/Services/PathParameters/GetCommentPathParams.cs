using System;
namespace IntegrationsTests.Services.PathParameters
{
    public class GetCommentPathParams : PathParams
    {
        public GetCommentPathParams(CommentPath path)
        {
            resource = path.resource;
        }

        public static class CommentParams
        {
            /// <summary>
            /// <para>Resource used to get an existing comment</para>
            /// <para>Method: GET</para>
            /// <para>URLSegements to be replaced: name</para>
            /// </summary>
            public static CommentPath GetCommentId = new CommentPath() { Method = "GET", resource = "/{id}/comments" };

        }

        public class CommentPath
        {
            public string Method { get; set; }
            public string resource { get; set; }
        }
    }
}
