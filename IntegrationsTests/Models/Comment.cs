using System;
using Newtonsoft.Json;

namespace IntegrationsTests.Models
{
    public class Comment
    {
        [JsonProperty("PostId")]
         public string PostId { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
    }
}
