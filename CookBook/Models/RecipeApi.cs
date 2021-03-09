using System;
using Newtonsoft.Json;

namespace CookBook.Models
{
    public class RecipeApi
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("level")]
        public string Level { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("timeCreate")]
        public DateTime CreateTime { get; set; }
    }
}
