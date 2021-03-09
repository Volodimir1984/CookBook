using Newtonsoft.Json;

namespace CookBook.Models
{
    public class RecipeUpdateApi
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}