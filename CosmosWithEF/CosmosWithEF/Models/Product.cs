using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CosmosWithEF.Models
{
    public class Product
    {
        [JsonProperty("id")]
      
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        
    }
}
