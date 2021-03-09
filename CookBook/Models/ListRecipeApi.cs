using System.Collections.Generic;
using Newtonsoft.Json;

namespace CookBook.Models
{
    public class ListRecipeApi
    {
        [JsonProperty("parentsLevel")]
        public List<ParentApi> Parents { get; set; }
        [JsonProperty("recipes")]
        public List<RecipeApi> Recipes { get; set; }
    }
}