using Newtonsoft.Json;

namespace CookBook.Models
{
    public class ParentApi
    {
        [JsonProperty("level")]
        public string Level { get; set; }
        [JsonProperty("Title")]
        public string Title { get; set; }
    }
}