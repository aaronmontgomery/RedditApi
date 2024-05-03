using System.Text.Json.Serialization;

namespace Models
{
    public class DataNested
    {
        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }

    public class Children
    {
        [JsonPropertyName("data")]
        public DataNested? Data { get; set; }
    }
    
    public class Data
    {
        [JsonPropertyName("children")]
        public IEnumerable<Children>? Childrens { get; set; }
    }
    
    public class PopularModel
    {
        [JsonPropertyName("data")]
        public Data? Data { get; set; }
    }
}
