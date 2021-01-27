using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using IntegrationsTests.Interfaces;

namespace IntegrationsTests.Services.PathParameters
{
    public class PathParams : IPathParams
    {
        /// <summary>
        /// Resource path, eg /help
        /// </summary>
        private String resourcePath;

        /// <summary>
        /// <para>The resource part of the URL, eg "/flight"</para>
        /// <para>Replaceable parts of the URL should be in curly braces, eg "/post/{post-id}</para>
        /// </summary>
        public String resource
        {
            get
            {
                return resourcePath;
            }
            set
            {
                resourcePath = value;
                var matches = Regex.Matches(value, @"{([^{}]*)");
                replaceableSegments = matches.Cast<Match>().Select(m => m.Groups[1].Value).Distinct().ToList();
            }
        }

        /// <summary>
        /// A list from the resource of replaceable URL segments which need to be implemented
        /// </summary>
        public List<string> replaceableSegments { get; private set; }

        /// <summary>
        /// A dictionary of replaceable URL segments and their value, eg "post-id, 100"
        /// </summary>
        public Dictionary<string, string> urlSegments { get; set; }
    }
}
